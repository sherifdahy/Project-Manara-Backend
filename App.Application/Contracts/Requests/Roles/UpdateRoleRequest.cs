using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Requests.Roles;

public record UpdateRoleRequest
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? RoleId { get; set; }
    public IList<string> Permissions { get; set; } = [];
}
