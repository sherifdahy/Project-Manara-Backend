using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.API.Filters;

public class SubjectAccessFilter : IAsyncActionFilter
{
    private readonly string _parameterName;
    private readonly ISubjectService subjectService;

    public SubjectAccessFilter(string parameterName, ISubjectService subjectService)
    {
        _parameterName = parameterName;
        this.subjectService = subjectService;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;

        if (!context.ActionArguments.TryGetValue(_parameterName, out var obj)
            || obj is not int subjectId)
        {
            await next();
            return;
        }

        if (!await subjectService.IsUserHasAccessToSubject(user, subjectId))
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }

}