namespace App.Core.Entities.Teaching;
public class Lecture
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime LectureDate { get; set; }
}
