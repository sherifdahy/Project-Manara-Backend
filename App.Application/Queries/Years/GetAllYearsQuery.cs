using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Years;

namespace App.Application.Queries.Years;

public class GetAllYearsQuery : IRequest<Result<List<YearResponse>>>
{
    public bool? IncludeDisabled { get; set; }
    public int FacultyId { get; set; }

    public GetAllYearsQuery(bool includeDisabled, int facultyId)
    {
        IncludeDisabled = includeDisabled;
        FacultyId = facultyId;
    }
}
