using App.Application.Commands.Periods;
using App.Application.Contracts.Responses.Periods;

namespace App.Application.Handlers.Commands.Periods;

public class UpdatePeriodCommandHandler(IUnitOfWork unitOfWork
    ,FacultyErrors facultyErrors,PeriodErrors periodErrors) : IRequestHandler<UpdatePeriodCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly FacultyErrors _facultyErrors = facultyErrors;
    private readonly PeriodErrors _periodErrors = periodErrors;


    public async Task<Result> Handle(UpdatePeriodCommand request, CancellationToken cancellationToken)
    {

        var isFacultyExists = await _unitOfWork.Fauclties.IsExistAsync(x => x.Id == request.FacultyId);

        if (!isFacultyExists)
            return Result.Failure(_facultyErrors.NotFound);


        var periods = await _unitOfWork.Periods
            .FindAllAsync(x => x.StartTime == request.OldStartTime && x.EndTime == request.OldEndTime && x.FacultyId == request.FacultyId,cancellationToken);

        if(!periods.Any())
            return Result.Failure(_periodErrors.NotFound);


        var periodIds = periods.Select(x => x.Id).ToList();

        var isDuplicated = await _unitOfWork.Periods
            .IsExistAsync(x => x.FacultyId == request.FacultyId
                            && !periodIds.Contains(x.Id)  
                            && (x.StartTime == request.StartTime
                            || x.EndTime == request.EndTime));

        if (isDuplicated)
            return Result.Failure(_periodErrors.DuplicatedPeriod);



        foreach(var period in periods)
        {
            period.StartTime = request.StartTime;
            period.EndTime = request.EndTime;
        }

        _unitOfWork.Periods.UpdateRange(periods);

        await _unitOfWork.SaveAsync();

        return Result.Success();


    }
}
