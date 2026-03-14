using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.API.Filters;

public class YearAccessFilter : IAsyncActionFilter
{
    private readonly string _parameterName;
    private readonly IYearService _yearService;

    public YearAccessFilter(string parameterName, IYearService yearService)
    {
        _parameterName = parameterName;
        this._yearService = yearService;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;

        if (!context.ActionArguments.TryGetValue(_parameterName, out var obj)
            || obj is not int yearId)
        {
            await next();
            return;
        }

        if (!await _yearService.IsUserHasAccessToYear(user, yearId))
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }

}
