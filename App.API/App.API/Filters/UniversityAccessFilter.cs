using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class UniversityAccessFilter : IAsyncActionFilter
{
    private readonly IAuthenticationService _accessService;
    private readonly string _parameterName;

    public UniversityAccessFilter(
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
            || obj is not int universityId)
        {
            await next();
            return;
        }

        if (!_accessService.IsUserHasAccessToUniversity(user, universityId))
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }
}