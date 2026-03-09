using App.Application.Contracts.Responses.Subjects;

namespace App.Application.Queries.Subjects;

public record GetSubjectQuery : IRequest<Result<SubjectDetailResponse>>
{
    public int Id { get; set; }
}
