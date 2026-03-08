
using System.Security.Claims;

namespace App.Core.Interfaces;

public interface ISubjectService
{
    public  Task<bool> IsUserHasAccessToSubject(ClaimsPrincipal user, int requestSubjectId);
}
