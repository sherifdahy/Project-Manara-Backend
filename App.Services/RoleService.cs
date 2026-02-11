
using App.Core.Entities.Identity;
using App.Core.Extensions;
using App.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using SA.Accountring.Core.Entities.Interfaces;
using System.Security.Claims;

namespace App.Services;

public class RoleService(UserManager<ApplicationUser> userManager
    , IUnitOfWork unitOfWork,IScopeService scopeService) : IRoleService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IScopeService _scopeService = scopeService;

    public async Task<bool> IsUserHasAccessToRole(ClaimsPrincipal user, int requestRoleId)
    {

        var userEntity = await _userManager.FindByIdAsync(user.GetUserId().ToString());

        if (userEntity == null)
            return false;

        var roleNames = await _userManager.GetRolesAsync(userEntity);


        var roleEntities = await _unitOfWork.Roles.FindAllAsync(r => roleNames.Contains(r.Name!));


        foreach (var roleEntity in roleEntities)
        {
            var requestRoleEntity = await _unitOfWork.Roles.FindAsync(r=>r.Id==requestRoleId);

            while (requestRoleEntity != null)
            {
                if (roleEntity.Id == requestRoleEntity.Id)
                    return true;

                if (roleEntity.ParentRoleId == requestRoleEntity.ParentRoleId)
                    return true;

                requestRoleEntity = await _unitOfWork.Roles.FindAsync(s => s.Id == requestRoleEntity.ParentRoleId);
            }
        }

        return false;
    }


}