using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Programs;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Programs;

public record CreateProgramCommand : IRequest<Result<ProgramResponse>>
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CreditHours { get; set; }
    public int DepartmentId { get; set; }
}
