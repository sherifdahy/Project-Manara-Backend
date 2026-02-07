using App.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Attributes;
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class RequireRoleAccessAttribute : TypeFilterAttribute
{
    public RequireRoleAccessAttribute(string parameterName)
        : base(typeof(RoleAccessFilter))
    {
        Arguments = new object[] { parameterName };
    }
}


