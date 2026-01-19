namespace App.Application.Commands.Universities;

public record CreateUniversityCommand : IRequest<Result<UniversityResponse>>
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Website { get; init; } = string.Empty;
}
