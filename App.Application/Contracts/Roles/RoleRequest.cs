using App.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Roles;

public record RoleRequest
{
    public string Name { get; set; } = string.Empty;
    public RoleType RoleType { get; set; } = RoleType.Student;
    public IList<string> Permissions { get; set; } = [];
}
