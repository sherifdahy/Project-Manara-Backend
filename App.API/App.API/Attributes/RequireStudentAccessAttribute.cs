using App.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class RequireStudentAccessAttribute : TypeFilterAttribute
{
    public RequireStudentAccessAttribute(string parameterName)
        : base(typeof(StudentAccessFilter))
    {
        Arguments = new object[] { parameterName };
    }
}

