using App.Application.Contracts.Responses.Subjects;

namespace App.Application.Commands.Subjects;

public record CreateSubjectCommand : IRequest<Result<SubjectResponse>>
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CreditHours { get; set; }
    public int FacultyId { get; set; }
    public ICollection<int> PrerequisiteIds { get; set; } = new HashSet<int>();
}
