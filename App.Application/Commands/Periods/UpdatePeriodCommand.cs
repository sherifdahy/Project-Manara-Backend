using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Periods;

public record UpdatePeriodCommand : IRequest<Result>
{
    public int FacultyId { get; set; }

    
    public TimeOnly OldStartTime { get; set; }
    public TimeOnly OldEndTime { get; set; }


    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}
