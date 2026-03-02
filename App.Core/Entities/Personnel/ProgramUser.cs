using App.Core.Entities.Identity;

namespace App.Core.Entities.Personnel;

public class ProgramUser
{
    public int UserId { get; set; }
    public int ProgramId { get; set; }

    public Program Program { get; set; } = default!;
    public ApplicationUser User { get; set; } = default!;
}
