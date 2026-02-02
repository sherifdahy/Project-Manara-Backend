using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Roles;

public record RoleDetailResponse
(
    int Id,
    string Name,
    string Description,
    bool IsDeleted,
    IEnumerable<string> Permissions
);
