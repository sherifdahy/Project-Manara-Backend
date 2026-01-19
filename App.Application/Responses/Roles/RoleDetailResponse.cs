using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Responses.Role;

public record RoleDetailResponse
(
    int Id,
    string Name,
    bool IsDeleted,
    IEnumerable<string> Permissions
);
