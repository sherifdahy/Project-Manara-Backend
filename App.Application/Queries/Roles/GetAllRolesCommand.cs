using App.Application.Abstractions;
using App.Application.Responses.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Roles;

public class GetAllRolesCommand : IRequest<Result<List<RoleResponse>>>
{
    public bool? IncludeDisabled { get; set; } = false;

    public GetAllRolesCommand(bool? includeDisabled)
    {
        IncludeDisabled = includeDisabled;
    }
}
