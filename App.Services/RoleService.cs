
using App.Core.Entities.Identity;
using App.Core.Extensions;
using App.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using SA.Accountring.Core.Entities.Interfaces;
using System.Security.Claims;

namespace App.Services;

public class RoleService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork) : IRoleService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    public async Task<bool> IsUserHasAccessToRole(ClaimsPrincipal user, int requestRoleId)
    {


        var userEntity = await _userManager.FindByIdAsync(user.GetUserId().ToString());

        if (userEntity == null)
            return false;

        var roleNames = await _userManager.GetRolesAsync(userEntity);


        var roles = await _unitOfWork.Roles.FindAllAsync(r => roleNames.Contains(r.Name!));


        foreach (var role in roles)
        {
            var currentRole = role;

            while (currentRole != null)
            {
                var nextRoleId = currentRole.RoleId;

                if (nextRoleId == requestRoleId)
                    return true;

                currentRole = await _unitOfWork.Roles.FindAsync(r => r.Id == nextRoleId);
            }
        }

        return false;
    }
}