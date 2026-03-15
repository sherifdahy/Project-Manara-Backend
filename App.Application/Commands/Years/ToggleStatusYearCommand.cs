

namespace App.Application.Commands.Years;

public record ToggleStatusYearCommand(int Id) : IRequest<Result>;
