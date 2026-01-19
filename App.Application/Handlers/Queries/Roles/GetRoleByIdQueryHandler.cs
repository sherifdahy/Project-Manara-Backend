using App.Application.Abstractions;
using App.Application.Errors;
using App.Application.Queries.Roles;
using App.Application.Responses.Role;
using App.Core.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace App.Application.Handlers.Queries.Roles;

public class GetRoleByIdQueryHandler(RoleManager<ApplicationRole> _roleManager,RoleErrors errors) : IRequestHandler<GetRoleByIdCommand, Result<RoleDetailResponse>>
{
    private readonly RoleErrors _errors = errors;

    public async Task<Result<RoleDetailResponse>> Handle(GetRoleByIdCommand request, CancellationToken cancellationToken)
    {
        if (await _roleManager.FindByIdAsync(request.Id.ToString()) is not { } role)
            return Result.Failure<RoleDetailResponse>(_errors.NotFound);

        var permissions = await _roleManager.GetClaimsAsync(role);

        var response = new RoleDetailResponse
        (
            role.Id,
            role.Name!,
            role.IsDeleted,
            permissions.Select(c => c.Value)
        );

        return Result.Success(response);
    }
}
