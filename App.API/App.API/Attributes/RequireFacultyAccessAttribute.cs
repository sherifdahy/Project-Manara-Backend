using Microsoft.AspNetCore.Mvc;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class RequireFacultyAccessAttribute : TypeFilterAttribute
{
    public RequireFacultyAccessAttribute(string parameterName)
        : base(typeof(FacultyAccessFilter))
    {
        Arguments = new object[] { parameterName };
    }
}