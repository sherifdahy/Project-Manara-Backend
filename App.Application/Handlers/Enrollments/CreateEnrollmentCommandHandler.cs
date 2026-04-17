using App.Application.Commands.Enrollments;
using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Enrollments;
using App.Core.Entities.Relations;
using Microsoft.AspNetCore.Identity;

namespace App.Application.Handlers.Enrollments;

public class CreateEnrollmentCommandHandler(IUnitOfWork unitOfWork
    ,UserErrors userErrors
    ,ProgramErrors programErrors
    ,YearErrors yearErrors
    ) : IRequestHandler<CreateEnrollmentCommand, Result<EnrollmentResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserErrors _userErrors = userErrors;
    private readonly ProgramErrors _programErrors = programErrors;
    private readonly YearErrors _yearErrors = yearErrors;

    public async Task<Result<EnrollmentResponse>> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
    {

        var student = await _unitOfWork.Students.GetByIdAsync(request.UserId);

        if (student == null)
            return Result.Failure<EnrollmentResponse>(_userErrors.NotFound);


        var program = await _unitOfWork.Programs
            .FindAsync(x => x.Id == request.ProgramId && x.Department.FacultyId==student.FacultyId,x=>x.Include(x=>x.Department.Faculty));

        if(program==null)
            return Result.Failure<EnrollmentResponse>(_programErrors.NotFound);


        var year = await _unitOfWork.AcademicYears.FindAsync(x=>x.Id==request.YearId && x.FacultyId==student.FacultyId);

        if (year == null)
            return Result.Failure<EnrollmentResponse>(_yearErrors.NotFound);


        var termIsExists = await _unitOfWork.Terms.IsExistAsync(x=>x.Id==request.TermId);

        if(!termIsExists)
            return Result.Failure<EnrollmentResponse>(_yearErrors.TermNotFound);


        var Entity = request.Adapt<StudentProgramYearTerm>();

        await _unitOfWork.StudentProgramYearTerms.AddAsync(Entity);
        await _unitOfWork.SaveAsync();

        return Result.Success<EnrollmentResponse>(Entity.Adapt<EnrollmentResponse>());

    }
}
