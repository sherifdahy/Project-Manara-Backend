using App.Core.Entities.Teaching;

namespace MappingOfManaraaProject.Entities.Relations;

public class SemesterSubjectLectureDoctor
{
    public int SemesterId { get; set; }
    public int SubjectId { get; set; }
    public int LectureId { get; set; }
    public int DoctorId { get; set; }

    public Semester Semester { get; set; } = default!;
    public Subject Subject { get; set; } = default!;
    public Lecture Lecture { get; set; } = default!;
    public Doctor Doctor { get; set; } = default!;
}
