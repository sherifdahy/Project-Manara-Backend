namespace App.Application.Commands.Authentications;

public record RevokeRefreshTokenCommand : IRequest<Result>
{
    public const string Route = "revoke-refresh-token";
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
