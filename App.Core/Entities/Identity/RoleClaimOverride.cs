using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities.Identity;

public class RoleClaimOverride
{
    public int RoleId { get; set; }
    public int FacultyId { get; set; }
    public string ClaimValue { get; set; } = string.Empty;

    public Faculty Faculty { get; set; } = default!;
    public ApplicationRole Role { get; set; } = default!;
}
