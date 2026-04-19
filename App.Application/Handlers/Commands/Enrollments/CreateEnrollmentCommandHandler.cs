using App.Application.Commands.Enrollments;
using App.Application.Contracts.Responses.Enrollments;
using App.Core.Entities.Relations;

namespace App.Application.Handlers.Commands.Enrollments;

public class CreateEnrollmentCommandHandler(IUnitOfWork unitOfWork
    ,UserErrors userErrors
    ,ProgramErrors programErrors
    ,YearErrors yearErrors
    ,EnrollmentErrors enrollmentErrors
    ) : IRequestHandler<CreateEnrollmentCommand, Result<EnrollmentResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserErrors _userErrors = userErrors;
    private readonly ProgramErrors _programErrors = programErrors;
    private readonly YearErrors _yearErrors = yearErrors;
    private readonly EnrollmentErrors _enrollmentErrors = enrollmentErrors;

    public async Task<Result<EnrollmentResponse>> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
    {

        var student = await _unitOfWork.Students.GetByIdAsync(request.UserId,cancellationToken);

        if (student == null)
            return Result.Failure<EnrollmentResponse>(_userErrors.NotFound);


        var program = await _unitOfWork.Programs
            .FindAsync(x => x.Id == request.ProgramId && x.Department.FacultyId==student.FacultyId,x=>x.Include(x=>x.Department.Faculty),cancellationToken);

        if(program==null)
            return Result.Failure<EnrollmentResponse>(_programErrors.NotFound);


        var year = await _unitOfWork.AcademicYears.FindAsync(x=>x.Id==request.YearId && x.FacultyId==student.FacultyId,null,cancellationToken);

        if (year == null)
            return Result.Failure<EnrollmentResponse>(_yearErrors.NotFound);


        var term = await _unitOfWork.Terms.FindAsync(x=>x.Id==request.TermId,null,cancellationToken);

        if(term==null)
            return Result.Failure<EnrollmentResponse>(_yearErrors.TermNotFound);


        var duplicateProgram = await _unitOfWork.StudentProgramYearTerms
            .IsExistAsync(x => x.UserId == request.UserId
                           && x.ProgramId == request.ProgramId);

        if (duplicateProgram)
            return Result.Failure<EnrollmentResponse>(_enrollmentErrors.DuplicatedEnrollment);


        var duplicateYearTerm = await _unitOfWork.StudentProgramYearTerms
            .IsExistAsync(x => x.UserId == request.UserId
                           && x.YearId == request.YearId
                           && x.TermId == request.TermId);

        if (duplicateYearTerm)
            return Result.Failure<EnrollmentResponse>(_enrollmentErrors.AlreadyEnrolledInThisYearTerm);

        var entity = request.Adapt<StudentProgramYearTerm>();

        await _unitOfWork.StudentProgramYearTerms.AddAsync(entity);
        await _unitOfWork.SaveAsync();


        var response = new EnrollmentResponse(entity.Id,program.Name,year.Name,term.Name,student.UserId,false);

        return Result.Success(response);

    }
}
