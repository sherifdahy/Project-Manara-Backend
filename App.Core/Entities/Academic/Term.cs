using App.Core.Entities.Relations;

namespace App.Core.Entities.Academic;
public class Term
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public ICollection<YearTerm> YearTerms { get; set; } = new HashSet<YearTerm>();
    public ICollection<DepartmentUserSubjectYearTermPeriod> DepartmentUserSubjectYearTermPeriods { get; set; } = new HashSet<DepartmentUserSubjectYearTermPeriod>();
}
