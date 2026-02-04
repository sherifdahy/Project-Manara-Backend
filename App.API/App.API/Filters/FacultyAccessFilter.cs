using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
public class FacultyAccessFilter : IAsyncActionFilter
{
    private readonly IAuthenticationService _accessService;
    private readonly string _parameterName;

    public FacultyAccessFilter(
        IAuthenticationService accessService,
        string parameterName)
    {
        _accessService = accessService;
        _parameterName = parameterName;
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

        if (!_accessService.IsUserHasAccessToFaculty(user, facultyId))
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }
}
