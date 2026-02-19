namespace App.Application.Commands.ProgramUsers;

public record ToggleStatusProgramUserCommand : IRequest<Result>
{
    public int Id { get; set; }
}
