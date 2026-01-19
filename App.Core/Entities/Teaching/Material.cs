namespace App.Core.Entities.Teaching;
public class Material
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int LectureId { get; set; }
    public Lecture Lecture { get; set; } = default!;
}
