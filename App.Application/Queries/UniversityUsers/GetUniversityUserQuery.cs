using App.Application.Contracts.Responses.FacultyUsers;
using App.Application.Contracts.Responses.UniversityUser;

namespace App.Application.Queries.UniversityUsers;

public record GetUniversityUserQuery : IRequest<Result<UniversityUserResponse>>
{
    public int Id { get; set; }
}
