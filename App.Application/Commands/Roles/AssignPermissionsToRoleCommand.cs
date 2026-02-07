using App.Application.Contracts.Responses.Roles;
using System.Security.Claims;

namespace App.Application.Commands.Roles;

public record AssignPermissionsToRoleCommand : IRequest<Result>
{
    public int RoleId { get; set; }
    public int FacultyId { get; set; }
    public ClaimsPrincipal User { get; set; } = default!;
    public List<string> ClaimValues { get; set; } = default!;

}
