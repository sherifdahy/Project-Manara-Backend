using App.Application.Contracts.Responses.Departments;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Departments;

public record UpdateDepartmentCommand : IRequest<Result>
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string HeadOfDepartment { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

