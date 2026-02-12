using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace App.Core.Interfaces;

public interface IProgramService
{
    Task<bool> IsUserHasAccessToProgram(ClaimsPrincipal user, int requestProgramId);
}
