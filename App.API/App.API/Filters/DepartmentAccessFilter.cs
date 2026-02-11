using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.API.Filters;

public class DepartmentAccessFilter : IAsyncActionFilter
{
    private readonly string _parameterName;
    private readonly IDepartmentService _departmentService;

    public DepartmentAccessFilter(string parameterName,IDepartmentService departmentService)
    {
        _parameterName = parameterName;
        this._departmentService = departmentService;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;

        if (!context.ActionArguments.TryGetValue(_parameterName, out var obj)
            || obj is not int departmentId)
        {
            await next();
            return;
        }

        if (!await _departmentService.IsUserHasAccessToDepartment(user, departmentId))
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }

}
