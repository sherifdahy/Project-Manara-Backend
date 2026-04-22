using App.Application.Commands.Periods;
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
            .IsExistAsync(x=> x.FacultyId==request.FacultyId  && ( x.StartTime == request.StartTime && x.EndTime==request.EndTime));

        if (isPeriodExists)
            return Result.Failure<PeriodResponse>(_periodErrors.DuplicatedPeriod);

        var entity = request.Adapt<Period>();

        await _unitOfWork.Periods.AddAsync(entity,cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);

        var response = entity.Adapt<PeriodResponse>();

        return Result.Success(response);
    }
}
