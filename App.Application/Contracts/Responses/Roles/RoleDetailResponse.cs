using App.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Roles;

public record RoleDetailResponse
(
    int Id,
    string Name,
    bool IsDeleted,
    int? UniversityId,
    RoleType RoleType,
    IEnumerable<string> Permissions
);
