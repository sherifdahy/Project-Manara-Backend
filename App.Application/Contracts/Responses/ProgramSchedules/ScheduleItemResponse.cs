using App.Application.Contracts.Responses.Subjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.ProgramSchedules;

public record ScheduleItemResponse
{
    public SubjectResponse Subject { get; set; } = default!;
    public int PeriodId { get; set; }
    public int DayId { get; set; }
}
