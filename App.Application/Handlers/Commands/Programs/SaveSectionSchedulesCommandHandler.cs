using App.Application.Commands.Programs;
using App.Application.Contracts.Responses.Programs;
using App.Core.Entities.Academic;
using App.Core.Entities.Relations;

namespace App.Application.Handlers.Commands.Programs;

public class SaveSectionSchedulesCommandHandler(
    IUnitOfWork unitOfWork,
    ProgramErrors programErrors,
    UserErrors userErrors,
    SubjectErrors subjectErrors,
    PeriodErrors periodErrors,
    YearErrors yearErrors,
    DayErrors dayErrors) : IRequestHandler<SaveSectionSchedulesCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ProgramErrors _programErrors = programErrors;
    private readonly UserErrors _userErrors = userErrors;
    private readonly SubjectErrors _subjectErrors = subjectErrors;
    private readonly PeriodErrors _periodErrors = periodErrors;
    private readonly DayErrors _dayErrors = dayErrors;
    private readonly YearErrors _yearErrors = yearErrors;
    public async Task<Result> Handle(SaveSectionSchedulesCommand request, CancellationToken cancellationToken)
    {
        #region Check Instructors
        var inputDepartmentUsersIds = request.Schedules
            .Select(x => x.InstructorId)
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

        #region Get Active Year Term
        var currentFaculty = await _unitOfWork.Fauclties.FindAsync(x => x.Departments.Any(x => x.Programs.Any(x => x.Id == request.ProgramId)));

        var yearTerm = await _unitOfWork.YearTerms.FindAsync(x => x.Year.FacultyId == currentFaculty!.Id);

        if (yearTerm == null)
            return Result.Failure<List<SectionScheduleItemResponse>>(_yearErrors.NoActiveYearTerm);
        #endregion

        #region Delete Schedules
        var existingSchedules = await _unitOfWork.SectionSchedules.FindAllAsync(x => x.ProgramId == request.ProgramId && x.YearId == yearTerm.YearId && x.TermId == yearTerm.TermId, cancellationToken);

        if (existingSchedules.Any())
            _unitOfWork.SectionSchedules.DeleteRange(existingSchedules);
        #endregion

        #region Creation of Schedules
        if (request.Schedules.Any())
        {
            var newSchedules = request.Schedules.Select(x =>
            {
                var sectionSchedule = x.Adapt<SectionSchedule>() ;

                sectionSchedule.ProgramId = request.ProgramId;
                sectionSchedule.YearId = yearTerm.YearId;
                sectionSchedule.TermId = yearTerm.TermId;

                return sectionSchedule;
            }).ToList();

            await _unitOfWork.SectionSchedules.AddRangeAsync(newSchedules, cancellationToken);
        }
        #endregion

        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
