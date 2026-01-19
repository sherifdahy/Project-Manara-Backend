namespace App.Core.Entities.Identity;
using App.Core.Entities.University;
public class ApplicationUser : IdentityUser<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } =string.Empty;
    public bool IsDisabled { get; set; }
    public University? University { get; set; }
    public Faculty? Faculty { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; } = [];
    public int? UniversityId { get; set; }
    public int? FacultyId { get; set; }
}
