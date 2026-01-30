using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Roles;

public record ToggleStatusPermissionRequest
{
    public string ClaimValue { get; set; } = string.Empty;

}
