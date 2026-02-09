using App.Core.Consts;
using App.Core.Entities.Identity;
using App.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SA.Accountring.Core.Entities.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

public class UniversityAccessFilter : IAsyncActionFilter
{
    private readonly string _parameterName;
    private readonly IUniversityService _universityService;

    public UniversityAccessFilter(string parameterName,IUniversityService universityService)
    {
        _parameterName = parameterName;
        this._universityService = universityService;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;

        if (!context.ActionArguments.TryGetValue(_parameterName, out var obj)
            || obj is not int universityId)
        {
            await next();
            return;
        }

        if (!await _universityService.IsUserHasAccessToUniversity(user, universityId))
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }


}