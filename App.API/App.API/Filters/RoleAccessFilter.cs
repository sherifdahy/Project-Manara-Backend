using App.Core.Consts;
using App.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SA.Accountring.Core.Entities.Interfaces;
using System.Security.Claims;

namespace App.API.Filters;

public class RoleAccessFilter : IAsyncActionFilter
{
    private readonly string _parameterName;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IRoleService _roleService;

    public RoleAccessFilter(string parameterName, IUnitOfWork unitOfWork
        , UserManager<ApplicationUser> userManager,IRoleService roleService)
    {
        _parameterName = parameterName;
        this._unitOfWork = unitOfWork;
        this._userManager = userManager;
        this._roleService = roleService;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;

        if (!context.ActionArguments.TryGetValue(_parameterName, out var obj)
            || obj is not int roleId)
        {
            await next();
            return;
        }

        if (!await _roleService.IsUserHasAccessToRole(user, roleId))
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }

}