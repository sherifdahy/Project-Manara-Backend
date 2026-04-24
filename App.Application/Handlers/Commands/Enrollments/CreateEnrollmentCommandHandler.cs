using App.Application.Commands.Enrollments;
using App.Application.Contracts.Responses.Enrollments;
using App.Core.Entities.Relations;

namespace App.Application.Handlers.Commands.Enrollments;

public class CreateEnrollmentCommandHandler(IUnitOfWork unitOfWork
    ,UserErrors userErrors
    ,ProgramErrors programErrors
    ,YearErrors yearErrors
    ,EnrollmentErrors enrollmentErrors
    ) : IRequestHandler<CreateEnrollmentCommand, Result<List<ProgramEnrollmentResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserErrors _userErrors = userErrors;
    private readonly ProgramErrors _programErrors = programErrors;
    private readonly YearErrors _yearErrors = yearErrors;
    private readonly EnrollmentErrors _enrollmentErrors = enrollmentErrors;

    public async Task<Result<List<ProgramEnrollmentResponse>>> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
    {

        var uniqueStudentIds = request.StudentIds.Distinct().ToList();

        var program = await _unitOfWork.Programs
            .FindAsync(x => x.Id == request.ProgramId,
                       x => x.Include(x => x.Department.Faculty),
                       cancellationToken);

        if (program == null)
            return Result.Failure<List<ProgramEnrollmentResponse>>(_programErrors.NotFound);


        var year = await _unitOfWork.AcademicYears
            .FindAsync(x => x.Id == request.YearId
                         && x.FacultyId == program.Department.FacultyId,
                       null, cancellationToken);

        if (year == null)
            return Result.Failure<List<ProgramEnrollmentResponse>>(_yearErrors.NotFound);

        var term = await _unitOfWork.Terms
            .FindAsync(x => x.Id == request.TermId, null, cancellationToken);

        if (term == null)
            return Result.Failure<List<ProgramEnrollmentResponse>>(_yearErrors.TermNotFound);


        var students = await _unitOfWork.Students
            .FindAllAsync(x => uniqueStudentIds.Contains(x.UserId)
                            && x.FacultyId == program.Department.FacultyId,
                            x=>x.Include(x=>x.User),
                            cancellationToken);


        var existingEnrollments = await _unitOfWork.StudentProgramYearTerms
            .FindAllAsync(x => uniqueStudentIds.Contains(x.UserId)
                            && (x.ProgramId == request.ProgramId
                                || (x.YearId == request.YearId
                                    && x.TermId == request.TermId)),
                         cancellationToken);


        var entitles = new List<StudentProgramYearTerm>();

        foreach (var studentId in uniqueStudentIds)
        {
            var student = students.FirstOrDefault(s => s.UserId == studentId);

            if (student == null)
                return Result.Failure<List<ProgramEnrollmentResponse>>(_userErrors.NotFound);

            if (existingEnrollments.Any(e => e.UserId == studentId
                                          && e.ProgramId == request.ProgramId))
                return Result.Failure<List<ProgramEnrollmentResponse>>(
                    _enrollmentErrors.DuplicatedEnrollment);

            if (existingEnrollments.Any(e => e.UserId == studentId
                                          && e.YearId == request.YearId
                                          && e.TermId == request.TermId))
                return Result.Failure<List<ProgramEnrollmentResponse>>(
                    _enrollmentErrors.AlreadyEnrolledInThisYearTerm);

            entitles.Add(new StudentProgramYearTerm
            {
                ProgramId = program.Id,
                TermId = term.Id,
                YearId = year.Id,
                UserId = studentId
            });
        }


        await _unitOfWork.StudentProgramYearTerms.AddRangeAsync(entitles);
        await _unitOfWork.SaveAsync();


        var response = entitles.Select(x => new ProgramEnrollmentResponse(
            x.Id,
            year.Name,
            term.Name,
            students.First(s => s.UserId == x.UserId).User.Name,
            x.UserId,
            x.IsDeleted
        )).ToList();


        return Result.Success(response);


    }
}
