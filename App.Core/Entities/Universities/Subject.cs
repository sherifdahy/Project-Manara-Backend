using App.Core.Entities.Relations;

namespace App.Core.Entities.Universities;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CreditHours { get; set; }
    public int ParentSubjectId { get; set; }
    public Subject ParentSubject { get; set; } = default!;
    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; } = default!;
    public ICollection<ProgramSubject> ProgramSubjects { get; set; } = new HashSet<ProgramSubject>();
    public ICollection<DepartmentUserSubjectYearTermPeriod> DepartmentUserSubjectYearTermPeriods { get; set; } = new HashSet<DepartmentUserSubjectYearTermPeriod>();
}

