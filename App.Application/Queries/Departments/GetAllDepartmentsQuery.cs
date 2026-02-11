using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Faculties;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Departments;

public record GetAllDepartmentsQuery : IRequest<Result<List<DepartmentResponse>>>
{
    public bool? IncludeDisabled { get; set; }
    public int FacultyId { get; set; }

    public GetAllDepartmentsQuery(bool includeDisabled, int facultyId)
    {
        IncludeDisabled = includeDisabled;
        FacultyId = facultyId;
    }
}
