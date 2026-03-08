using App.Core.Entities.Relations;

namespace App.Core.Entities.Universities;

public class Program

{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CreditHours { get; set; }
    public bool IsDeleted { get; set; } = false;

    public int DepartmentId { get; set; }
    public Department Department { get; set; } = default!;
    public ICollection<ProgramUserProgramYearTerm> ProgramUserProgramYearTerms { get; set; } = new HashSet<ProgramUserProgramYearTerm>();
    public ICollection<ProgramSubject> ProgramSubjects { get; set; } = new HashSet<ProgramSubject>();    
    public ICollection<ProgramUser> ProgramUsers { get; set; } = new HashSet<ProgramUser>();
}
