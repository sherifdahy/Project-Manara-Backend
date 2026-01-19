namespace App.Core.Entities.University;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CreditHours { get; set; }

    public int ProgramId { get; set; }
    public Program Program { get; set; } = default!;

    public ICollection<FAQ> FAQs { get; set; } = new HashSet<FAQ>();
}

