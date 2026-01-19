namespace App.Core.Entities.Personnel;

public class Student
{
    public int Id { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public decimal GPA { get; set; }
    public string Status { get; set; } = string.Empty;
    public int AcademicLevel { get; set; }

    public int UserId { get; set; }
    public int ProgramId { get; set; }

    //public UniversityManagement.Program Program { get; set; } = default!;
}
