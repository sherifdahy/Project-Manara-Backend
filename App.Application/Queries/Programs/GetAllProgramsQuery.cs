using App.Application.Contracts.Responses.Programs;

namespace App.Application.Queries.Programs;

public class GetAllProgramsQuery : IRequest<Result<List<ProgramResponse>>>
{
    public bool? IncludeDisabled { get; set; }
    public int DepartmentId { get; set; }

    public GetAllProgramsQuery(bool includeDisabled, int departmentId)
    {
        IncludeDisabled = includeDisabled;
        DepartmentId = departmentId;
    }
}
