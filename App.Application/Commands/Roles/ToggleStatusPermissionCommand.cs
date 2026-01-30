namespace App.Application.Commands.Roles;

public record ToggleStatusPermissionCommand : IRequest<Result>
{
    public int UserId { get; set; }
    public string ClaimValue { get; set; } = string.Empty;

}
