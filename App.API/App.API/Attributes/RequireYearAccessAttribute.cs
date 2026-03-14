using App.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Attributes;
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]

public class RequireYearAccessAttribute : TypeFilterAttribute
{
    public RequireYearAccessAttribute(string parameterName)
        : base(typeof(YearAccessFilter))
    {
        Arguments = new object[] { parameterName };
    }
}

