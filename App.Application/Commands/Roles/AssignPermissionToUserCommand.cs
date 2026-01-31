

using App.Application.Contracts.Responses.Roles;

namespace App.Application.Commands.Roles;

public record AssignPermissionToUserCommand :IRequest<Result<AssignToUserPermissionResponse>>
{
    public int UserId { get; set; } 
    public string ClaimValue { get; set; }=string.Empty;
    public bool IsAllowed { get; set; } = true;
}
