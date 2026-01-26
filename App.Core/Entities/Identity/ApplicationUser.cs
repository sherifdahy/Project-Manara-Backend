using App.Core.Entities.Relations;
using App.Core.Entities.Universities;

namespace App.Core.Entities.Identity;
public class ApplicationUser : IdentityUser<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } =string.Empty;
    public bool IsDisabled { get; set; }
    public int? UniversityId { get; set; }
    public int? FacultyId { get; set; }

    public University? University { get; set; }
    public Faculty? Faculty { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; } = [];
    public ICollection<UserPermissionOverride> PermissionOverrides { get; set; } = new HashSet<UserPermissionOverride>();

}
