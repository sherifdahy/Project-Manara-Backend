using App.Core.Entities.Identity;
using App.Core.Extensions;
using App.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SA.Accountring.Core.Entities.Interfaces;
using System.Security.Claims;

namespace App.Services;

public class ScopeService(UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork) : IScopeService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> IsUserHasAccessToScope(ClaimsPrincipal user, string requestScopeName)
    {

        var userEntity = await _userManager.FindByIdAsync(user.GetUserId().ToString());

        if (userEntity == null)
            return false;

        var roleNames = await _userManager.GetRolesAsync(userEntity);


        var roles = await _unitOfWork.Roles.FindAllAsync(r => roleNames.Contains(r.Name!), r=>r.Include(d=>d.Scope),CancellationToken.None);

        foreach (var role in roles)
        {
            var requestScopeEntity = await _unitOfWork.Scopes.FindAsync(s => s.Name == requestScopeName);
                       
            while (requestScopeEntity!=null)
            {
                if (role.Scope.Name == requestScopeEntity.Name)
                    return true;

                if (role.Scope.ParentScopeId == requestScopeEntity.ParentScopeId)
                    return true;

                requestScopeEntity = await _unitOfWork.Scopes.FindAsync(s => s.Id == requestScopeEntity.ParentScopeId);
            }
        }

        return false;
    }
}
