using App.Application.Contracts.Responses.DepartmentUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Instructors;

public record GetAllInstructorsInsideFacultyQuery : IRequest<Result<PaginatedList<DepartmentUserResponse>>>
{
    public int FacultyId { get; set; }
}
