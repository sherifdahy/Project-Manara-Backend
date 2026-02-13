using App.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Attributes;
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]

public class RequireProgramAccessAttribute : TypeFilterAttribute
{
    public RequireProgramAccessAttribute(string parameterName)
        : base(typeof(ProgramAccessFilter))
    {
        Arguments = new object[] { parameterName };
    }
}
