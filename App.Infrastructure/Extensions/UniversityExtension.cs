using App.Core.Consts;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace App.Infrastructure.Extensions;

public static class UniversityExtension
{
    public static int? GetUniversityId(this ClaimsPrincipal user)
    {

        var universityId = user.FindFirst(ClaimsConstants.universityId)?.Value;

        int.TryParse(universityId, out var id);

        return id == 0 ? null : id;
    }
}
