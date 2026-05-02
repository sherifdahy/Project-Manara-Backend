using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities.Relations;

public class SectionRegistration
{
    public int SectionScheduleId { get; set; }
    public int StudentId { get; set; }
    public SectionSchedule SectionSchedule { get; set; } = default!;
    public Student Student { get; set; } = default!;
}
