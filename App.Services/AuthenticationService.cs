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

public class AuthenticationService(UserManager<ApplicationUser> userManager
    ,IUnitOfWork unitOfWork
    ,RoleManager<ApplicationRole> roleManager) : IAuthenticationService
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
    public async Task<(IEnumerable<string> roles, IEnumerable<string> permissions)> 
        GetUserOverrideRolesAndPermissions(ApplicationUser user,CancellationToken cancellationToken, bool includeUserOverride = true)
    {

        var roles = await _userManager.GetRolesAsync(user);

        var permissions = new List<string>();

        foreach (var roleName in roles)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role is null) continue;

            var rolePermissions = await _roleManager.GetClaimsAsync(role);

            permissions.AddRange(rolePermissions.Where(x => x.Type == Permissions.Type).Select(x => x.Value).Distinct());

            int facultyId = await GetUserFacultyId(user);

            var roleClaimOverrides = await _unitOfWork.RoleClaimOverrides
                .FindAllAsync(rc => rc.RoleId == role.Id && rc.FacultyId == facultyId);

            foreach (var roleClaimOverride in roleClaimOverrides)
            {
                if (roleClaimOverride is null) continue;
                permissions.Remove(roleClaimOverride.ClaimValue);
            }
        }

        if (includeUserOverride)
        {
            foreach (var over in user.PermissionOverrides)
            {
                var permissionValue = over.ClaimValue;
                permissions.Remove(permissionValue!);
            }
        }

         
        return (roles, permissions);
    }

   



    //TODO IsUserHasAccessToDepartment , IsUserHasAccessToProgramm


    private async Task<int> GetUserFacultyId(ApplicationUser user)
    {
        int facultyId = 0;

        var facultyUser = await _unitOfWork.FacultyUsers
            .FindAsync(fu => fu.UserId == user.Id);

        if (facultyUser != null)
            facultyId = facultyUser.FacultyId;

        var departmentUser = await _unitOfWork.DepartmentUsers
            .FindAsync(du => du.UserId == user.Id, [du => du.Department]);

        if (departmentUser != null)
            facultyId = departmentUser.Department.FacultyId;

        var programUser = await _unitOfWork.ProgramUsers
            .FindAsync(du => du.UserId == user.Id, [du => du.Program.Department]);

        if (programUser != null)
            facultyId = programUser.Program.Department.FacultyId;

        return facultyId;
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
