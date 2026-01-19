namespace MappingOfManaraaProject.Entities.Relations;

public class SubjectTaskSemesterInstructor
{
    public int SubjectId { get; set; }
    public int TaskId { get; set; }
    public int SemesterId { get; set; }
    public int InstructorId { get; set; }


    public Subject Subject { get; set; } = default!;
    public App.Core.Entities.Assessment.Task Task { get; set; } = default!;
    public Semester Semester { get; set; } = default!;
    public Instructor Instructor { get; set; } = default!;
}
