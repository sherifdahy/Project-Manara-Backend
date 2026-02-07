using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Requests.Roles;

public record AssignPermissionRequest
{
    public List<string> ClaimValues { get; set; } = default!;
}
