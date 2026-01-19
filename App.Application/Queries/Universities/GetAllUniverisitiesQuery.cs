using App.Application.Responses.Universities;

namespace App.Application.Queries.Universities;

public record GetAllUniverisitiesQuery
(
    bool? IncludeDisabled
    
) : IRequest<Result<List<UniversityResponse>>>;

