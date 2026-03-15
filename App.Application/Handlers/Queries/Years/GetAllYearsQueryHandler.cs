using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Years;
using App.Application.Queries.Departments;
using App.Application.Queries.Years;

namespace App.Application.Handlers.Queries.Years;

public class GetAllYearsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllYearsQuery, Result<List<YearResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<YearResponse>>> Handle(GetAllYearsQuery request, CancellationToken cancellationToken)
    {
        var years = await _unitOfWork.AcademicYears
            .FindAllAsync(x => x.FacultyId == request.FacultyId
                && (!x.IsDeleted || (request.IncludeDisabled.HasValue && request.IncludeDisabled.Value)), null, cancellationToken);

        var response = years.Adapt<List<YearResponse>>();
        return Result.Success(response);
    }
}

