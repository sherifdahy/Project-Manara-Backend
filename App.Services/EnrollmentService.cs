using App.Core.Consts;
using App.Core.Entities.Identity;
using App.Core.Extensions;
using App.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using SA.Accountring.Core.Entities.Interfaces;
using System.Security.Claims;

namespace App.Services;

public class EnrollmentService(UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork) : IEnrollmentService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    public async Task<bool> IsUserHasAccessToEnrollment(ClaimsPrincipal user, int requestEnrollmentId)
    {
        var userEntity = await _userManager.FindByIdAsync(user.GetUserId().ToString());
        var userRoles = await _userManager.GetRolesAsync(userEntity!);

        if (userRoles.Contains(RolesConstants.SystemAdmin))
            return true;

        var universityUser = await _unitOfWork.UniversityUsers
           .FindAsync(fu => fu.UserId == user.GetUserId());

        if (universityUser != null)
        {
            return await _unitOfWork.StudentProgramYearTerms
                .IsExistAsync(p => p.Program.Department.Faculty.UniversityId == universityUser.UniversityId && p.Id == requestEnrollmentId);

        }

        var facultyUser = await _unitOfWork.FacultyUsers
            .FindAsync(fu => fu.UserId == user.GetUserId());

        if (facultyUser != null)
        {
            return await _unitOfWork.StudentProgramYearTerms
                .IsExistAsync(p => p.Program.Department.FacultyId == facultyUser.FacultyId && p.Id == requestEnrollmentId);
        }

        var departmentUser = await _unitOfWork.DepartmentUsers
            .FindAsync(fu => fu.UserId == user.GetUserId());

        if (departmentUser != null)
        {
            return await _unitOfWork.StudentProgramYearTerms
                .IsExistAsync(p => p.Program.DepartmentId == departmentUser.DepartmentId && p.Id == requestEnrollmentId);
        }



        return false;
    }
}
