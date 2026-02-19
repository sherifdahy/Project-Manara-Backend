namespace App.Application.Contracts.Responses.UniversityUser;

public record UniversityUserResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SSN { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsDisabled { get; set; }
    public bool IsDeleted { get; set; }
    public List<string> Roles { get; set; } = [];
}
