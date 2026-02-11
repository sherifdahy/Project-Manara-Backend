using App.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Attributes;
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]

public class RequireDepartmentAccessAttribute : TypeFilterAttribute
{
    public RequireDepartmentAccessAttribute(string parameterName)
        : base(typeof(DepartmentAccessFilter))
    {
        Arguments = new object[] { parameterName };
    }
}
