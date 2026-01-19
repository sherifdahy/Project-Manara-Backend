using App.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Roles;

public record ToggleStatusRoleCommand : IRequest<Result>
{
    public int Id { get; private set; }

    public ToggleStatusRoleCommand(int id)
    {
        Id = id;
    }

}
