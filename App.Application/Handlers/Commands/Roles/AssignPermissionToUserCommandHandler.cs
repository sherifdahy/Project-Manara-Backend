

using App.Application.Commands.Roles;
using App.Application.Contracts.Responses.Roles;
using App.Application.Errors;
using App.Core.Entities.Relations;
using App.Infrastructure.Abstractions.Consts;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace App.Application.Handlers.Commands.Roles;

public class AssignPermissionToUserCommandHandler(UserManager<ApplicationUser> userManager
    , AuthenticationErrors authenticationErrors
    , PermissionErrors permissionErrors
    , IUnitOfWork unitOfWork
    , IAuthenticationService authenticationService) : IRequestHandler<AssignPermissionToUserCommand, Result<AssignToUserPermissionResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly AuthenticationErrors _authenticationErrors = authenticationErrors;
    private readonly PermissionErrors _permissionErrors = permissionErrors;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthenticationService _authenticationService = authenticationService;

    public async Task<Result<AssignToUserPermissionResponse>> Handle(AssignPermissionToUserCommand request, CancellationToken cancellationToken)
    {

        if (await _userManager.FindByIdAsync(request.UserId.ToString()) is not { } user)
            return Result.Failure<AssignToUserPermissionResponse>(_authenticationErrors.NotFound);

        var allowedPermissions = Permissions.GetAllPermissions();

        if (!allowedPermissions.Contains(request.ClaimValue))
            return Result.Failure<AssignToUserPermissionResponse>(_permissionErrors.InvalidPermissions);

        var (userRoles, userPermission) = await _authenticationService.GetUserRolesAndPermissions(user, cancellationToken);

        if (userPermission.Contains(request.ClaimValue) && request.IsAllowed==true)
            return Result.Failure<AssignToUserPermissionResponse>(_permissionErrors.UserAlreadyHasPermission);
 

        var isOverrideExist =  _unitOfWork.UserPermissionOverrides
            .IsExist(x=>x.ClaimValue==request.ClaimValue && x.ApplicationUserId==request.UserId);

        if(isOverrideExist)
            return Result.Failure<AssignToUserPermissionResponse>(_permissionErrors.DuplicatedPermissionForUser);

        var userPermissionOverride = new UserPermissionOverride
        {
            ApplicationUserId = request.UserId,
            ClaimValue = request.ClaimValue,
            IsAllowed = request.IsAllowed
        };

        await _unitOfWork.UserPermissionOverrides.AddAsync(userPermissionOverride);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(userPermissionOverride.Adapt<AssignToUserPermissionResponse>());
    }
}
