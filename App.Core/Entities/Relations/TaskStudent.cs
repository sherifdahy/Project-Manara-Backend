using App.Core.Entities.Personnel;

namespace MappingOfManaraaProject.Entities.Relations;

public class TaskStudent
{
    public int TaskId { get; set; }
    public int StudentId { get; set; }
    public App.Core.Entities.Assessment.Task Task { get; set; } = default!;
    public Student Student { get; set; } = default!;
}
