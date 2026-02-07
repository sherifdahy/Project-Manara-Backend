using App.Core.Entities.Identity;

namespace App.Core.Entities.Personnel;

public class FacultyUser
{
    public int UserId { get; set; }
    public int FacultyId { get; set; }

    public Faculty Faculty { get; set; } = default!;
    public ApplicationUser User { get; set; } = default!;

}
