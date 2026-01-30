using App.Application.Abstractions;
using App.Application.Contracts.Responses.Roles;
using App.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Roles;

public record CreateRoleCommand : IRequest<Result<RoleDetailResponse>>
{
    public string Name { get; set; } = string.Empty;
    public int UniversityId { get; set; } 
    public RoleType RoleType { get; set; } = RoleType.Student; 
    public IList<string> Permissions { get; set; } = [];
}
