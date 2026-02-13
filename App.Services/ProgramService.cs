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

        var universityUser = await _unitOfWork.UniversityUsers
           .FindAsync(fu => fu.UserId == user.GetUserId());

        if (universityUser != null)
            return await _unitOfWork.Programs.IsExistAsync(p => p.Department.Faculty.UniversityId == universityUser.UniversityId && p.Id == requestProgramId);


        var facultyUser = await _unitOfWork.FacultyUsers
            .FindAsync(fu => fu.UserId == user.GetUserId());

        if (facultyUser != null)
            return await _unitOfWork.Programs.IsExistAsync(p => p.Department.FacultyId == facultyUser.FacultyId && p.Id == requestProgramId);

        var departmentUser = await _unitOfWork.DepartmentUsers
            .FindAsync(fu => fu.UserId == user.GetUserId());

        if(departmentUser != null)
            return await _unitOfWork.Programs.IsExistAsync(p => p.DepartmentId == departmentUser.DepartmentId && p.Id == requestProgramId);

        var programUser = await _unitOfWork.ProgramUsers
            .FindAsync(fu => fu.UserId == user.GetUserId());

        if (programUser != null)
            return requestProgramId == programUser.ProgramId;


        return false;
    }
}
