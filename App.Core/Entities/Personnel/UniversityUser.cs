using App.Core.Entities.Identity;

namespace App.Core.Entities.Personnel;

public class UniversityUser
{
    public int UserId { get; set; }
    public int UniversityId { get; set; }

    public University University { get; set; } = default!;
    public ApplicationUser User { get; set; } = default!;
}
