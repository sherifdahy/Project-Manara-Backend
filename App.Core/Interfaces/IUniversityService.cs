

using System.Security.Claims;

namespace App.Core.Interfaces;

public interface IUniversityService
{
    Task<bool> IsUserHasAccessToUniversity(ClaimsPrincipal user, int requestUniversityId);
}
