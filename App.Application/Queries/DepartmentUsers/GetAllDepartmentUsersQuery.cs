using App.Application.Contracts.Responses.DepartmentUsers;

namespace App.Application.Queries.DepartmentUsers;

public record GetAllDepartmentUsersQuery : IRequest<Result<PaginatedList<DepartmentUserResponse>>>
{
    public bool? IncludeDisabled { get; set; }
    public RequestFilters Filters { get; set; } = default!;
    public int DepartmentId { get; set; }
}
