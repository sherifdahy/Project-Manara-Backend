using App.Application.Commands.Enrollments;
using App.Application.Contracts.Responses.Enrollments;

namespace App.Application.Handlers.Enrollments;

public class UpdateEnrollmentCommandHandler(IUnitOfWork unitOfWork
    ,EnrollmentErrors enrollmentErrors,YearErrors yearErrors) : IRequestHandler<UpdateEnrollmentCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly EnrollmentErrors _enrollmentErrors = enrollmentErrors;
    private readonly YearErrors _yearErrors = yearErrors;


    public async Task<Result> Handle(UpdateEnrollmentCommand request, CancellationToken cancellationToken)
    {

        var enrollmentEntity = await _unitOfWork.StudentProgramYearTerms
            .FindAsync(x=>x.Id==request.Id,x=>x.Include(x=>x.User.Faculty),cancellationToken);

        if (enrollmentEntity == null)
            return Result.Failure(_enrollmentErrors.NotFound);


        var isProgramInSameFaculty = await _unitOfWork.Programs
            .FindAsync(x => x.Id == request.ProgramId 
                        && x.Department.FacultyId == enrollmentEntity.User.FacultyId,x=>x.Include(x=>x.Department.Faculty),cancellationToken);

        if(isProgramInSameFaculty ==null)
            return Result.Failure(_enrollmentErrors.DifferentProgram);


        var isYearInSameFaculty = await _unitOfWork.AcademicYears
            .FindAsync(x => x.Id == request.YearId
                        && x.FacultyId == enrollmentEntity.User.FacultyId,null,cancellationToken);

        if (isYearInSameFaculty == null)
            return Result.Failure(_enrollmentErrors.DifferentProgram);



        var termIsExists = await _unitOfWork.Terms.IsExistAsync(x => x.Id == request.TermId);

        if (!termIsExists)
            return Result.Failure<EnrollmentResponse>(_yearErrors.TermNotFound);




        enrollmentEntity.ProgramId=request.ProgramId;
        enrollmentEntity.YearId=request.YearId;
        enrollmentEntity.TermId=request.TermId;


        _unitOfWork.StudentProgramYearTerms.Update(enrollmentEntity);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();

    }
}