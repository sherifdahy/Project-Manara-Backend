namespace App.Application.Commands.Enrollments;

public record ToggleStatusEnrollmentCommand(int Id) : IRequest<Result>;
