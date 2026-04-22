using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Requests.ProgramSchedules;

public class ScheduleItemRequest
{
    public int SubjectId { get; set; }
    public int PeriodId { get; set; }
    public int DayId { get; set; }
}
