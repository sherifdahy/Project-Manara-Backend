namespace App.Application.Contracts.Requests.FacultyUsers;

public record FacultyUserRequest
{
    public string Name { get; set; } = string.Empty;
    public string SSN { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsDisabled { get; set; } = false;
    public List<string> Roles { get; set; } = [];
}
