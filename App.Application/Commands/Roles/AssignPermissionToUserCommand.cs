

using App.Core.Enums;

namespace App.Application.Commands.Roles;

public class AssignPermissionToUserCommand :IRequest<Result>
{
    public int UserId { get; set; } 
    public int RoleId { get; set; } 
    public string RoleClaim { get; set; }=string.Empty;
    public bool IsAllowed { get; set; } 
}
