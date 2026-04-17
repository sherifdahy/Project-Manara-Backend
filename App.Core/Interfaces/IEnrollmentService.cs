using System.Security.Claims;

namespace App.Core.Interfaces;

public interface IEnrollmentService
{
    Task<bool> IsUserHasAccessToEnrollment(ClaimsPrincipal user, int requestEnrollmentId);
}
