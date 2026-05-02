using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities.Relations;

public class LectureRegistration 
{
    public decimal GPA { get; set; }

    public int LectureScheduleId { get; set; }
    public int StudentId { get; set; }

    public LectureSchedule LectureSchedule { get; set; } = default!;
    public Student Student { get; set; } = default!;
}
