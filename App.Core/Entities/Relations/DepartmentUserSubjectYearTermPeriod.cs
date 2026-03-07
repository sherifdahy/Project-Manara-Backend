using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities.Relations;

public class DepartmentUserSubjectYearTermPeriod
{
    public int UserId { get; set; }
    public DepartmentUser User { get; set; } = default!;

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = default!;


    public int YearTermId { get; set; }
    public YearTerm YearTerm { get; set; }= default!;

    public int PeriodId { get; set; }
    public Period Period { get; set; }= default!;
}
