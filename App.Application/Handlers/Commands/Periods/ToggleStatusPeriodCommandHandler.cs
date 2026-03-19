

using App.Application.Commands.Departments;
using App.Application.Commands.Periods;
using App.Application.Errors;

namespace App.Application.Handlers.Commands.Periods;

public class ToggleStatusPeriodCommandHandler(IUnitOfWork unitOfWork
    ,FacultyErrors facultyErrors,PeriodErrors periodErrors) : IRequestHandler<ToggleStatusPeriodCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly FacultyErrors _facultyErrors = facultyErrors;
    private readonly PeriodErrors _periodErrors = periodErrors;

    public async Task<Result> Handle(ToggleStatusPeriodCommand request, CancellationToken cancellationToken)
    {

        var isFacultyExists = await _unitOfWork.Fauclties.IsExistAsync(x => x.Id == request.FacultyId);

        if (!isFacultyExists)
            return Result.Failure(_facultyErrors.NotFound);


        var periods = await _unitOfWork.Periods
            .FindAllAsync(x => x.StartTime == request.StartTime && x.EndTime == request.EndTime && x.FacultyId == request.FacultyId, cancellationToken);

        if (!periods.Any())
            return Result.Failure(_periodErrors.NotFound);


        foreach (var period in periods)
        {
           period.IsDeleted=!period.IsDeleted;
        }

        _unitOfWork.Periods.UpdateRange(periods);

        await _unitOfWork.SaveAsync();

        return Result.Success();
    }
}
