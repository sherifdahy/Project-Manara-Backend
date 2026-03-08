using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Subjects;

public record UpdateSubjectCommand : IRequest<Result>
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CreditHours { get; set; }
    public ICollection<int> PrerequisiteIds { get; set; } = new HashSet<int>();
}
