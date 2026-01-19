

using Microsoft.AspNetCore.Identity;
using System.Text;

namespace App.Application.Handlers.Commands.Authentications;

public class ResetPasswordCommandHandler (UserManager<ApplicationUser> userManager,AuthenticationErrors errors) : IRequestHandler<ResetPasswordCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly AuthenticationErrors _errors = errors;

    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return Result.Failure(_errors.InvalidCode);

        IdentityResult result;

        try
        {
            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));//Decode the code
            result = await _userManager.ResetPasswordAsync(user, code, request.NewPassword);//reset password

        }
        catch (FormatException)
        {
            result = IdentityResult.Failed(_userManager.ErrorDescriber.InvalidToken());
        }

        if (result.Succeeded)
            return Result.Success();

        var error = result.Errors.First();

        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status401Unauthorized));
    }
}
