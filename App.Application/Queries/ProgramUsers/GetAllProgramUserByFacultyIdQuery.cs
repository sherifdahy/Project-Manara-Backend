using App.Application.Contracts.Responses.ProgramUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.ProgramUsers;

public record GetAllProgramUserByFacultyIdQuery : IRequest<Result<PaginatedList<ProgramUserResponse>>>
{
    public bool? IncludeDisabled { get; set; }
    public RequestFilters Filters { get; set; } = default!;
    public int FacultyId { get; set; }
}
