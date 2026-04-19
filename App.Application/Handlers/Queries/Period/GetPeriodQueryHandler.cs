using App.Application.Contracts.Responses.Periods;
using App.Application.Queries.Periods;

namespace App.Application.Handlers.Queries.Period;

public class GetPeriodQueryHandler(IUnitOfWork unitOfWork,PeriodErrors periodErrors) : IRequestHandler<GetPeriodQuery, Result<PeriodResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly PeriodErrors _periodErrors = periodErrors;

    public async Task<Result<PeriodResponse>> Handle(GetPeriodQuery request, CancellationToken cancellationToken)
    {
        var period = _unitOfWork.Periods.FindAsync(x => x.Id == request.Id);

        if (period is null)
            return Result.Failure<PeriodResponse>(_periodErrors.NotFound);

        return Result.Success(period.Adapt<PeriodResponse>());
    }
}
