using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Days;

public record DayResponse
{
    public int Id { get; set; }
    public string Value { get; set; } = string.Empty;
}
