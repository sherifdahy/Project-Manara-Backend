using App.Application.Contracts.Responses.FacultyUsers;

namespace App.Application.Queries.UniversityUsers;

public record GetUniversityUserQuery : IRequest<Result<FacultyUserResponse>>
{
    public int Id { get; set; }
}
