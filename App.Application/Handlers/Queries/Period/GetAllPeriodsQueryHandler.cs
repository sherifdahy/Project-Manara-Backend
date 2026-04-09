using App.Application.Contracts.Responses.Periods;
using App.Application.Queries.Periods;

namespace App.Application.Handlers.Queries.Period;

public class GetAllPeriodsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllPeriodsQuery, Result<List<PeriodResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<PeriodResponse>>> Handle(GetAllPeriodsQuery request, CancellationToken cancellationToken)
    {
        var periods = await _unitOfWork.Periods
            .FindAllGroupedAsync(
                criteria: x => x.FacultyId == request.FacultyId
                            && (!x.IsDeleted || (request.IncludeDisabled.HasValue
                                              && request.IncludeDisabled.Value)),
                groupBy: x => new { x.StartTime, x.EndTime, x.IsDeleted },
                select: g => new PeriodResponse(
                    g.Key.StartTime,
                    g.Key.EndTime,
                    g.Key.IsDeleted
                ),
                cancellationToken
            );

        return Result.Success(periods.ToList());
    }
}

