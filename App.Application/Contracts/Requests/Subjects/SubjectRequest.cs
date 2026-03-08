

namespace App.Application.Contracts.Requests.Subjects;

public record SubjectRequest
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CreditHours { get; set; }
    public ICollection<int> PrerequisiteIds { get; set; } = new HashSet<int>();

}
