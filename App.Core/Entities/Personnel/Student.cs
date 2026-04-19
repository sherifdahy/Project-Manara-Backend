using App.Core.Entities.Identity;
using App.Core.Entities.Relations;

namespace App.Core.Entities.Personnel;

public class Student
{
    public int UserId { get; set; }
    public ApplicationUser User { get; set; } = default!;

    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; } = default!;

    public ICollection<StudentProgramYearTerm> StudentProgramYearTerms { get; set; } = new HashSet<StudentProgramYearTerm>();
}
