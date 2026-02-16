using App.Application.Contracts.Responses.FacultyUsers;
using App.Application.Contracts.Responses.UniversityUser;

namespace App.Application.Commands.UniversityUsers;

public record CreateUniversityUserCommand : IRequest<Result<UniversityUserResponse>>
{
    public string Name { get; set; } = string.Empty;
    public string SSN { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsDisabled { get; set; }
    public List<string> Roles { get; set; } = [];
    public int UniversityId { get; set; }
}
