

using App.Application.Contracts.Responses.Roles;

namespace App.Application.Commands.Roles;

public record AssignPermissionsToUserCommand :IRequest<Result>
{
    public int UserId { get; set; } 
    public List<string> ClaimValues { get; set; }=default!;
    //public bool IsAllowed { get; set; } = true;
}
