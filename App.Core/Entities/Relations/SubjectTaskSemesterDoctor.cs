namespace MappingOfManaraaProject.Entities.Relations;

public class SubjectTaskSemesterDoctor
{
    public App.Core.Entities.Assessment.Task Task{ get; set; } = default!;
    public Subject Subject { get; set; } = default!;
}
