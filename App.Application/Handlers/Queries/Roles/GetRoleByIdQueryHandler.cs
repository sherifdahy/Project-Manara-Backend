using App.Application.Abstractions;
using App.Application.Contracts.Responses.Roles;
using App.Application.Queries.Roles;

namespace App.Application.Handlers.Queries.Roles;

public class GetRoleByIdQueryHandler(
    RoleManager<ApplicationRole> _roleManager
    ,RoleErrors errors
    ,IUnitOfWork unitOfWork) : IRequestHandler<GetRoleByIdQuery, Result<RoleDetailResponse>>
{
    private readonly RoleErrors _errors = errors;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<RoleDetailResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        if (await _roleManager.FindByIdAsync(request.Id.ToString()) is not { } role)
            return Result.Failure<RoleDetailResponse>(_errors.NotFound);

        var permissions = await _roleManager.GetClaimsAsync(role);

        var numberOfUsers = await _unitOfWork.UserRoles.CountAsync(ur=>ur.RoleId==role.Id);

        var response = new RoleDetailResponse
        (
            role.Id,
            role.Name!,
            role.Code,
            role.Description,
            role.IsDeleted,
            numberOfUsers,
            permissions.Select(c => c.Value),
            role.RoleId != null ? (await _roleManager.FindByIdAsync(role.RoleId.ToString()!)).Adapt<RoleResponse>() : null
        );

        return Result.Success(response);
    }
}
