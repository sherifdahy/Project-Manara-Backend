using App.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Attributes;
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]

public class RequireEnrollmentAccessAttribute : TypeFilterAttribute
{
    public RequireEnrollmentAccessAttribute(string parameterName)
        : base(typeof(EnrollmentAccessFilter))
    {
        Arguments = new object[] { parameterName };
    }
}

