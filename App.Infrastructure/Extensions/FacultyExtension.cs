using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace App.Infrastructure.Extensions;

public static class FacultyExtension
{
    public static int GetFacultyId(this ClaimsPrincipal user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var facultyId = user.FindFirst("facultyId")?.Value;

        int.TryParse(facultyId, out var id);

        return id;
    }
}
