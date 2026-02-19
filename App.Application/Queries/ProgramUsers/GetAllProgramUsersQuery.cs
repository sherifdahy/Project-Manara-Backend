using App.Application.Contracts.Responses.ProgramUsers;

namespace App.Application.Queries.ProgramUsers;

public record GetAllProgramUsersQuery : IRequest<Result<PaginatedList<ProgramUserResponse>>>
{
    public bool? IncludeDisabled { get; set; }
    public RequestFilters Filters { get; set; } = default!;
    public int ProgramId { get; set; }
}
