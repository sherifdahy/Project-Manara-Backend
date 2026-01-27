using App.Application.Abstractions;
using App.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Roles;

public record UpdateRoleCommand : IRequest<Result>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int UniversityId { get; set; }
    public RoleType RoleType { get; set; } = RoleType.Student;
    public IList<string> Permissions { get; set; } = [];
}
