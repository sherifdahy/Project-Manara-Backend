using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.API.Filters;

public class StudentAccessFilter : IAsyncActionFilter
{
    private readonly string _parameterName;
    private readonly IStudentService _studentService;

    public StudentAccessFilter(string parameterName, IStudentService facultyService)
    {
        _parameterName = parameterName;
        this._studentService = facultyService;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;

        if (!context.ActionArguments.TryGetValue(_parameterName, out var obj)
            || obj is not int studentId)
        {
            await next();
            return;
        }

        if (!await _studentService.IsUsersInSameFaculty(user, studentId))
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }

}
