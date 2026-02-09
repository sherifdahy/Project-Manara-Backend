using App.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Attributes;
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]

public class RequireUserAccessAttribute : TypeFilterAttribute
{
    public RequireUserAccessAttribute(string parameterName)
        : base(typeof(UserAccessFilter))
    {
        Arguments = new object[] { parameterName };
    }
}


