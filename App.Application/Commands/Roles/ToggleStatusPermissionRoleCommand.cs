using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace App.Application.Commands.Roles;

public record ToggleStatusPermissionRoleCommand : IRequest<Result>
{
    public int RoleId { get; set; }
    public int FacultyId { get; set; }
    public ClaimsPrincipal User { get; set; } = default!;
    public string ClaimValue { get; set; } = string.Empty;

}
