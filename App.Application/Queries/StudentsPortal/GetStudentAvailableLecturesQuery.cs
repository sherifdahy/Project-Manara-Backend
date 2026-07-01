using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.StudentsPortal;

namespace App.Application.Queries.StudentsPortal;

public record GetStudentAvailableLecturesQuery(int StudentId)
    : IRequest<Result<List<StudentPortalDetailResponse>>>;