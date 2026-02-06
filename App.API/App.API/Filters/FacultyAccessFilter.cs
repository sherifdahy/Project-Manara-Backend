using App.Core.Consts;
using App.Core.Entities.Identity;
using App.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SA.Accountring.Core.Entities.Interfaces;
using System.Security.Claims;
public class FacultyAccessFilter : IAsyncActionFilter
{
    private readonly string _parameterName;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;

    public FacultyAccessFilter(string parameterName,IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager)
    {
        _parameterName = parameterName;
        this._unitOfWork = unitOfWork;
        this._userManager = userManager;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;

        if (!context.ActionArguments.TryGetValue(_parameterName, out var obj)
            || obj is not int facultyId)
        {
            await next();
            return;
        }

        if (!await IsUserHasAccessToFaculty(user, facultyId))
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }
    private async Task<bool> IsUserHasAccessToFaculty(ClaimsPrincipal user, int requestFacultyId)
    {
        var userEntity = await _userManager.FindByIdAsync(user.GetUserId().ToString());
        var userRoles = await _userManager.GetRolesAsync(userEntity!);

        if (userRoles.Contains(RolesConstants.SystemAdmin))
            return true;

        var universityUser = _unitOfWork.UniversityUsers
           .Find(fu => fu.UserId == user.GetUserId());

        if (universityUser != null)
            return _unitOfWork.Fauclties.IsExist(f => f.UniversityId == universityUser.UniversityId && f.Id == requestFacultyId);


        var facultyUser = _unitOfWork.FacultyUsers
            .Find(fu => fu.UserId == user.GetUserId());

        if (facultyUser != null)
            return requestFacultyId == facultyUser.FacultyId;


        return false;
    }
}
