using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Roles;

public record RoleDetailResponse
(
    int Id,
    string Name,
    string Code,
    string Description,
    bool IsDeleted,
    int  NumberOfUsers,
    IEnumerable<string> Permissions
);
