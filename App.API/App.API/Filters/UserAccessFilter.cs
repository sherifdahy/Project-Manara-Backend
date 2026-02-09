using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.API.Filters;

public class UserAccessFilter : IAsyncActionFilter
{
    private readonly string _parameterName;
    private readonly IUserService _userService;

    public UserAccessFilter(string parameterName, IUserService userService)
    {
        _parameterName = parameterName;
        this._userService = userService;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;

        if (!context.ActionArguments.TryGetValue(_parameterName, out var obj)
            || obj is not int userId)
        {
            await next();
            return;
        }

        if (!await _userService.IsUserHasAccessToUser(user, userId))
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }


}