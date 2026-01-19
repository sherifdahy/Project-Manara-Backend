using Microsoft.AspNetCore.Authorization;

namespace App.Application.Authentication.Filters;
public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{
}
