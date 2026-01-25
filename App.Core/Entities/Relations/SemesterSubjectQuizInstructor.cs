using App.Core.Entities.Assessment;
using App.Core.Entities.Universities;

namespace MappingOfManaraaProject.Entities.Relations;
public class SemesterSubjectQuizInstructor
{
    public int SemesterId { get; set; }
    public int SubjectId { get; set; }
    public int QuizId { get; set; }
    public int InstructorId { get; set; }

    public Semester Semester { get; set; } = default!;
    public Subject Subject { get; set; } = default!;
    public Quiz Quiz { get; set; } = default!;
    public Instructor Instructor { get; set; } = default!;
}
