using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Programs;

public record UpdateProgramCommand : IRequest<Result>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CreditHours { get; set; }
}
