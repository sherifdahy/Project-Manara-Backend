using App.Application.Contracts.Responses.DepartmentUsers;
using App.Application.Contracts.Responses.FacultyUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.DepartmentUsers;

public record GetAllDepartmentUsersQuery : IRequest<Result<PaginatedList<DepartmentUserResponse>>>
{
    public bool? IncludeDisabled { get; set; }
    public RequestFilters Filters { get; set; } = default!;
    public int DepartmentId { get; set; }
}
