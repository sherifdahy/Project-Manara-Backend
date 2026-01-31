using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Roles;

public record AssignToRolePermissionResponse
(
    int RoleId,
    int FacultyId,
    string ClaimValue,
    bool IsAllowed
);
