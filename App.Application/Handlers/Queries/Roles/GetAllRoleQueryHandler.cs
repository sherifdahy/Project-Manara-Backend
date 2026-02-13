using App.Application.Contracts.Responses.Roles;
using App.Application.Queries.Roles;
using App.Core.Extensions;
using App.Infrastructure.Presistance.Data;


namespace App.Application.Handlers.Queries.Roles;

public class GetAllRoleQueryHandler : IRequestHandler<GetAllRolesQuery, Result<List<RoleResponse>>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserErrors _userErrors;

    public GetAllRoleQueryHandler(UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager
        , ApplicationDbContext context
        ,IHttpContextAccessor httpContextAccessor
        ,IUnitOfWork unitOfWork
        ,UserErrors userErrors)
    {
        this._userManager = userManager;
        _roleManager = roleManager;
        this._context = context;
        this._httpContextAccessor = httpContextAccessor;
        this._unitOfWork = unitOfWork;
        this._userErrors = userErrors;
    }

    public async Task<Result<List<RoleResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        //TODO Complete And Task
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            return Result.Failure<List<RoleResponse>>(_userErrors.NotFound);

        var userRoles = await _userManager.GetRolesAsync(user);

        var roles = await _roleManager
            .Roles
            .Where(x => !x.IsDefault && (!x.IsDeleted || (request.IncludeDisabled.HasValue && request.IncludeDisabled.Value)))
            .Select(r => new RoleResponse(
                r.Id,
                r.Name!,
                r.Code,
                r.Description,
                r.IsDeleted,
                _context.RoleClaims.Count(rc => rc.RoleId == r.Id)
            ))
            .ToListAsync();

        //var roles = await GetRolesHirechery(userRoles.ToList());

        return Result.Success(roles);
    }

    //private async Task<List<RoleResponse>> GetRolesHirechery(List<string> userRoles)
    //{
    //    //First Must I Find The BiggerRole
    //    //Then return all the role under it
    //}

    //private string GetHigherRole(List<string> roles)
    //{
    //    var higherRole = string.Empty;

    //    foreach (var role in roles) 
    //    {
            
    //    }
        

    //    return  string.Empty;
    //}
}