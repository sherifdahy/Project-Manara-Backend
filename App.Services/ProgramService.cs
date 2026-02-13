using App.Core.Consts;
using App.Core.Entities.Identity;
using App.Core.Extensions;
using App.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using SA.Accountring.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace App.Services;

public class ProgramService(UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork) :IProgramService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> IsUserHasAccessToProgram(ClaimsPrincipal user, int requestProgramId)
    {
        var userEntity = await _userManager.FindByIdAsync(user.GetUserId().ToString());
        var userRoles = await _userManager.GetRolesAsync(userEntity!);

        if (userRoles.Contains(RolesConstants.SystemAdmin))
            return true;

        var universityUser = _unitOfWork.UniversityUsers
           .Find(fu => fu.UserId == user.GetUserId());

        if (universityUser != null)
            return _unitOfWork.Programs.IsExist(p => p.Department.Faculty.UniversityId == universityUser.UniversityId && p.Id == requestProgramId);


        var facultyUser = _unitOfWork.FacultyUsers
            .Find(fu => fu.UserId == user.GetUserId());

        if (facultyUser != null)
            return _unitOfWork.Programs.IsExist(p => p.Department.FacultyId == facultyUser.FacultyId && p.Id == requestProgramId);

        var departmentUser = _unitOfWork.DepartmentUsers
            .Find(fu => fu.UserId == user.GetUserId());

        if(departmentUser != null)
            return _unitOfWork.Programs.IsExist(p => p.DepartmentId == departmentUser.DepartmentId && p.Id == requestProgramId);

        var programUser = _unitOfWork.ProgramUsers
            .Find(fu => fu.UserId == user.GetUserId());

        if (programUser != null)
            return requestProgramId == programUser.ProgramId;


        return false;
    }
}
