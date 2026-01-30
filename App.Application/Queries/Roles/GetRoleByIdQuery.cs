using App.Application.Abstractions;
using App.Application.Contracts.Responses.Roles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Roles;

public class GetRoleByIdQuery : IRequest<Result<RoleDetailResponse>>
{
    public int Id { get; set; }

    public GetRoleByIdQuery(int id)
    {
        Id = id;
    }
}
