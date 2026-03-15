using App.Application.Contracts.Responses.Years;
using App.Application.Queries.Years;

namespace App.Application.Handlers.Queries.Years;

public class GetAllTermsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllTermsQuery, Result<List<TermResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    public async Task<Result<List<TermResponse>>> Handle(GetAllTermsQuery request, CancellationToken cancellationToken)
    {
        var terms = await _unitOfWork.Terms.GetAllAsync();

        var response = terms.Adapt<List<TermResponse>>();

        return Result.Success(response);
    }
}
