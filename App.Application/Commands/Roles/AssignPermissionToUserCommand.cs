

using App.Application.Responses.Roles;
using App.Core.Enums;

namespace App.Application.Commands.Roles;

public record AssignPermissionToUserCommand :IRequest<Result<AssignToUserPermissionResponse>>
{
    public int UserId { get; set; } 
    public string ClaimValue { get; set; }=string.Empty;
    public bool IsAllowed { get; set; } = true;
}
