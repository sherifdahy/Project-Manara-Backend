using App.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Interfaces;

public interface IAuthenticationService
{
    Task<(IEnumerable<string> roles, IEnumerable<string> permissions)> GetUserRolesAndPermissions(ApplicationUser user, CancellationToken cancellationToken);
    (string refreshToken, DateTime refreshTokenExpiration) AddRefreshToken(ApplicationUser user);
}
