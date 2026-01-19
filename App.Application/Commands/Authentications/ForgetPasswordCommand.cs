
namespace App.Application.Commands.Authentications;

public record ForgetPasswordCommand : IRequest<Result>
{
    public const string Route = "forget-password";
    public string Email { get; set; } = string.Empty;
}
