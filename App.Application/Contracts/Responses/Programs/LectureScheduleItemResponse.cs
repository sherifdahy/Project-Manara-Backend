using App.Application.Contracts.Responses.Subjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Programs;

public record LectureScheduleItemResponse
{
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public int PeriodId { get; set; }
    public int DayId { get; set; }
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public int MaxSlots { get; set; }
}
