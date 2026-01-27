

using App.Application.Commands.Roles;
using App.Application.Errors;
using App.Application.Responses.Role;
using App.Core.Entities.Relations;
using App.Infrastructure.Abstractions.Consts;
using Microsoft.AspNetCore.Identity;

namespace App.Application.Handlers.Commands.Roles;

public class AssignPermissionToUserHandler(UserManager<ApplicationUser> userManager
    ,AuthenticationErrors authenticationErrors
    ,PermissionErrors permissionErrors
    ,IUnitOfWork unitOfWork
    ,IAuthenticationService authenticationService) : IRequestHandler<AssignPermissionToUserCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly AuthenticationErrors _authenticationErrors = authenticationErrors;
    private readonly PermissionErrors _permissionErrors = permissionErrors;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthenticationService _authenticationService = authenticationService;

    public async Task<Result> Handle(AssignPermissionToUserCommand request, CancellationToken cancellationToken)
    {    

        if (await _userManager.FindByIdAsync(request.UserId.ToString()) is not { } user)
            return Result.Failure(_authenticationErrors.NotFound);

        var allowedPermissions = Permissions.GetAllPermissions();

        if (!allowedPermissions.Contains(request.RoleClaim))
            return Result.Failure<RoleDetailResponse>(_permissionErrors.InvalidPermissions);

        var roleClaim = await _unitOfWork.RoleClaims.FindAsync(x=>(x.ClaimValue==request.RoleClaim) && (x.RoleId==request.RoleId));

        if (roleClaim == null)
            return Result.Failure(_permissionErrors.InvalidPermissions);

        var (userRoles, userPermissions) = await _authenticationService.GetUserRolesAndPermissions(user,cancellationToken);

        if (userPermissions.Contains(roleClaim.ClaimValue) && request.IsAllowed == true)
            return Result.Failure(_permissionErrors.DuplicatedPermissionForUser);

        var userPermissionOverride = new UserPermissionOverride
        {
            ApplicationUserId = request.UserId,
            RoleClaimId = roleClaim.Id,
            IsAllowed = request.IsAllowed
        };

        await _unitOfWork.UserPermissionOverrides.AddAsync(userPermissionOverride);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
