namespace App.Core.Entities.Assessment;
public class Task
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public decimal MaxGrade { get; set; }
    public bool IsGroupTask { get; set; }
}
