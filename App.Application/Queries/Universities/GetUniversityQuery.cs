using App.Application.Contracts.Responses.Universities;

namespace App.Application.Queries.Universities;

public record GetUniversityQuery(int Id) : IRequest<Result<UniversityDetailResponse>>;