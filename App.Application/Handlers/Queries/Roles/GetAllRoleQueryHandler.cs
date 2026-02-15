using App.Application.Contracts.Responses.Roles;
using App.Application.Queries.Roles;
using App.Core.Extensions;
using App.Infrastructure.Presistance.Data;
using System.Threading;
using System.Threading.Tasks;


namespace App.Application.Handlers.Queries.Roles;

public class GetAllRoleQueryHandler : IRequestHandler<GetAllRolesQuery, Result<List<RoleResponse>>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserErrors _userErrors;

    public GetAllRoleQueryHandler(UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager
        ,IHttpContextAccessor httpContextAccessor
        ,IUnitOfWork unitOfWork
        ,UserErrors userErrors
        ,IRoleService roleService)
    {
        this._userManager = userManager;
        _roleManager = roleManager;
        this._httpContextAccessor = httpContextAccessor;
        this._unitOfWork = unitOfWork;
        this._userErrors = userErrors;
    }

    public async Task<Result<List<RoleResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {

        var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext!.User.GetUserId().ToString());

        if (user == null)
            return Result.Failure<List<RoleResponse>>(_userErrors.NotFound);

        var userRoles = await _userManager.GetRolesAsync(user);

        var userRolesEntities = await _unitOfWork.Roles
            .FindAllAsync(x => userRoles.Contains(x.Name!),null,cancellationToken);



        var mainScope = userRolesEntities.First().ScopeId;

        var firstRolesInScope = await _unitOfWork.Roles
            .FindAllAsync(x => x.ScopeId ==mainScope && x.ParentRoleId==null,null,cancellationToken);

        var firstRoleInScope = firstRolesInScope.FirstOrDefault();

        var childRolesInScope = await GetRoleChildsInScope(firstRoleInScope!, cancellationToken);

        var higherUserRole = GetHigherUserRole(userRoles.ToList(),firstRoleInScope!,childRolesInScope);

        var rolesEntities = await GetHierarchyRoles(higherUserRole, request,mainScope, cancellationToken); 

        var response = new List<RoleResponse>();

        foreach (var roleEntity in rolesEntities)
        {
            response.Add(new RoleResponse(
                roleEntity.Id,
                roleEntity.Name!,
                roleEntity.Code,
                roleEntity.Description,
                roleEntity.IsDeleted,
                await _unitOfWork.RoleClaims.CountAsync(rc => rc.RoleId == roleEntity.Id, cancellationToken)
            ));
        }

        return Result.Success(response);
    }

    private async Task<List<ApplicationRole>> GetHierarchyRoles(ApplicationRole higherUserRole, GetAllRolesQuery request , int mainScope, CancellationToken cancellationToken)
    {

        List<ApplicationRole> outRoles = new List<ApplicationRole>();

        outRoles.Add(higherUserRole);

        var startScopeEntity = await _unitOfWork.Scopes.FindAsync(x => x.ParentScopeId == mainScope);


        var childesInScope = await GetRoleChildsInScope(higherUserRole,cancellationToken);
        outRoles.AddRange(childesInScope);


        var brothersInScopes = await _unitOfWork.Roles
            .FindAllAsync(x => x.Name != higherUserRole.Name 
                    && x.ParentRoleId == higherUserRole.ParentRoleId && x.ScopeId == higherUserRole.ScopeId, null, cancellationToken);

        outRoles.AddRange(brothersInScopes);

        while (startScopeEntity != null)
        {
            var startRoles = await _unitOfWork.Roles
                .FindAllAsync(x =>
                        x.ScopeId == startScopeEntity!.Id && x.ParentRoleId == null, null, cancellationToken);

            outRoles.AddRange(startRoles!);

            var childRoles = await GetRoleChildsInScope(startRoles.First()!, cancellationToken);
            outRoles.AddRange(childRoles);

            startScopeEntity = await _unitOfWork.Scopes.FindAsync(x => x.ParentScopeId == startScopeEntity!.Id);
        }

        return outRoles;
    }

    private async Task<List<ApplicationRole>> GetRoleChildsInScope(ApplicationRole firstRoleInScope,CancellationToken cancellationToken)
    {
        List<ApplicationRole> allRolesInMainScope = new List<ApplicationRole>();

        while (firstRoleInScope != null)
        {
            var childRoles = await _unitOfWork.Roles
                .FindAllAsync(x => x.ScopeId == firstRoleInScope.ScopeId && x.ParentRoleId == firstRoleInScope.Id, null, cancellationToken);

            if (childRoles.Any())
                allRolesInMainScope.AddRange(childRoles);

            firstRoleInScope = childRoles.FirstOrDefault()!;
        }

        return allRolesInMainScope;
    }

    private  ApplicationRole GetHigherUserRole(List<string> userRoles,ApplicationRole firstRoleInScope
        ,List<ApplicationRole> childRolesInScope)
    {
        var higherUserRole = new ApplicationRole();

        if (userRoles.Contains(firstRoleInScope!.Name!))
        {
            higherUserRole = firstRoleInScope;
            return higherUserRole;
        }

        foreach (var childRoleInScope in childRolesInScope)
        {
            if (userRoles.Contains(childRoleInScope!.Name!))
            {
                higherUserRole = childRoleInScope;
                return higherUserRole;
            }
        }
        return higherUserRole;
    }


}