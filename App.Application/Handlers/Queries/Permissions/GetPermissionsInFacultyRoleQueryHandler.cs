using App.Application.Contracts.Responses.Roles;
using App.Application.Queries.Roles;

namespace App.Application.Handlers.Queries.Permissions;

public class GetPermissionsInFacultyRoleQueryHandler(RoleManager<ApplicationRole> roleManager
    ,RoleErrors roleErrors
    ,IUnitOfWork unitOfWork) : IRequestHandler<GetPermissionsInFacultyRoleQuery, Result<GetPermissionsInFacultyRoleResponse>>
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly RoleErrors _roleErrors = roleErrors;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<GetPermissionsInFacultyRoleResponse>> Handle(GetPermissionsInFacultyRoleQuery request, CancellationToken cancellationToken)
    {
        if (await _roleManager.FindByIdAsync(request.RoleId.ToString()) is not { } role)
            return Result.Failure<GetPermissionsInFacultyRoleResponse>(_roleErrors.NotFound);

        var defaultPermissions = await _roleManager.GetClaimsAsync(role);

        var overridePermissions = await _unitOfWork.RoleClaimOverrides
            .FindAllAsync(rco=>rco.RoleId == request.RoleId && rco.FacultyId==request.FacultyId,cancellationToken);

        var response = new GetPermissionsInFacultyRoleResponse
        (
            role.Id,
            role.Name!,
            role.Code,
            role.Description,
            role.IsDeleted,
            defaultPermissions.Select(x=>x.Value),
            overridePermissions.Select(x => x.ClaimValue)
        );

        return Result.Success(response);

    }
}
