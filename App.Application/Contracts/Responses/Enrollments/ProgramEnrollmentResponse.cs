namespace App.Application.Contracts.Responses.Enrollments;

public record ProgramEnrollmentResponse
(
    int Id,
    string YearName,
    string TermName,
    string StudentName,
    int StudentId,
    bool IsDeleted
);
