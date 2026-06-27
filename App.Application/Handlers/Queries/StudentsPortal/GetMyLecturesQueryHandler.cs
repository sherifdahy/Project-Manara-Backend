using App.Application.Contracts.Responses.StudentsPortal;
using App.Application.Queries.DepartmentUsers;
using App.Application.Queries.StudentsPortal;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace App.Application.Handlers.Queries.StudentsPortal;

public class GetMyLecturesQueryHandler(UserManager<ApplicationUser> userManager
    ,IUnitOfWork unitOfWork,ProgramUserErrors programUserErrors,YearErrors yearErrors) 
    : IRequestHandler<GetMyLecturesQuery, Result<List<StudentPortalDetailResponse>>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ProgramUserErrors _programUserErrors = programUserErrors;
    private readonly YearErrors _yearErrors = yearErrors;

    public async Task<Result<List<StudentPortalDetailResponse>>> Handle(GetMyLecturesQuery request, CancellationToken cancellationToken)
    {


        #region Checking
        //First Get The UserId And Check That this user Is student and if not a student return error Then Get the facultyId Of this users
        var programUser = await _unitOfWork.Students
            .FindAsync(fu => fu.UserId == request.UserId);

        if (programUser == null)
            return Result.Failure<List<StudentPortalDetailResponse>>(_programUserErrors.NotFound);
        #endregion


        #region Years And Active Term
        //Then Get the years in this faculty then search in the year term table using this years and then get the yearTerm that is Active
        var years = await _unitOfWork.AcademicYears
            .FindAllAsync(x => x.FacultyId == programUser.FacultyId, cancellationToken);

        if (years == null)
            return Result.Failure<List<StudentPortalDetailResponse>>(_yearErrors.NotFound);

        var activeYearTerm = await _unitOfWork.YearTerms.FindAsync(x => x.IsActive && years.Contains(x.Year));
        #endregion


        #region GetStudentCurrentProgram
        //Get The Current Program That is assign to this student
        var studentPrograms = await _unitOfWork.StudentProgramYearTerms
        .FindAllAsync(
            x => x.UserId == request.UserId
                && (!x.IsDeleted),
            q => q.Include(x => x.Program)
                  .Include(x => x.YearTerm).ThenInclude(yt => yt.Year)
                  .Include(x => x.YearTerm).ThenInclude(yt => yt.Term),
            cancellationToken);

        if (studentPrograms == null || studentPrograms.Count() == 0)
            return Result.Failure<List<StudentPortalDetailResponse>>(_programUserErrors.HasNoProgram);


        var currentStudentProgramId = studentPrograms
            .OrderByDescending(e => e.YearTerm.Year.StartDate)
            .ThenByDescending(e => e.YearTerm.TermId)
            .FirstOrDefault()?.ProgramId;
        #endregion


        #region GetAllLectureSchendles
        //Then Search In the LectureSchendels table by (ProgramId,YearId,TermId)
        var lectureSchendels = await _unitOfWork.LectureSchedules
            .FindAllAsync(
                x => x.ProgramId == currentStudentProgramId
                    && x.YearId == activeYearTerm!.YearId
                    && x.TermId == activeYearTerm!.TermId,
                q => q
                    .Include(x => x.Doctor.User)
                    .Include(x => x.Subject)
                    .Include(x => x.Period)
                    .Include(x => x.Day),
                cancellationToken);
        #endregion


        #region Filter01
        //ApplyFilters (later)
        //1.0 Is subject is belong to program that student belong to (current Program)

        var programSubjects = await _unitOfWork.ProgramSubjects
        .FindAllAsync(
            x => x.ProgramId == currentStudentProgramId,
            cancellationToken: cancellationToken);

        lectureSchendels = lectureSchendels
            .Where(ls => programSubjects.Any(ps =>
                ps.ProgramId == ls.ProgramId &&
                ps.SubjectId == ls.SubjectId))
            .ToList();
        #endregion


        #region Filter02

        var studentRegistrations = await _unitOfWork.LectureRegistrations
            .FindAllAsync(
                x => x.StudentId == request.UserId,
                q => q
                    .Include(x => x.LectureSchedule)
                        .ThenInclude(ls => ls.Subject),
                cancellationToken: cancellationToken);

        var prerequisites = await _unitOfWork.SubjectPrerequisites
            .FindAllAsync(
                x => x.Subject.FacultyId == programUser.FacultyId,
                q => q.Include(x => x.Subject),
                cancellationToken: cancellationToken);

        var subjectsWithPrerequisites = prerequisites
            .Select(x => x.SubjectId)
            .ToHashSet();

        lectureSchendels = lectureSchendels
            .Where(ls =>

                // Student took this subject before and failed
                studentRegistrations.Any(sr =>
                    sr.LectureSchedule.SubjectId == ls.SubjectId &&
                    sr.GPA < 2 && sr.GPA >0)

                ||

                // Student never took it and subject has no prerequisites
                (
                    !studentRegistrations.Any(sr =>
                        sr.LectureSchedule.SubjectId == ls.SubjectId)
                    &&
                    !subjectsWithPrerequisites.Contains(ls.SubjectId)
                )

                ||

                // Student never took it and passed all prerequisites
                (
                    !studentRegistrations.Any(sr =>
                        sr.LectureSchedule.SubjectId == ls.SubjectId)
                    &&
                    subjectsWithPrerequisites.Contains(ls.SubjectId)
                    &&
                    prerequisites
                        .Where(p => p.SubjectId == ls.SubjectId)
                        .All(p =>
                            studentRegistrations.Any(sr =>
                                sr.LectureSchedule.SubjectId == p.PrerequisiteId
                                && sr.GPA >= 2))
                )
            )
            .ToList();

        #endregion


        #region Filter02 Explanations
        /*
    if(Std Take the subject Mean that it has a row in LectureRegisterationTable){

        if(GPA>2)
        {
            Remove# Mean Remove This lectureSchendel
        }


        else
        {
            Add# Mean Keep This lectureSchendel
        }
    }

    else{
        if(subject has pre)
        {
           if(the student pass the request of this subject)
                {Add# Mean Add This lectureSchendel}
            else
                {Remove#Remove# remove This lectureSchendel}
        }

        else 
        {
          {Add# Mean Add This lectureSchendel}

        }


    }

 */
        #endregion


        #region Map And Return
        //Return
        var result = lectureSchendels
        .Select(x => new StudentPortalDetailResponse(
            x.Id,
            new SubjectResponse(
                x.Subject.Id,
                x.Subject.Name
            ),
            new DepartmentUserResponse(
                x.Doctor.UserId,
                x.Doctor.User.Name
            ),
            new PeriodResponse(
                x.Period.Id,
                x.Period.StartTime,
                x.Period.EndTime
            ),
            new DayResponse(
                x.Day.Id,
                x.Day.Value
            )
        ))
        .ToList();

        return Result.Success(result); 
        #endregion

    }
}
