using App.Application.Queries.Roles;
using App.Application.Responses.Role;


namespace App.Application.Handlers.Queries.Roles;

public class GetAllRoleQueryHandler : IRequestHandler<GetAllRolesCommand, Result<List<RoleResponse>>>
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public GetAllRoleQueryHandler(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Result<List<RoleResponse>>> Handle(GetAllRolesCommand request, CancellationToken cancellationToken)
    {
        var roles = await _roleManager
                                .Roles
                                .Where(x=> !x.IsDefualt && (!x.IsDeleted || (request.IncludeDisabled.HasValue && request.IncludeDisabled.Value)))
                                .ProjectToType<RoleResponse>().ToListAsync(cancellationToken);

        return Result.Success(roles);
    }
}
