using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Enrollments;
using App.Application.Queries.Departments;
using App.Application.Queries.Enrollments;

namespace App.Application.Handlers.Queries.Enrollments;

public class GetEnrollmentQueryHandler(IUnitOfWork unitOfWork
    ,EnrollmentErrors enrollmentErrors) : IRequestHandler<GetEnrollmentQuery, Result<EnrollmentDetailResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly EnrollmentErrors _enrollmentErrors = enrollmentErrors;


    public async Task<Result<EnrollmentDetailResponse>> Handle(GetEnrollmentQuery request, CancellationToken cancellationToken)
    {
           var enrollment = await _unitOfWork.StudentProgramYearTerms
            .FindAsync(x => x.Id == request.id, null, cancellationToken);

        if (enrollment == null)
            return Result.Failure<EnrollmentDetailResponse>(_enrollmentErrors.NotFound);

        var response = enrollment.Adapt<EnrollmentDetailResponse>();

        return Result.Success(response);

    }
}
