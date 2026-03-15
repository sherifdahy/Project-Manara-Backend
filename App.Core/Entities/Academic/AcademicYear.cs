using App.Core.Entities.Relations;

namespace App.Core.Entities.Academic;
public class AcademicYear
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<YearTerm> YearTerms { get; set; } = new HashSet<YearTerm>();
    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; } = default!;
    public bool IsDeleted { get; set; } = false;
}