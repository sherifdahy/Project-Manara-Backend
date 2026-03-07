

using App.Core.Entities.Identity;
using App.Core.Entities.Relations;

namespace App.Core.Entities.Personnel;

public class DepartmentUser
{
    public int UserId { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = default!;
    public ApplicationUser User { get; set; } = default!;
    public ICollection<DepartmentUserSubjectYearTermPeriod> DepartmentUserSubjectYearTermPeriods { get; set; } = new HashSet<DepartmentUserSubjectYearTermPeriod>();
}
