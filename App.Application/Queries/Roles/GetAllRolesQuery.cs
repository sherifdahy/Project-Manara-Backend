using App.Application.Abstractions;
using App.Application.Contracts.Responses.Roles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Roles;

public class GetAllRolesQuery : IRequest<Result<List<RoleResponse>>>
{
    public bool? IncludeDisabled { get; set; } = false;

    public GetAllRolesQuery(bool? includeDisabled)
    {
        IncludeDisabled = includeDisabled;
    }
}
