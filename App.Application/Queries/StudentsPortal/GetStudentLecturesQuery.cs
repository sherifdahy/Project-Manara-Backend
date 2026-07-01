using App.Application.Contracts.Responses.StudentsPortal;

namespace App.Application.Queries.StudentsPortal;

public record GetStudentLecturesQuery(int StudentId)
    : IRequest<Result<List<StudentLecturesResponse>>>;
