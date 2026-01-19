using App.Application.Abstractions;
using App.Application.Responses.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Roles;

public class GetRoleByIdCommand : IRequest<Result<RoleDetailResponse>>
{
    public int Id { get; set; }

    public GetRoleByIdCommand(int id)
    {
        Id = id;
    }
}
