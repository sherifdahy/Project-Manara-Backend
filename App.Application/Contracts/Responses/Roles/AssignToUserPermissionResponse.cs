using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Roles;

public record AssignToUserPermissionResponse
(
    int UserId,
    string ClaimValue,
    bool IsAllowed
);
