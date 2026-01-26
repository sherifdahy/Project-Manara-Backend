using App.Core.Entities.Identity;
using App.Core.Interfaces;
using App.Infrastructure.Abstractions.Consts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SA.Accountring.Core.Entities.Interfaces;
using System.Security.Cryptography;
namespace App.Services;

public class AuthenticationService(UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork,RoleManager<ApplicationRole> roleManager) : IAuthenticationService
{
    private readonly int _refreshTokenExpirationDays = 14;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

    public  (string refreshToken,DateTime refreshTokenExpiration) AddRefreshToken(ApplicationUser user)
    {
        var refreshToken = GenerateRefreshToken();

        var refreshTokenExpiration = GetRefreshTokenExpiration();

        user.RefreshTokens.Add(
            new RefreshToken
            {
                Token = refreshToken,
                ExpireOn = refreshTokenExpiration
            }
            );

        return (refreshToken,refreshTokenExpiration);
    }
    public async Task<(IEnumerable<string> roles, IEnumerable<string> permissions)> GetUserRolesAndPermissions(ApplicationUser user, CancellationToken cancellationToken)
    {

        var roles = await _userManager.GetRolesAsync(user);

        var permissions = new List<string>();

        foreach (var roleName in roles)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role is null) continue;

            var rolePermissions = await _roleManager.GetClaimsAsync(role);

            permissions.AddRange(rolePermissions.Where(x => x.Type == Permissions.Type).Select(x => x.Value).Distinct());
        }

        foreach (var over in user.PermissionOverrides)
        {
            var permissionValue = over.RoleClaim.ClaimValue;

            if (over.IsAllowed)
            {
                permissions.Add(permissionValue!);
            }
            else
            {
                permissions.Remove(permissionValue!);
            }
        }

        return (roles, permissions);


    }
    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
    private DateTime GetRefreshTokenExpiration()
    {
        return DateTime.UtcNow.AddDays(_refreshTokenExpirationDays);
    }
}
