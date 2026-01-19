using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Responses.Role;

public record RoleResponse
(
    int Id,
    string Name,
    bool IsDeleted
);
