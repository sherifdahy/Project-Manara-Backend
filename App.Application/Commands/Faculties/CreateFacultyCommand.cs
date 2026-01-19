using App.Application.Responses.Faculties;
namespace App.Application.Commands.Faculties;

public record CreateFacultyCommand : IRequest<Result<FacultyResponse>>
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Website { get; init; } = string.Empty;
    public int UniversityId { get; init; }
}

