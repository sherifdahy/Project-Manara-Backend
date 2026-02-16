using App.Application.Contracts.Responses.FacultyUsers;
using App.Application.Contracts.Responses.UniversityUser;

namespace App.Application.Queries.UniversityUsers;

public record GetAllUniversityUsersQuery : IRequest<Result<List<UniversityUserResponse>>>
{
    public int UniversityId { get; set; }
}
