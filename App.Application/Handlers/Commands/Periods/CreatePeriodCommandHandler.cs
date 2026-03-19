using App.Application.Commands.Periods;
using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Periods;
using App.Core.Entities.Academic;

namespace App.Application.Handlers.Commands.Periods;

public class CreatePeriodCommandHandler(IUnitOfWork unitOfWork
    ,FacultyErrors facultyErrors
    ,PeriodErrors periodErrors) : IRequestHandler<CreatePeriodCommand, Result<PeriodResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly FacultyErrors _facultyErrors = facultyErrors;
    private readonly PeriodErrors _periodErrors = periodErrors;


    public async Task<Result<PeriodResponse>> Handle(CreatePeriodCommand request, CancellationToken cancellationToken)
    {
        var isFacultyExists = await _unitOfWork.Fauclties.IsExistAsync(f => f.Id == request.FacultyId);

        if (!isFacultyExists)
            return Result.Failure<PeriodResponse>(_facultyErrors.NotFound);


        var isPeriodExists = await _unitOfWork.Periods
            .IsExistAsync(x=> x.FacultyId==request.FacultyId  && ( x.StartTime == request.StartTime || x.EndTime==request.EndTime));

        if (isPeriodExists)
            return Result.Failure<PeriodResponse>(_periodErrors.DuplicatedPeriod);

        List<Period> periods = new List<Period>();

        for(int i = 1; i <= 7; i++)
        {
            periods.Add(new Period { StartTime = request.StartTime, EndTime = request.EndTime, DayId = i ,FacultyId=request.FacultyId});
        }

        await _unitOfWork.Periods.AddRangeAsync(periods);

        await _unitOfWork.SaveAsync();

        var response = new PeriodResponse(
            request.StartTime,
            request.EndTime,
            false
        );

        return Result.Success(response);
    }
}
