using App.Application.Commands.Roles;
using App.Application.Contracts.Responses.Roles;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;

namespace App.Application.Handlers.Commands.Roles;

public class ToggleStatusPermissionRoleCommandHandler(IUnitOfWork unitOfWork
    ,PermissionErrors permissionErrors
    ,FacultyErrors facultyErrors
    ,IAuthenticationService authenticationService) : IRequestHandler<ToggleStatusPermissionRoleCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly PermissionErrors _permissionErrors = permissionErrors;
    private readonly FacultyErrors _facultyErrors = facultyErrors;
    private readonly IAuthenticationService _authenticationService = authenticationService;

    public async Task<Result> Handle(ToggleStatusPermissionRoleCommand request, CancellationToken cancellationToken)
    {

        var allowedPermissions = Permissions.GetAllPermissions();

        if (!allowedPermissions.Contains(request.ClaimValue))
            return Result.Failure(_permissionErrors.InvalidPermissions);

        var roleClaimOverride = await _unitOfWork.RoleClaimOverrides
            .FindAsync(rc => rc.RoleId == request.RoleId && rc.ClaimValue == request.ClaimValue && rc.FacultyId == request.FacultyId);

        if (roleClaimOverride == null)
            return Result.Failure(_permissionErrors.OverridePermissionNotFound);


        //roleClaimOverride.IsAllowed = !roleClaimOverride.IsAllowed;

        _unitOfWork.RoleClaimOverrides.Update(roleClaimOverride);
        await _unitOfWork.SaveAsync();

        return Result.Success();
    }
}
