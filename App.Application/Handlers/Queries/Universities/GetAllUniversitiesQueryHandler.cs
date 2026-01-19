using App.Application.Queries.Universities;

namespace App.Application.Handlers.Queries.Universities;

public class GetAllUniversitiesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllUniverisitiesQuery, Result<List<UniversityResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<List<UniversityResponse>>> Handle(GetAllUniverisitiesQuery request, CancellationToken cancellationToken)
    {
        var universities = await _unitOfWork.Universities.FindAllAsync(x=> !x.IsDeleted || (request.IncludeDisabled.HasValue && request.IncludeDisabled.Value), null,cancellationToken);

        var response = universities.Adapt<List<UniversityResponse>>();

        return Result.Success(response);
    }
}
