using App.Application.Contracts.Responses.Periods;

namespace App.Application.Queries.Periods;

public record GetPeriodQuery : IRequest<Result<PeriodResponse>>
{
    public int Id { get; set; }
}
