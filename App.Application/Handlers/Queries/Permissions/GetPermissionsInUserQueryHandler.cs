using App.Application.Contracts.Responses.Permissions;
using App.Application.Queries.Permissions;

namespace App.Application.Handlers.Queries.Permissions;

public class GetPermissionsInUserQueryHandler(UserManager<ApplicationUser> userManager
    ,UserErrors userErrors
    , IAuthenticationService authenticationService
    ,IUnitOfWork unitOfWork) : IRequestHandler<GetPermissionsInUserQuery, Result<GetPermissionsInUserResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly UserErrors _userErrors = userErrors;
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    public async Task<Result<GetPermissionsInUserResponse>> Handle(GetPermissionsInUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());

        if(user == null)
            return Result.Failure<GetPermissionsInUserResponse>(_userErrors.NotFound);


        var (roles,defaultPermissions) = await _authenticationService.GetUserOverrideRolesAndPermissions(user, cancellationToken, false);

        var overridePermissions = await _unitOfWork.UserClaimOverrides
            .FindAllAsync(rco => rco.ApplicationUserId == request.UserId, cancellationToken);

        var response = new GetPermissionsInUserResponse
        (
            defaultPermissions,
            overridePermissions.Select(x => x.ClaimValue)
        );

        return Result.Success(response);
    }
}
