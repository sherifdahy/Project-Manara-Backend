using App.Application.Contracts.Responses.Permissions;
using App.Application.Contracts.Responses.Roles;

namespace App.Application.Queries.Permissions;

public record GetPermissionsInUserQuery : IRequest<Result<GetPermissionsInUserResponse>>
{
    public int UserId { get; set; }
    public GetPermissionsInUserQuery(int userId)
    {
        UserId = userId;
    }
}