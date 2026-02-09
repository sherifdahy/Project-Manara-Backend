

using System.Security.Claims;

namespace App.Core.Interfaces;

public interface IRoleService
{
    Task<bool> IsUserHasAccessToRole(ClaimsPrincipal user, int requestRoleId);
}

