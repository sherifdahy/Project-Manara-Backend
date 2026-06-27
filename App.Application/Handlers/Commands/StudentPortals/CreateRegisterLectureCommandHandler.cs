using App.Application.Commands.Departments;
using App.Application.Commands.StudentPortals;
using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.StudentPortals;
using App.Application.Contracts.Responses.StudentsPortal;
using App.Application.Errors;
using App.Core.Entities.Relations;

namespace App.Application.Handlers.Commands.StudentPortals;

public class CreateRegisterLectureCommandHandler(IUnitOfWork unitOfWork,YearErrors yearErrors,ProgramUserErrors programUserErrors,RegistrationErrors registrationErrors) 
    : IRequestHandler<CreateRegisterLectureCommand, Result<RegisterLectureResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly YearErrors _yearErrors = yearErrors;
    private readonly ProgramUserErrors _programUserErrors = programUserErrors;
    private readonly RegistrationErrors _registrationErrors = registrationErrors;

    public async Task<Result<RegisterLectureResponse>> Handle(CreateRegisterLectureCommand request, CancellationToken cancellationToken)
    {


        #region LectureExisting
        var lectureSchedule = await _unitOfWork.LectureSchedules.GetByIdAsync(request.LectureScheduleId);

        if (lectureSchedule == null)
            return Result.Failure<RegisterLectureResponse>(_registrationErrors.lectureScheduleNotFound);
        #endregion

        #region UserExisting
        var programUser = await _unitOfWork.Students
            .FindAsync(fu => fu.UserId == request.UserId);

        if (programUser == null)
            return Result.Failure<RegisterLectureResponse>(_programUserErrors.NotFound);
        #endregion

        #region Program
        var studentPrograms = await _unitOfWork.StudentProgramYearTerms
        .FindAllAsync(
            x => x.UserId == request.UserId
                && (!x.IsDeleted),
            q => q.Include(x => x.Program)
                  .Include(x => x.YearTerm).ThenInclude(yt => yt.Year)
                  .Include(x => x.YearTerm).ThenInclude(yt => yt.Term),
            cancellationToken);

        if (studentPrograms == null || studentPrograms.Count() == 0)
            return Result.Failure<RegisterLectureResponse>(_programUserErrors.HasNoProgram);


        var currentStudentProgramId = studentPrograms
            .OrderByDescending(e => e.YearTerm.Year.StartDate)
            .ThenByDescending(e => e.YearTerm.TermId)
            .FirstOrDefault()?.ProgramId;

        if (currentStudentProgramId != lectureSchedule.ProgramId)
            return Result.Failure<RegisterLectureResponse>(_registrationErrors.WrongProgram); 
        #endregion

        #region Years And Active Term
        var years = await _unitOfWork.AcademicYears
            .FindAllAsync(x => x.FacultyId == programUser.FacultyId, cancellationToken);

        if (years == null)
            return Result.Failure<RegisterLectureResponse>(_yearErrors.NotFound);

        var activeYearTerm = await _unitOfWork.YearTerms.FindAsync(x => x.IsActive && years.Contains(x.Year));

        if(activeYearTerm!.YearId!=lectureSchedule.YearId || activeYearTerm.TermId!=lectureSchedule.TermId)
            return Result.Failure<RegisterLectureResponse>(_registrationErrors.WrongTermOrYear);

        #endregion

        #region Filter01

        var programSubjects = await _unitOfWork.ProgramSubjects
            .FindAllAsync(
                x => x.ProgramId == currentStudentProgramId,
                cancellationToken: cancellationToken);

        var isValidProgram = programSubjects.Any(ps =>
            ps.ProgramId == lectureSchedule.ProgramId &&
            ps.SubjectId == lectureSchedule.SubjectId);


        if(!isValidProgram)
            return Result.Failure<RegisterLectureResponse>(_registrationErrors.InvalidSubject);


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

        var isValidRegistration =

            // Student took this subject before and failed
            studentRegistrations.Any(sr =>
                sr.LectureSchedule.SubjectId == lectureSchedule.SubjectId &&
                sr.GPA < 2 &&
                sr.GPA > 0)

            ||

            // Student never took it and subject has no prerequisites
            (
                !studentRegistrations.Any(sr =>
                    sr.LectureSchedule.SubjectId == lectureSchedule.SubjectId)
                &&
                !subjectsWithPrerequisites.Contains(lectureSchedule.SubjectId)
            )

            ||

            // Student never took it and passed all prerequisites
            (
                !studentRegistrations.Any(sr =>
                    sr.LectureSchedule.SubjectId == lectureSchedule.SubjectId)
                &&
                subjectsWithPrerequisites.Contains(lectureSchedule.SubjectId)
                &&
                prerequisites
                    .Where(p => p.SubjectId == lectureSchedule.SubjectId)
                    .All(p =>
                        studentRegistrations.Any(sr =>
                            sr.LectureSchedule.SubjectId == p.PrerequisiteId &&
                            sr.GPA >= 2))
            );

        if(!isValidRegistration)
            return Result.Failure<RegisterLectureResponse>(_registrationErrors.InvalidRegistration);


        #endregion

        #region Duplicated
        var isLectureRegistrationExists = await _unitOfWork.LectureRegistrations
            .IsExistAsync(x => x.LectureScheduleId == request.LectureScheduleId && x.StudentId == request.UserId);

        if (isLectureRegistrationExists)
            return Result.Failure<RegisterLectureResponse>(_registrationErrors.DuplicatedRegistration);
        #endregion

        #region MaxSlot
        var numberOfRegistration = await _unitOfWork.LectureRegistrations
            .CountAsync(x => x.LectureScheduleId == request.LectureScheduleId && x.GPA == 0);

        if (numberOfRegistration >= lectureSchedule.MaxSlots)
            return Result.Failure<RegisterLectureResponse>(_registrationErrors.MaxSlotFinish);
        #endregion


        #region Saving 

        var lectureRegistration = new LectureRegistration()
        {
            LectureScheduleId = request.LectureScheduleId,
            StudentId = request.UserId,
            GPA = 0
        };


        await _unitOfWork.LectureRegistrations.AddAsync(lectureRegistration);
        await _unitOfWork.SaveAsync();
        return Result.Success<RegisterLectureResponse>(lectureRegistration.Adapt<RegisterLectureResponse>());
        #endregion
    }
}