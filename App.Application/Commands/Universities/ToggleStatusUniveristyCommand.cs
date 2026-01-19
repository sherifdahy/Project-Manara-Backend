namespace App.Application.Commands.Universities;

public record ToggleStatusUniveristyCommand(int Id) : IRequest<Result>;
