using App.Application.Responses.Faculties;

namespace App.Application.Queries.Faculties;

public record GetAllFacultiesQuery : IRequest<Result<List<FacultyResponse>>>
{
    public bool? IncludeDisabled { get; set; }
    public int UniversityId { get; set; }

    public GetAllFacultiesQuery(bool includeDisabled,int universityId)
    {
        IncludeDisabled = includeDisabled;
        UniversityId = universityId;
    }
};
