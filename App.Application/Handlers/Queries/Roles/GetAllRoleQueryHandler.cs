using App.Application.Contracts.Responses.Roles;
using App.Application.Queries.Roles;
using App.Core.Consts;
using App.Core.Extensions;
using App.Infrastructure.Presistance.Data;


namespace App.Application.Handlers.Queries.Roles;

public class GetAllRoleQueryHandler : IRequestHandler<GetAllRolesQuery, Result<List<RoleResponse>>>
{
    
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public GetAllRoleQueryHandler(IHttpContextAccessor httpContextAccessor,UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Result<List<RoleResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext!.User.GetUserId().ToString());

        var systemRoles = await _roleManager.Roles.Where(r => !r.IsDefualt && (!r.IsDeleted || (request.IncludeDisabled.HasValue && request.IncludeDisabled.Value))).Include(x => x.Role).ToListAsync(cancellationToken);

        var userRoles = await _userManager.GetRolesAsync(user!);

        var applicationRoles = new List<ApplicationRole>();

        if (userRoles.FirstOrDefault(x => x.Contains(RolesConstants.SystemAdmin)) is not null)
        {
            applicationRoles.AddRange(systemRoles);
        }
        else if (userRoles.FirstOrDefault(x => x.Contains(RolesConstants.SystemAdmin)) is string role)
        {
            var highestRole = systemRoles.First(x => x.Name!.Contains(RolesConstants.SystemAdmin));

            applicationRoles.Add(highestRole);

            while (highestRole.Role != null)
            {
                applicationRoles.Add(highestRole.Role);
                highestRole = highestRole.Role;
            }
        }

        //var roles = await _roleManager
        //    .Roles
        //    .Where(x => !x.IsDefualt && (!x.IsDeleted || (request.IncludeDisabled.HasValue && request.IncludeDisabled.Value)))
        //    .Select(r => new RoleResponse(
        //        r.Id,
        //        r.Name!,
        //        r.Code,
        //        r.Description,
        //        r.IsDeleted,
        //        _context.RoleClaims.Count(rc => rc.RoleId == r.Id)
        //    ))
        //    .ToListAsync();

        return Result.Success(applicationRoles.Adapt<List<RoleResponse>>());
    }
}
