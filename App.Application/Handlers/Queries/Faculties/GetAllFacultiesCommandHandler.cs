using App.Application.Queries.Faculties;
using App.Application.Responses.Faculties;

namespace App.Application.Handlers.Queries.Faculties;

public class GetAllFacultiesCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllFacultiesQuery, Result<List<FacultyResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    async Task<Result<List<FacultyResponse>>> IRequestHandler<GetAllFacultiesQuery, Result<List<FacultyResponse>>>.Handle(GetAllFacultiesQuery request, CancellationToken cancellationToken)
    {
        var faculties = await _unitOfWork.Fauclties.FindAllAsync(x=> x.UniversityId == request.UniversityId && (!x.IsDeleted || (request.IncludeDisabled.HasValue && request.IncludeDisabled.Value)), null,cancellationToken);

        var response = faculties.Adapt<List<FacultyResponse>>();

        return Result.Success(response);
    }
}
