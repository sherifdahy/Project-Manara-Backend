using App.Application.Contracts.Responses.Departments;

namespace App.Application.Commands.Departments;

public record CreateDepartmentCommand : IRequest<Result<DepartmentResponse>>
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string HeadOfDepartment { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int FacultyId { get; set; }
}
