using App.Application.Contracts.Responses.Subjects;

namespace App.Application.Queries.Subjects;

public record GetAllSubjectsQuery : IRequest<Result<PaginatedList<SubjectResponse>>>
{
    public bool? IncludeDisabled { get; set; }
    public RequestFilters Filters { get; set; } = default!;
    public int FacultyId { get; set; }
}
