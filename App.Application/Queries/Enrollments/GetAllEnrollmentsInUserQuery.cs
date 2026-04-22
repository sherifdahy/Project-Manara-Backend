using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Enrollments;

namespace App.Application.Queries.Enrollments;

public record GetAllEnrollmentsInUserQuery : IRequest<Result<List<EnrollmentResponse>>>
{
    public bool? IncludeDisabled { get; set; }
    public int UserId { get; set; }

    public GetAllEnrollmentsInUserQuery(bool includeDisabled, int userId)
    {
        IncludeDisabled = includeDisabled;
        UserId = userId;
    }
}
