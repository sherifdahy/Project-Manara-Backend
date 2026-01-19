using App.Core.Entities.Identity;
using App.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SA.Accountring.Core.Entities.Interfaces;
using System.Security.Cryptography;
namespace App.Services;

public class AuthenticationService(UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork) : IAuthenticationService
{
    private readonly int _refreshTokenExpirationDays = 14;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

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
        var userRoles = await _userManager.GetRolesAsync(user);

        var userPermissions = await
            (
                from r in _unitOfWork.Roles.Query()
                join p in _unitOfWork.RoleClaims.Query()
                on r.Id equals p.RoleId
                where userRoles.Contains(r.Name!)
                select p.ClaimValue
            )
            .Distinct()
            .ToListAsync(cancellationToken);

        return (userRoles, userPermissions);

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
