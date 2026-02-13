using App.Core.Consts;
using App.Core.Entities.Identity;
using App.Core.Extensions;
using App.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using SA.Accountring.Core.Entities.Interfaces;
using System.Security.Claims;

namespace App.Services;

public class DepartmentService(UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork) : IDepartmentService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> IsUserHasAccessToDepartment(ClaimsPrincipal user, int requestDepartmentId)
    {
        var userEntity = await _userManager.FindByIdAsync(user.GetUserId().ToString());
        var userRoles = await _userManager.GetRolesAsync(userEntity!);

        if (userRoles.Contains(RolesConstants.SystemAdmin))
            return true;

        var universityUser = await _unitOfWork.UniversityUsers
           .FindAsync(fu => fu.UserId == user.GetUserId());

        if (universityUser != null)
            return await _unitOfWork.Departments.IsExistAsync(f => f.Faculty.UniversityId == universityUser.UniversityId && f.Id == requestDepartmentId);


        var facultyUser = await _unitOfWork.FacultyUsers
            .FindAsync(fu => fu.UserId == user.GetUserId());

        if(facultyUser!=null)
            return await _unitOfWork.Departments.IsExistAsync(f => f.FacultyId == facultyUser.FacultyId && f.Id == requestDepartmentId);

        var departmentUser = await _unitOfWork.DepartmentUsers
            .FindAsync(fu => fu.UserId == user.GetUserId());

        if (departmentUser != null)
        return requestDepartmentId == departmentUser.DepartmentId;


        return false;
    }
}
