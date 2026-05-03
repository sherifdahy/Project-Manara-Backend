using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Requests.SectionSchedules;

public class SectionScheduleItemRequest
{
    public int SubjectId { get; set; }
    public int PeriodId { get; set; }
    public int DayId { get; set; }
    public int InstructorId { get; set; }
    public int MaxSlots { get; set; }
}
