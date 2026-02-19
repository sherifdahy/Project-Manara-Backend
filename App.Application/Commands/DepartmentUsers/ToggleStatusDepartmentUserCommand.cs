namespace App.Application.Commands.DepartmentUsers;

public record ToggleStatusDepartmentUserCommand : IRequest<Result>
{
    public int Id { get; set; }
}
