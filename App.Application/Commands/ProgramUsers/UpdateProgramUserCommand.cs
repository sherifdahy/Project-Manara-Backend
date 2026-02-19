namespace App.Application.Commands.ProgramUsers;

public record UpdateProgramUserCommand : IRequest<Result>
{
    public string Name { get; set; } = string.Empty;
    public string SSN { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsDisabled { get; set; } = false;

    public string Gender { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public decimal GPA { get; set; }
    public string Status { get; set; } = string.Empty;
    public int AcademicLevel { get; set; }

    public List<string> Roles { get; set; } = [];

    public int UserId { get; set; }

}
