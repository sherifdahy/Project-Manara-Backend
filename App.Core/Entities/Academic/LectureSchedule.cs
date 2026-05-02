using App.Core.Entities.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities.Academic;

public class LectureSchedule
{
    public int Id { get; set; }
    public int ProgramId { get; set; }
    public int SubjectId { get; set; }
    public int PeriodId { get; set; }
    public int DayId { get; set; }
    public int YearTermId { get; set; }
    public int DoctorId { get; set; }
    public int MaxSlots { get; set; }


    public Program Program { get; set; } = default!;
    public DepartmentUser Doctor { get; set; } = default!;
    public YearTerm YearTerm { get; set; } = default!;
    public Subject Subject { get; set; } = default!;
    public Period Period { get; set; } = default!;
    public Day Day { get; set; } = default!;

    public ICollection<LectureRegistration> LectureRegistrations { get; set; } = new HashSet<LectureRegistration>();
}
