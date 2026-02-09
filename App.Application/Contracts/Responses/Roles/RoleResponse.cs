using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Roles;

public record RoleResponse
(
    int Id,
    string Name,
    string Code,
    string Description,
    bool IsDeleted,
    int NumberOfPermissions
);