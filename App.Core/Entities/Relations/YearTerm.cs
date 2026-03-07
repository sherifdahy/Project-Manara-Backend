using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities.Relations;

public class YearTerm
{
    public bool IsActive { get; set; }
    public int YearId { get; set; }
    public AcademicYear Year { get; set; } = default!;
    public int TermId { get; set; }
    public Term Term { get; set; } = default!;
    public ICollection<ProgramUserProgramYearTerm> ProgramUserProgramYearTerms { get; set; } = new HashSet<ProgramUserProgramYearTerm>();
    public ICollection<DepartmentUserSubjectYearTermPeriod> DepartmentUserSubjectYearTermPeriods { get; set; } = new HashSet<DepartmentUserSubjectYearTermPeriod>();
}
