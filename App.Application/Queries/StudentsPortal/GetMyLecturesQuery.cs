using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.StudentsPortal;

namespace App.Application.Queries.StudentsPortal;

public record GetMyLecturesQuery(int UserId)
    : IRequest<Result<List<StudentPortalDetailResponse>>>;