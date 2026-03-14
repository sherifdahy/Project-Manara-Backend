using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Years;

public record UpdateYearCommand : IRequest<Result>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ActiveTermId { get; set; }
}
