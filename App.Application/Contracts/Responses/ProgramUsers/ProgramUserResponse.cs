using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.ProgramUsers;

public record ProgramUserResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SSN { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsDisabled { get; set; } = false;

    public string Gender { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public decimal GPA { get; set; }
    public string Status { get; set; } = string.Empty;
    public int AcademicLevel { get; set; }

    public List<string> Roles { get; set; } = [];
}
