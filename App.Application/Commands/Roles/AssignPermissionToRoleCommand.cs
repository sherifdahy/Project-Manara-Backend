using App.Application.Contracts.Responses.Roles;

namespace App.Application.Commands.Roles;

public record AssignPermissionToRoleCommand : IRequest<Result<AssignToRolePermissionResponse>>
{
    public int RoleId { get; set; }
    public int FacultyId { get; set; }
    public string ClaimValue { get; set; } = string.Empty;
    public bool IsAllowed { get; set; } = true;
}
