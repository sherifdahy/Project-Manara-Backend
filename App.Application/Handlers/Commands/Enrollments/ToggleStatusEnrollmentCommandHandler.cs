using App.Application.Commands.Enrollments;
using App.Application.Errors;

namespace App.Application.Handlers.Commands.Enrollments;

public class ToggleStatusEnrollmentCommandHandler(IUnitOfWork unitOfWork,EnrollmentErrors enrollmentErrors) : IRequestHandler<ToggleStatusEnrollmentCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly EnrollmentErrors _enrollmentErrors = enrollmentErrors;


    public async Task<Result> Handle(ToggleStatusEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollmentEntity = await _unitOfWork.StudentProgramYearTerms
            .FindAsync(x => x.Id == request.Id,null, cancellationToken);

        if (enrollmentEntity == null)
            return Result.Failure(_enrollmentErrors.NotFound);

        enrollmentEntity.IsDeleted =!enrollmentEntity.IsDeleted;

        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}