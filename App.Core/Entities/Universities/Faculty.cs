using App.Core.Entities.Identity;

namespace App.Core.Entities.Universities;
public class Faculty
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public int UniversityId { get; set; }
    public University University { get; set; } = default!;
    public ICollection<FacultyUser> FacultyUsers { get; set; } = new HashSet<FacultyUser>();
    public ICollection<Department> Departments { get; set; } = new HashSet<Department>();
    public ICollection<RoleClaimOverride> RoleClaimOverrides { get; set; } = new HashSet<RoleClaimOverride>();
}
