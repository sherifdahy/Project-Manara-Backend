
namespace App.Application.Commands.Subjects;

public record ToggleStatusSubjectCommand(int Id) : IRequest<Result>;
