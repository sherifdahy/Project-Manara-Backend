

using App.Application.Contracts.Responses.Periods;
using App.Application.Queries.Periods;

namespace App.Application.Handlers.Queries.Period;

public class GetPeriodQueryHandler(IUnitOfWork unitOfWork
    ,FacultyErrors facultyErrors,PeriodErrors periodErrors) : IRequestHandler<GetPeriodQuery, Result<PeriodResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly FacultyErrors _facultyErrors = facultyErrors;
    private readonly PeriodErrors _periodErrors = periodErrors;

    public async Task<Result<PeriodResponse>> Handle(GetPeriodQuery request, CancellationToken cancellationToken)
    {
        var isFacultyExists = await _unitOfWork.Fauclties.IsExistAsync(x => x.Id == request.FacultyId);

        if (!isFacultyExists)
            return Result.Failure<PeriodResponse>(_facultyErrors.NotFound);


        var periods = await _unitOfWork.Periods
            .FindAllAsync(x => x.StartTime == request.StartTime && x.EndTime == request.EndTime && x.FacultyId == request.FacultyId, cancellationToken);

        if (!periods.Any())
            return Result.Failure<PeriodResponse>(_periodErrors.NotFound);

        return Result.Success(new PeriodResponse(request.StartTime, request.EndTime, periods.ToList()[0].IsDeleted));
    }
}
