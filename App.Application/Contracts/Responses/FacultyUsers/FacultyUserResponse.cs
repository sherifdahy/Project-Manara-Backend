using App.Application.Contracts.Responses.Roles;

namespace App.Application.Contracts.Responses.FacultyUsers;

public record FacultyUserResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SSN { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsDisabled { get; set; }
    public bool IsDeleted { get; set; }
    public List<string> Roles { get; set; } = [];
}
