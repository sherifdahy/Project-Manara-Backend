using App.Application.Contracts.Responses.FacultyUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.FacultyUsers;

public record GetAllFacultyUsersQuery : IRequest<Result<PaginatedList<FacultyUserResponse>>>
{
    public bool? IncludeDisabled { get; set; }
    public RequestFilters Filters { get; set; } = default!;
    public int FacultyId { get; set; }
}
