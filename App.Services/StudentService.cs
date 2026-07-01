using App.Core.Consts;
using App.Core.Entities.Identity;
using App.Core.Extensions;
using App.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SA.Accountring.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace App.Services;

public class StudentService(UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork) : IStudentService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    public async Task<bool> IsUsersInSameFaculty(ClaimsPrincipal user, int requestStudentId)
    {
        var userEntity = await _userManager.FindByIdAsync(user.GetUserId().ToString());
        var userRoles = await _userManager.GetRolesAsync(userEntity!);

        var requestStudent = await _unitOfWork.Students.GetByIdAsync(requestStudentId);


        if (userRoles.Contains(RolesConstants.SystemAdmin))
            return true;

        if(requestStudent==null)
            return false;

        var universityUser = await _unitOfWork.UniversityUsers
           .FindAsync(fu => fu.UserId == user.GetUserId());

        if (universityUser != null)
            return await _unitOfWork.Fauclties.IsExistAsync(f => f.UniversityId == universityUser.UniversityId && f.Id == requestStudent.FacultyId);


        var facultyUser = await _unitOfWork.FacultyUsers
            .FindAsync(fu => fu.UserId == user.GetUserId());

        if (facultyUser != null)
            return requestStudent.FacultyId == facultyUser.FacultyId;


        var departmentUser = await _unitOfWork.DepartmentUsers
            .FindAsync(fu => fu.UserId == user.GetUserId(),q=>q.Include(x=>x.Department));

        if (departmentUser != null)
            return requestStudent.FacultyId == departmentUser.Department.FacultyId;


        return user.GetUserId()==requestStudent.UserId;
    }


}
