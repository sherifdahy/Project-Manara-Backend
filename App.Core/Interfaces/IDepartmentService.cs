using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace App.Core.Interfaces;

public interface IDepartmentService
{
    Task<bool> IsUserHasAccessToDepartment(ClaimsPrincipal user, int requestDepartmentId);
}
