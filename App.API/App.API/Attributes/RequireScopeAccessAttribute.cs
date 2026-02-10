using App.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Attributes;


[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class RequireScopeAccessAttribute : TypeFilterAttribute
{
    public RequireScopeAccessAttribute(string parameterName)
        : base(typeof(ScopeAccessFilter))
    {
        Arguments = new object[] { parameterName };
    }
}