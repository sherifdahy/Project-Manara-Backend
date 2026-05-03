using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Requests.LectureSchedules;

public class LectureScheduleItemRequest
{
    public int SubjectId { get; set; }
    public int PeriodId { get; set; }
    public int DayId { get; set; }
    public int DoctorId { get; set; }
    public int MaxSlots { get; set; }
}
