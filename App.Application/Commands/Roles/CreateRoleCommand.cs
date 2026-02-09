using App.Application.Abstractions;
using App.Application.Contracts.Responses.Roles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Roles;

public record CreateRoleCommand : IRequest<Result<RoleDetailResponse>>
{
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public bool IsDeleted { get; init; } = false; 
    public int? ParentRoleId { get; init; }
    public IList<string> Permissions { get; init; } = [];
}
