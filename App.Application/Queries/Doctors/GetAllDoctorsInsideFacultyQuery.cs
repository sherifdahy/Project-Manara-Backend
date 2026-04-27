using App.Application.Contracts.Responses.DepartmentUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Doctors;

public record GetAllDoctorsInsideFacultyQuery : IRequest<Result<PaginatedList<DepartmentUserResponse>>>
{
    public int FacultyId { get; set; }
    public RequestFilters Filters { get; set; } = default!;

    public GetAllDoctorsInsideFacultyQuery(int facultyId , RequestFilters filters)
    {
        FacultyId = facultyId;
        Filters = filters;
    }
}
