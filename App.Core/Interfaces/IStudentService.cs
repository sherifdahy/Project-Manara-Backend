using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace App.Core.Interfaces;

public interface IStudentService
{
     Task<bool> IsUsersInSameFaculty(ClaimsPrincipal user, int requestStudentId);
}
