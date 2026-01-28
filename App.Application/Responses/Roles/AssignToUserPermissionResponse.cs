using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Responses.Roles;

public record AssignToUserPermissionResponse
(
    int UserId,
    string ClaimValue,
    bool IsAllowed
);
