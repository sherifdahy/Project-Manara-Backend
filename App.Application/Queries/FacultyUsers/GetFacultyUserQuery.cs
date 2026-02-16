using App.Application.Contracts.Responses.FacultyUsers;

namespace App.Application.Queries.FacultyUsers;

public record GetFacultyUserQuery : IRequest<Result<FacultyUserResponse>>
{
    public int Id { get; set; }
}
