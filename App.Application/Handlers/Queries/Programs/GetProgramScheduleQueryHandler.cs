using App.Application.Contracts.Responses.ProgramSchedules;
using App.Application.Queries.Programs;

namespace App.Application.Handlers.Queries.Programs;

public class GetProgramScheduleQueryHandler(IUnitOfWork unitOfWork, ProgramErrors programErrors) : IRequestHandler<GetProgramScheduleQuery, Result<ProgramScheduleResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ProgramErrors _programErrors = programErrors;
    public async Task<Result<ProgramScheduleResponse>> Handle(GetProgramScheduleQuery request, CancellationToken cancellationToken)
    {
        #region Check Program
        if (await _unitOfWork.Programs.GetByIdAsync(request.ProgramId) is null)
            return Result.Failure<ProgramScheduleResponse>(_programErrors.NotFound);
        #endregion

        var existingSchedules = await _unitOfWork.ProgramSchedules.FindAllAsync(x => x.ProgramId == request.ProgramId && x.Subject.IsDeleted != true && x.Period.IsDeleted != true,x=>x.Include(d=>d.Subject), cancellationToken);

        var response = new ProgramScheduleResponse()
        {
            Schedules = existingSchedules.Adapt<List<ScheduleItemResponse>>()
        };

        return Result.Success(response);
    }
}
