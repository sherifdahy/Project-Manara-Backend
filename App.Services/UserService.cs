using App.Core.Interfaces;
using SA.Accountring.Core.Entities.Interfaces;
using System.Security.Claims;

namespace App.Services;

public class UserService(IUnitOfWork unitOfWork,IRoleService roleService) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRoleService _roleService = roleService;

    public async Task<bool> IsUserHasAccessToUser(ClaimsPrincipal user,int requestUserId)
    {

        var requestRoles = await _unitOfWork.UserRoles
            .FindAllAsync(ur => ur.UserId == requestUserId);

        foreach (var requestRole in requestRoles) 
        {
            if (await _roleService.IsUserHasAccessToRole(user, requestRole.RoleId))
                return true;
        }

        return false;   

    }
}
