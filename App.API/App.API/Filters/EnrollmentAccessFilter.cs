using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.API.Filters;

public class EnrollmentAccessFilter : IAsyncActionFilter
{
    private readonly string _parameterName;
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentAccessFilter(string parameterName,IEnrollmentService enrollmentService)
    {
        _parameterName = parameterName;
        this._enrollmentService = enrollmentService;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;

        if (!context.ActionArguments.TryGetValue(_parameterName, out var obj)
            || obj is not int enrollmentId)
        {
            await next();
            return;
        }

        if (!await _enrollmentService.IsUserHasAccessToEnrollment(user, enrollmentId))
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }

}
