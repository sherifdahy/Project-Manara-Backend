using App.Application.Abstractions;
using App.Application.Responses.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Roles;

public record CreateRoleCommand : IRequest<Result<RoleDetailResponse>>
{
    public string Name { get; set; } = string.Empty;
    public IList<string> Permissions { get; set; } = [];
}
