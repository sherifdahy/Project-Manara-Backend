using App.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SA.Accountring.Core.Entities.Interfaces;

namespace App.API.Filters;

public class ScopeAccessFilter : IAsyncActionFilter
{
    private readonly string _parameterName;
    private readonly IScopeService _scopeService;

    public ScopeAccessFilter(string parameterName,IScopeService scopeService)
    {
        _parameterName = parameterName;
        this._scopeService = scopeService;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;
        if (!context.ActionArguments.TryGetValue(_parameterName, out var obj)
                || obj is not string scopeName)
        {
            await next();
            return;
        }

        if (!await _scopeService.IsUserHasAccessToScope(user, scopeName))
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }

}