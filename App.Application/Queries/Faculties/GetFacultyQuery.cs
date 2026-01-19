using App.Application.Responses.Faculties;

namespace App.Application.Queries.Faculties;

public record GetFacultyQuery(int Id) : IRequest<Result<FacultyDetailResponse>>;