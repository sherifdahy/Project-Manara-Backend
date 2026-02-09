

using System.Security.Claims;

namespace App.Core.Interfaces;

public interface IFacultyService
{
    Task<bool> IsUserHasAccessToFaculty(ClaimsPrincipal user, int requestFacultyId);
}
