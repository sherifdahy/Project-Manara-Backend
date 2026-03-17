

using System.Security.Claims;

namespace App.Core.Interfaces;

public interface IYearService
{
    Task<bool> IsUserHasAccessToYear(ClaimsPrincipal user, int requestYearId);
}
