using App.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace App.Core.Interfaces;

public interface IAuthenticationService
{
    Task<(IEnumerable<string> roles, IEnumerable<string> permissions)> 
        GetUserOverrideRolesAndPermissions(ApplicationUser user, CancellationToken cancellationToken,bool includeUserOverride = true);
    (string refreshToken, DateTime refreshTokenExpiration) AddRefreshToken(ApplicationUser user);
}
