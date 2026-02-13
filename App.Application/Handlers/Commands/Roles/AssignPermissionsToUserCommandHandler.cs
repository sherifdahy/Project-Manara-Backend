

using App.Application.Commands.Roles;
using App.Application.Contracts.Responses.Roles;
using App.Application.Errors;
using App.Infrastructure.Abstractions.Consts;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Linq;

namespace App.Application.Handlers.Commands.Roles;

public class AssignPermissionsToUserCommandHandler(UserManager<ApplicationUser> userManager
    , AuthenticationErrors authenticationErrors
    , PermissionErrors permissionErrors
    , IUnitOfWork unitOfWork
    , IAuthenticationService authenticationService) : IRequestHandler<AssignPermissionsToUserCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly AuthenticationErrors _authenticationErrors = authenticationErrors;
    private readonly PermissionErrors _permissionErrors = permissionErrors;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthenticationService _authenticationService = authenticationService;

    public async Task<Result> Handle(AssignPermissionsToUserCommand request, CancellationToken cancellationToken)
    {

        if (await _userManager.FindByIdAsync(request.UserId.ToString()) is not { } user)
            return Result.Failure(_authenticationErrors.NotFound);


        var emptyClaimValues = request.ClaimValues?
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();

        if (emptyClaimValues == null || emptyClaimValues.Count == 0)
        {
            var entities = await _unitOfWork.UserClaimOverrides
                .FindAllAsync(uco => uco.ApplicationUserId == request.UserId, cancellationToken);

            _unitOfWork.UserClaimOverrides.DeleteRange(entities);
            await _unitOfWork.SaveAsync();

            return Result.Success();
        }

        var (defaultUserRoles,defaultUserPermissions) = await _authenticationService
            .GetUserOverrideRolesAndPermissions(user,cancellationToken,false);

        foreach (var claimValue in request.ClaimValues!)
        {
            if (!defaultUserPermissions.Contains(claimValue))
                return Result.Failure(_permissionErrors.InvalidPermissions);
        }

        var currentUserPermissionsOverride = await _unitOfWork.UserClaimOverrides
                .FindAllAsync(uco => uco.ApplicationUserId == request.UserId,cancellationToken);

        var newUserPermissionsOverride = request.ClaimValues.Except(currentUserPermissionsOverride.Select(x => x.ClaimValue));


        foreach (var newUserPermissionOverride in newUserPermissionsOverride)
        {
            await _unitOfWork.UserClaimOverrides
                 .AddAsync(new UserClaimOverride { ClaimValue = newUserPermissionOverride, ApplicationUserId = request.UserId });
        }

        var removedUserPermissionsOverride = currentUserPermissionsOverride
            .Select(x => x.ClaimValue).Except(request.ClaimValues);


        foreach (var removedUserPermissionOverride in removedUserPermissionsOverride)
        {
            var entityToRemove = currentUserPermissionsOverride.First(x => x.ClaimValue == removedUserPermissionOverride);
            _unitOfWork.UserClaimOverrides.Delete(entityToRemove);
        }

        await _unitOfWork.SaveAsync();

        return Result.Success();

    }


}
