using App.Application.Contracts.Responses.DepartmentUsers;
using App.Application.Contracts.Responses.FacultyUsers;

namespace App.Application.Queries.DepartmentUsers;

public record GetDepartmentUserQuery : IRequest<Result<DepartmentUserResponse>>
{
    public int Id { get; set; }
}

