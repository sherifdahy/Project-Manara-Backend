namespace App.Core.Entities.Teaching;
public class Meet
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public string Description { get; set; } = string.Empty;
}
