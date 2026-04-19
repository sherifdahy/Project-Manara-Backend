using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities.Relations;

public class ProgramSubjectPeriodDay
{
    public int ProgramId { get; set; }
    public Program Program { get; set; } = default!;
    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = default!;
    public int PeriodId { get; set; }
    public Period Period { get; set; } = default!;
    public int DayId { get; set; }
    public Day Day { get; set; } = default!;
}
