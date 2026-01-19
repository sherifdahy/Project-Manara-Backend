using Microsoft.AspNetCore.Authorization;

namespace App.Application.Authentication.Filters;

public class PermisssionRequirement : IAuthorizationRequirement
{
    public string permission { get; }
    public PermisssionRequirement(string permission)
    {
        this.permission = permission;
    }
}
