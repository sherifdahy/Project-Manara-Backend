using App.Application.Contracts.Responses.Faculties;
using App.Application.Contracts.Responses.FacultyUsers;

namespace App.Application.Queries.Faculties;

public record GetMyFacultyQuery : IRequest<Result<FacultyDetailResponse>> { }
