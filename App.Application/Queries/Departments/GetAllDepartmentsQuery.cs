using App.Application.Contracts.Responses.Departments;

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
