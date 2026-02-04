using App.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace App.Core.Interfaces;

public interface IAuthenticationService
{
    Task<(IEnumerable<string> roles, IEnumerable<string> permissions)> GetUserRolesAndPermissions(ApplicationUser user, CancellationToken cancellationToken);
    (string refreshToken, DateTime refreshTokenExpiration) AddRefreshToken(ApplicationUser user);
    bool IsUserHasAccessToUniversity(ClaimsPrincipal user, int requestUniversityId);
    bool IsUserHasAccessToFaculty(ClaimsPrincipal user, int requestFacultyId);
}
