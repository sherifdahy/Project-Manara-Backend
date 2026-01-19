namespace App.Core.Entities.University;
public class FAQ
{
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = default!;
}
