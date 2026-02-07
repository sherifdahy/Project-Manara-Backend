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
    private readonly IFacultyService _facultyService;

    public FacultyAccessFilter(string parameterName,IFacultyService facultyService)
    {
        _parameterName = parameterName;
        this._facultyService = facultyService;
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

        if (!await _facultyService.IsUserHasAccessToFaculty(user, facultyId))
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }

}
