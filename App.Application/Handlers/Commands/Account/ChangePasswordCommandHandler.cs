using App.Application.Commands.Account;
using App.Core.Extensions;

namespace App.Application.Handlers.Commands.Account;

public class ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager,IHttpContextAccessor httpContextAccessor) : IRequestHandler<ChangePasswordCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext!.User.GetUserId().ToString());

        var result = await _userManager.ChangePasswordAsync(user!, request.CurrentPassword, request.NewPassword);

        if (result.Succeeded)
            return Result.Success();

        var error = result.Errors.First();

        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
}
