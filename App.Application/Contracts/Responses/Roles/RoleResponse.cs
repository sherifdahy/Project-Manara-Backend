using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Roles;

public record RoleResponse
(
    int Id,
    string Name,
    string Description,
    bool IsDeleted
);
