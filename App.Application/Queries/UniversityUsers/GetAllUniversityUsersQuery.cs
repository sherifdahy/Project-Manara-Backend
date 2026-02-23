using App.Application.Contracts.Responses.FacultyUsers;
using App.Application.Contracts.Responses.UniversityUser;

namespace App.Application.Queries.UniversityUsers;

public record GetAllUniversityUsersQuery : IRequest<Result<PaginatedList<UniversityUserResponse>>>
{
    public int UniversityId { get; set; }
    public bool? IncludeDisabled { get; set; }
    public RequestFilters Filters { get; set; } = default!;
}
