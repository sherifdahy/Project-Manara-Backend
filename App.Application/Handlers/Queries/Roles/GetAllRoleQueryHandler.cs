using App.Application.Contracts.Responses.Roles;
using App.Application.Queries.Roles;
using App.Infrastructure.Presistance.Data;


namespace App.Application.Handlers.Queries.Roles;

public class GetAllRoleQueryHandler : IRequestHandler<GetAllRolesQuery, Result<List<RoleResponse>>>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _context;

    public GetAllRoleQueryHandler(RoleManager<ApplicationRole> roleManager,IUnitOfWork unitOfWork,ApplicationDbContext context)
    {
        _roleManager = roleManager;
        this._context = context;
    }

    public async Task<Result<List<RoleResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleManager
            .Roles
            .Where(x => !x.IsDefualt && (!x.IsDeleted || (request.IncludeDisabled.HasValue && request.IncludeDisabled.Value)))
            .Select(r => new RoleResponse(
                r.Id,
                r.Name!,
                r.Code,
                r.Description,
                r.IsDeleted,
                _context.RoleClaims.Count(rc => rc.RoleId == r.Id) 
            ))
            .ToListAsync();


        return Result.Success(roles);
    }
}
