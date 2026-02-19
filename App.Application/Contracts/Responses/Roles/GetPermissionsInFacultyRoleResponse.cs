using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Roles;

public record GetPermissionsInFacultyRoleResponse
(
    int Id,
    string Name,
    string Code,
    string Description,
    bool IsDeleted,
    IEnumerable<string> DefaultPermissions,
    IEnumerable<string> OverridePermissions
);
