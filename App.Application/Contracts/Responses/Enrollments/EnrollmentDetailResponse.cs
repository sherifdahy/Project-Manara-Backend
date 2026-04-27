namespace App.Application.Contracts.Responses.Enrollments;

public record EnrollmentDetailResponse
(
    int Id,
    int TermId,
    int YearId,
    int ProgramId
);

