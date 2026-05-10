using App.Application.Contracts.Responses.Programs;
using App.Application.Queries.Programs;

namespace App.Application.Handlers.Queries.Programs;

public class GetLectureSchedulesQueryHandler(IUnitOfWork unitOfWork, YearErrors yearErrors) : IRequestHandler<GetLectureSchedulesQuery, Result<List<LectureScheduleItemResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly YearErrors _yearErrors = yearErrors;

    public async Task<Result<List<LectureScheduleItemResponse>>> Handle(GetLectureSchedulesQuery request, CancellationToken cancellationToken)
    {
        #region Get Active Year Term
        var currentFaculty = await _unitOfWork.Fauclties.FindAsync(x => x.Departments.Any(x => x.Programs.Any(x => x.Id == request.ProgramId)));

        var yearTerm = await _unitOfWork.YearTerms.FindAsync(x => x.Year.FacultyId == currentFaculty!.Id && x.IsActive);

        if (yearTerm == null)
            return Result.Failure<List<LectureScheduleItemResponse>>(_yearErrors.NoActiveYearTerm);
        #endregion

        var existingSchedules = await _unitOfWork.LectureSchedules
            .FindAllAsync(x => 
                            x.ProgramId == request.ProgramId && 
                            (x.YearId == yearTerm.YearId && x.TermId == yearTerm.TermId) 
                            && x.Subject.IsDeleted != true 
                            && x.Period.IsDeleted != true, x => x.Include(d => d.Subject).Include(x=>x.Doctor).ThenInclude(x=>x.User), cancellationToken);

        var response = existingSchedules.Adapt<List<LectureScheduleItemResponse>>();

        return Result.Success(response);
    }
}
