using App.Core.Entities.Identity;
using App.Core.Entities.Universities;
using App.Core.Extensions;
using App.Core.Interfaces;
using App.Infrastructure.Abstractions.Consts;
using Microsoft.AspNetCore.Identity;
using SA.Accountring.Core.Entities.Interfaces;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
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
            
           
            var roleClaimOverrides = await _unitOfWork.RoleClaimOverrides
                .FindAllAsync(rc=>rc.RoleId == role.Id && rc.FacultyId==user.FacultyId);

            foreach (var roleClaimOverride in roleClaimOverrides)
            {
                if(roleClaimOverride is null) continue;

                if (roleClaimOverride.IsAllowed)
                {
                    permissions.Add(roleClaimOverride.ClaimValue);
                }
                else
                {
                    permissions.Remove(roleClaimOverride.ClaimValue);
                }
            }
        }

        foreach (var over in user.PermissionOverrides)
        {
            var permissionValue = over.ClaimValue;

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

    public bool IsUserHasAccessToUniversity(ClaimsPrincipal user, int requestUniversityId)
    {

        //1.0 System Admin 
        if (user.GetUniversityId() == null && user.GetFacultyId() == null)
            return true;

        //2.0 Others
        if (user.GetUniversityId() == requestUniversityId)
            return true;

        return false;
        
    }

    public bool IsUserHasAccessToFaculty(ClaimsPrincipal user, int requestFacultyId)
    {
        
        //1.0 System Admin 
        if (user.GetUniversityId() == null && user.GetFacultyId() == null)
            return true;

        if(user.GetFacultyId()==null)
        {
            //2.0 Admin University (Here I Must Check That the faculty is subset of the university)
            var isFacultyExists = _unitOfWork.Fauclties
                .IsExist(f => f.Id == requestFacultyId && f.UniversityId == user.GetUniversityId());

            return isFacultyExists;
        }
        else
        {
            //3.0 Others
            if(user.GetFacultyId()== requestFacultyId)
                return true;
            return false;
        }

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
