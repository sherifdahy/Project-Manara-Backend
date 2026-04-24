namespace App.Application.Contracts.Responses.Enrollments;


public record ProgramEnrollmentResponse
(
    int ProgramId,
    string ProgramName,
    List<ProgramEnrollmentItemResponse> Enrollments
);

public record ProgramEnrollmentItemResponse
(
    int Id,
    string YearName,
    string TermName,
    string StudentName,
    int StudentId,
    bool IsDeleted
);
