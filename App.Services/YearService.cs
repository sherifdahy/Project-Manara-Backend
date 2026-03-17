
using App.Core.Consts;
using App.Core.Entities.Identity;
using App.Core.Extensions;
using App.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using SA.Accountring.Core.Entities.Interfaces;
using System.Security.Claims;

namespace App.Services;

public class YearService(UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork) : IYearService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    public async Task<bool> IsUserHasAccessToYear(ClaimsPrincipal user, int requestYearId)
    {
        var userEntity = await _userManager.FindByIdAsync(user.GetUserId().ToString());
        var userRoles = await _userManager.GetRolesAsync(userEntity!);

        if (userRoles.Contains(RolesConstants.SystemAdmin))
            return true;

        var universityUser = await _unitOfWork.UniversityUsers
           .FindAsync(fu => fu.UserId == user.GetUserId());

        if (universityUser != null)
            return await _unitOfWork.AcademicYears.IsExistAsync(f => f.Faculty.UniversityId == universityUser.UniversityId && f.Id == requestYearId);

        var facultyUser = await _unitOfWork.FacultyUsers
            .FindAsync(fu => fu.UserId == user.GetUserId());

        if (facultyUser != null)
            return await _unitOfWork.AcademicYears.IsExistAsync(f => f.FacultyId == facultyUser.FacultyId && f.Id == requestYearId);

        return false;
    }

}
