

using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Periods;

namespace App.Application.Queries.Periods;

public record GetAllPeriodsQuery : IRequest<Result<List<PeriodResponse>>>
{
    public bool? IncludeDisabled { get; set; }
    public int FacultyId { get; set; }

    public GetAllPeriodsQuery(bool includeDisabled, int facultyId)
    {
        IncludeDisabled = includeDisabled;
        FacultyId = facultyId;
    }
}
