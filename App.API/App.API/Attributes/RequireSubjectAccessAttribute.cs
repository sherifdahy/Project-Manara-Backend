using App.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Attributes;
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]


public class RequireSubjectAccessAttribute : TypeFilterAttribute
{
    public RequireSubjectAccessAttribute(string parameterName)
        : base(typeof(SubjectAccessFilter))
    {
        Arguments = new object[] { parameterName };
    }
}
