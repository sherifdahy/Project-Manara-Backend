using App.Application.Contracts.Responses.FacultyUsers;

namespace App.Application.Queries.UniversityUsers;

public record GetAllUniversityUsersQuery : IRequest<Result<List<FacultyUserResponse>>>
{
    public int UniversityId { get; set; }
}
