using App.Application.Contracts.Responses.Years;

namespace App.Application.Queries.Years;

public record GetYearQuery(int Id) : IRequest<Result<YearDetailResponse>>;
