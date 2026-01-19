namespace App.Application.Commands.Faculties;

public record ToggleStatusFacultyCommand(int Id) : IRequest<Result>;