using App.Core.Enums;

namespace App.Application.Contracts.Responses.DepartmentUsers;

public record DepartmentUserResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsDisabled { get; set; }
    public bool IsDeleted { get; set; }
    public List<string> Roles { get; set; } = [];
    public DateOnly BirthDate { get; set; }
    public Gender Gender { get; set; }
    public Religion Religion { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
}
