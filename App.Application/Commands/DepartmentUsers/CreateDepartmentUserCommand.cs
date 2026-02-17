using App.Application.Contracts.Responses.DepartmentUsers;

namespace App.Application.Commands.DepartmentUsers;

public record CreateDepartmentUserCommand : IRequest<Result<DepartmentUserResponse>>
{
    public string Name { get; set; } = string.Empty;
    public string SSN { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsDisabled { get; set; }
    public List<string> Roles { get; set; } = [];
    public int DepartmentId { get; set; }
}
