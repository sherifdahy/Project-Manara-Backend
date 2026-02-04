using Microsoft.AspNetCore.Mvc;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class RequireUniversityAccessAttribute : TypeFilterAttribute
{
    public RequireUniversityAccessAttribute(string parameterName)
        : base(typeof(UniversityAccessFilter))
    {
        Arguments = new object[] { parameterName };
    }
}