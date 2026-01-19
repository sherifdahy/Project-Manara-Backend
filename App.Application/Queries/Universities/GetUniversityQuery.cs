using App.Application.Responses.Universities;

namespace App.Application.Queries.Universities;

public record GetUniversityQuery(int Id) : IRequest<Result<UniversityDetailResponse>>;