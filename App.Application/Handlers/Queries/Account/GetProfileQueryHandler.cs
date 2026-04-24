using App.Application.Contracts.Responses.Account;
using App.Application.Queries.Account;
using App.Core.Extensions;

namespace App.Application.Handlers.Queries.Account;

public class GetProfileQueryHandler(UserManager<ApplicationUser> userManager,IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetProfileQuery, Result<ProfileResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<ProfileResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext!.User.GetUserId().ToString());

        var response = user.Adapt<ProfileResponse>();

        return Result.Success(response);
    }
}
