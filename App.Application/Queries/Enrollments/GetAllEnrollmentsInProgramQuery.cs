using App.Application.Contracts.Responses.DepartmentUsers;
using App.Application.Contracts.Responses.Enrollments;

namespace App.Application.Queries.Enrollments;

public record GetAllEnrollmentsInProgramQuery : IRequest<Result<PaginatedList<ProgramEnrollmentResponse>>>
{
    public bool? IncludeDisabled { get; set; }
    public RequestFilters Filters { get; set; } = default!;
    public int ProgramId { get; set; }
}

