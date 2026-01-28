namespace App.Application.Commands.Roles;

public class ToggleStatusPermissionCommand : IRequest<Result>
{
    public int UserId { get; set; }
    public string ClaimValue { get; set; } = string.Empty;

}
