using App.Application.Contracts.Responses.DepartmentUsers;
using App.Core.Enums;

namespace App.Application.Commands.DepartmentUsers;

public record CreateDepartmentUserCommand : IRequest<Result<DepartmentUserResponse>>
{
    public int DepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public Gender Gender { get; set; }
    public Religion Religion { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsDisabled { get; set; }
    public List<string> Roles { get; set; } = [];
}
