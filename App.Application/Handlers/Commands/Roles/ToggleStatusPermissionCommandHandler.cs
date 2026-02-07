
using App.Application.Commands.Roles;
using App.Application.Errors;
using App.Infrastructure.Abstractions.Consts;
using App.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;

namespace App.Application.Handlers.Commands.Roles;

public class ToggleStatusPermissionCommandHandler(UserManager<ApplicationUser> userManager
    , AuthenticationErrors authenticationErrors
    , PermissionErrors permissionErrors
    , IUnitOfWork unitOfWork
    , IAuthenticationService authenticationService) : IRequestHandler<ToggleStatusPermissionCommand, Result>
{

    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly AuthenticationErrors _authenticationErrors = authenticationErrors;
    private readonly PermissionErrors _permissionErrors = permissionErrors;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthenticationService _authenticationService = authenticationService;
    public async Task<Result> Handle(ToggleStatusPermissionCommand request, CancellationToken cancellationToken)
    {

        if (await _userManager.FindByIdAsync(request.UserId.ToString()) is not { })
            return Result.Failure(_authenticationErrors.NotFound);

        var allowedPermissions = Permissions.GetAllPermissions();

        if (!allowedPermissions.Contains(request.ClaimValue))
            return Result.Failure(_permissionErrors.InvalidPermissions);


        var overridePermission = await _unitOfWork.UserClaimOverrides
            .FindAsync(x => x.ClaimValue == request.ClaimValue && x.ApplicationUserId == request.UserId);


        if(overridePermission==null)
            return Result.Failure(_permissionErrors.OverridePermissionNotFound);

        //overridePermission.IsAllowed = !overridePermission.IsAllowed;

         _unitOfWork.UserClaimOverrides.Update(overridePermission);
        await _unitOfWork.SaveAsync(cancellationToken);



        return Result.Success();
    }
}
