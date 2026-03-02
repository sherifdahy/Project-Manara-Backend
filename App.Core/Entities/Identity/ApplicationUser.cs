using App.Core.Entities.Universities;
using App.Core.Enums;

namespace App.Core.Entities.Identity;
public class ApplicationUser : IdentityUser<int>
{
    public string Name { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public Gender Gender { get; set; }
    public Religion Religion { get; set; }
    public bool IsDisabled { get; set; }
    public bool IsDeleted { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; } = [];
    public ICollection<UserClaimOverride> PermissionOverrides { get; set; } = new HashSet<UserClaimOverride>();
    public FacultyUser FacultyUser { get; set; } = default!;
    public UniversityUser UniversityUser { get; set; } = default!;
    public DepartmentUser DepartmentUser { get; set; } = default!;
    public ProgramUser ProgramUser { get; set; } = default!;
}
