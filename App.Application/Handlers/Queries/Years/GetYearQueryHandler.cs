using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Years;
using App.Application.Queries.Departments;
using App.Application.Queries.Years;

namespace App.Application.Handlers.Queries.Years;

public class GetYearQueryHandler(IUnitOfWork unitOfWork,YearErrors yearErrors) : IRequestHandler<GetYearQuery, Result<YearDetailResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly YearErrors _yearErrors = yearErrors;

    public async Task<Result<YearDetailResponse>> Handle(GetYearQuery request, CancellationToken cancellationToken)
    {
        var year = await _unitOfWork.AcademicYears.FindAsync(x => x.Id == request.Id, null, cancellationToken);

        if (year == null)
            return Result.Failure<YearDetailResponse>(_yearErrors.NotFound);

        var activeYearTerm = await _unitOfWork.YearTerms.FindAsync(x => x.YearId == year.Id && x.IsActive == true);

        if (activeYearTerm == null)
            return Result.Failure<YearDetailResponse>(_yearErrors.NotFound);

        var response = new YearDetailResponse(year.Name, year.StartDate, year.EndDate, activeYearTerm.TermId);

        return Result.Success(response);
    }
}
