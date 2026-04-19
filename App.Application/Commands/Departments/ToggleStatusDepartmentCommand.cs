namespace App.Application.Commands.Departments;

public record ToggleStatusDepartmentCommand(int Id) : IRequest<Result>;
