using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace App.Core.Interfaces;

public interface IScopeService
{
    Task<bool> IsUserHasAccessToScope(ClaimsPrincipal user, string requestScopeName);
}
