using System.Security.Claims;

namespace App.Core.Interfaces;

public interface IUserService
{
    Task<bool> IsUserHasAccessToUser(ClaimsPrincipal user, int requestUserId);
}
