using App.Core.Consts;
using System.Security.Claims;

namespace App.Core.Extensions;

public static class FacultyExtension
{
    public static int? GetFacultyId(this ClaimsPrincipal user)
    {

        var facultyId = user.FindFirst(ClaimsConstants.facultyId)?.Value;

        int.TryParse(facultyId, out var id);

        return id == 0 ? null : id;
    }
}
