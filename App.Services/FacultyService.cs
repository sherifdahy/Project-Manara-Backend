
using App.Core.Consts;
using App.Core.Entities.Identity;
using App.Core.Extensions;
using App.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using SA.Accountring.Core.Entities.Interfaces;
using System.Security.Claims;

namespace App.Services;

public class FacultyService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork) : IFacultyService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> IsUserHasAccessToFaculty(ClaimsPrincipal user, int requestFacultyId)
    {
        var userEntity = await _userManager.FindByIdAsync(user.GetUserId().ToString());
        var userRoles = await _userManager.GetRolesAsync(userEntity!);

        if (userRoles.Contains(RolesConstants.SystemAdmin))
            return true;

        var universityUser = await _unitOfWork.UniversityUsers
           .FindAsync(fu => fu.UserId == user.GetUserId());

        if (universityUser != null)
            return await _unitOfWork.Fauclties.IsExistAsync(f => f.UniversityId == universityUser.UniversityId && f.Id == requestFacultyId);


        var facultyUser = await _unitOfWork.FacultyUsers
            .FindAsync(fu => fu.UserId == user.GetUserId());

        if (facultyUser != null)
            return requestFacultyId == facultyUser.FacultyId;


        return false;
    }
}