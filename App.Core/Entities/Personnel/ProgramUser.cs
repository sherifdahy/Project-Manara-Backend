using App.Core.Entities.Identity;

namespace App.Core.Entities.Personnel;

public class ProgramUser
{
    public string Gender { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public decimal GPA { get; set; }
    public string Status { get; set; } = string.Empty;
    public int AcademicLevel { get; set; }

    public int UserId { get; set; }
    public int ProgramId { get; set; }

    public Program Program { get; set; } = default!;
    public ApplicationUser User { get; set; } = default!;
}
