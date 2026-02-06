using App.Core.Entities.Universities;

namespace App.Core.Entities.Identity;
public class ApplicationUser : IdentityUser<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } =string.Empty;
    public bool IsDisabled { get; set; }

    public List<RefreshToken> RefreshTokens { get; set; } = [];
    public ICollection<UserClaimOverride> PermissionOverrides { get; set; } = new HashSet<UserClaimOverride>();
    public FacultyUser FacultyUser { get; set; } = default!;
    public UniversityUser UniversityUser { get; set; } = default!;
    public DepartmentUser DepartmentUser { get; set; } = default!;
    public ProgramUser ProgramUser { get; set; } = default!;

}
