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

        var universityUser = _unitOfWork.UniversityUsers
           .Find(fu => fu.UserId == user.GetUserId());

        if (universityUser != null)
            return _unitOfWork.Departments.IsExist(f => f.Faculty.UniversityId == universityUser.UniversityId && f.Id == requestDepartmentId);


        var facultyUser = _unitOfWork.FacultyUsers
            .Find(fu => fu.UserId == user.GetUserId());

        if(facultyUser!=null)
            return _unitOfWork.Departments.IsExist(f => f.FacultyId == facultyUser.FacultyId && f.Id == requestDepartmentId);

        var departmentUser = _unitOfWork.DepartmentUsers
            .Find(fu => fu.UserId == user.GetUserId());

        if (departmentUser != null)
        return requestDepartmentId == departmentUser.DepartmentId;


        return false;
    }
}
