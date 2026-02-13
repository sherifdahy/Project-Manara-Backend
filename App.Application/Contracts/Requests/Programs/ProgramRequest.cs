using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Requests.Programs;

public record ProgramRequest
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; }=string.Empty;
    public string Description { get; set; } = string.Empty; 
    public int CreditHours { get; set; }
}
