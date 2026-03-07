using App.Application.Contracts.Responses.ProgramUsers;

namespace App.Application.Queries.ProgramUsers;

public record GetAllProgramUsersByProgramIdQuery : IRequest<Result<PaginatedList<ProgramUserResponse>>>
{
    public bool? IncludeDisabled { get; set; }
    public RequestFilters Filters { get; set; } = default!;
    public int ProgramId { get; set; }
}
