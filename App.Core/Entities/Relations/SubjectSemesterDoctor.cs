namespace MappingOfManaraaProject.Entities.Relations;

public class SubjectSemesterDoctor
{
    public int SubjectId { get; set; }
    public int SemesterId { get; set; }
    public int DoctorId { get; set; }

    public Subject Subject { get; set; } = default!;
    public Semester Semester { get; set; } = default!;
    public Doctor Doctor { get; set; } = default!;
}
