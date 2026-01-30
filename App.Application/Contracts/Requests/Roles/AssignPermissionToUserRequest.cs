using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Requests.Roles;

public record AssignPermissionToUserRequest
{
    public string ClaimValue { get; set; } = string.Empty;
    public bool IsAllowed { get; set; } = true;
}
