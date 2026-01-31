using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Requests.Roles;

public record RoleRequest
{
    public string Name { get; set; } = string.Empty;
    public IList<string> Permissions { get; set; } = [];
}
