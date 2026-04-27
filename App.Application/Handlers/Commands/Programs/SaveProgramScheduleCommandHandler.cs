using App.Application.Commands.Programs;
using App.Core.Entities.Relations;

namespace App.Application.Handlers.Commands.Programs;

public class SaveProgramScheduleCommandHandler(IUnitOfWork unitOfWork, ProgramErrors programErrors,UserErrors userErrors, SubjectErrors subjectErrors, PeriodErrors periodErrors, DayErrors dayErrors) : IRequestHandler<SaveProgramScheduleCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ProgramErrors _programErrors = programErrors;
    private readonly UserErrors _userErrors = userErrors;
    private readonly SubjectErrors _subjectErrors = subjectErrors;
    private readonly PeriodErrors _periodErrors = periodErrors;
    private readonly DayErrors _dayErrors = dayErrors;
    public async Task<Result> Handle(SaveProgramScheduleCommand request, CancellationToken cancellationToken)
    {
        #region Check Program
        if (await _unitOfWork.Programs.GetByIdAsync(request.ProgramId) is null)
            return Result.Failure(_programErrors.NotFound);
        #endregion

        #region Check Doctors and Instructors
        var inputDepartmentUsersIds = request.Schedules
            .SelectMany(x => new int?[] { x.InstructorId, x.DoctorId })
            .Where(x => x.HasValue)
            .Select(x => x.Value)
            .Distinct();

        var existsDepartmentUsersIds = (await _unitOfWork.DepartmentUsers.FindAllAsync(x => inputDepartmentUsersIds.Contains(x.UserId), cancellationToken)).Select(x => x.UserId);

        var notFoundDepartmentUsersIds = inputDepartmentUsersIds.Except(existsDepartmentUsersIds);

        if (notFoundDepartmentUsersIds.Any())
            return Result.Failure(_userErrors.NotFound);
        #endregion

        #region Check Subjects
        var existsSubjects = (await _unitOfWork.Subjects.FindAllAsync(x => request.Schedules.Select(x => x.SubjectId).Distinct().Contains(x.Id), cancellationToken)).Select(x => x.Id);

        var notFoundSubjectIds = request.Schedules.Select(x => x.SubjectId).Except(existsSubjects);

        if (notFoundSubjectIds.Any())
            return Result.Failure(_subjectErrors.NotFound);

        #endregion

        #region Check Days 
        var inputDayIds = request.Schedules.Select(x => x.DayId).Distinct();

        var existsDayIds = (await _unitOfWork.Days.FindAllAsync(x => inputDayIds.Contains(x.Id), cancellationToken)).Select(x => x.Id);

        var notFoundDayIds = inputDayIds.Except(existsDayIds);

        if (notFoundDayIds.Any())
            return Result.Failure(_dayErrors.NotFound);
        #endregion

        #region Check Period
        var inputPeriodIds = request.Schedules.Select(x => x.PeriodId).Distinct();

        var existsPeriodIds = (await _unitOfWork.Periods.FindAllAsync(x => inputPeriodIds.Contains(x.Id), cancellationToken)).Select(x => x.Id);

        var notFoundPeriodIds = inputPeriodIds.Except(existsPeriodIds);

        if (notFoundPeriodIds.Any())
            return Result.Failure(_periodErrors.NotFound);
        #endregion

        #region Delete Schedules
        var existingSchedules = await _unitOfWork.ProgramSchedules.FindAllAsync(x => x.ProgramId == request.ProgramId, cancellationToken);

        if (existingSchedules.Any())
            _unitOfWork.ProgramSchedules.DeleteRange(existingSchedules);
        #endregion

        #region Creation of Schedules
        if (request.Schedules.Any())
        {
            var newSchedules = request.Schedules.Select(x =>
            new ProgramSubjectPeriodDay
            {
                SubjectId = x.SubjectId,
                PeriodId = x.PeriodId,
                DayId = x.DayId,
                DoctorId = x.DoctorId,
                InstructorId = x.InstructorId,
                ProgramId = request.ProgramId,
            }).ToList();

            await _unitOfWork.ProgramSchedules.AddRangeAsync(newSchedules, cancellationToken);
        }
        #endregion

        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
