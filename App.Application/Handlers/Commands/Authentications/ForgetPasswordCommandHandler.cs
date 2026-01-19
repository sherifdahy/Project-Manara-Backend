using App.Infrastructure.Helpers;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System.Text;

namespace App.Application.Handlers.Commands.Authentications;

public class ForgetPasswordCommandHandler (UserManager<ApplicationUser> userManager
    ,ILogger<ForgetPasswordCommandHandler> logger
    ,IHttpContextAccessor httpContextAccessor
    ,IEmailSender emailSender) : IRequestHandler<ForgetPasswordCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ILogger<ForgetPasswordCommandHandler> _logger = logger;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IEmailSender _emailSender = emailSender;

    public async Task<Result> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return Result.Success();

        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        _logger.LogInformation("Reset code: {code}", code);

        await SendResetPasswordEmail(user, code);

        return Result.Success();
    }

    private async Task SendResetPasswordEmail(ApplicationUser user, string code)
    {
        var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

        var emailBody = EmailBodyBuilder.GenerateEmailBody("ForgetPassword",
            templateModel: new Dictionary<string, string>
            {
                { "{{name}}", user.FirstName },
                { "{{action_url}}", $"{origin}/auth/new-password?email={user.Email}&code={code}" }//This will Be the route of the frontend 
            }
        );

        BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "✅ Manaraa: Change Password", emailBody));

        await Task.CompletedTask;
    }
}
