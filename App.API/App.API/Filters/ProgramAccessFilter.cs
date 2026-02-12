using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.API.Filters;

public class ProgramAccessFilter : IAsyncActionFilter
{
    private readonly string _parameterName;
    private readonly IProgramService _programService;

    public ProgramAccessFilter(string parameterName,IProgramService programService)
    {
        _parameterName = parameterName;
        this._programService = programService;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;

        if (!context.ActionArguments.TryGetValue(_parameterName, out var obj)
            || obj is not int programId)
        {
            await next();
            return;
        }

        if (!await _programService.IsUserHasAccessToProgram(user, programId))
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }

}
