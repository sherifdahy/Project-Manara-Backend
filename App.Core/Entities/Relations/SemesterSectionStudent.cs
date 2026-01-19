namespace MappingOfManaraaProject.Entities.Relations;

public class SemesterSectionStudent
{
    public int SemesterId { get; set; }
    public int SectionId { get; set; }
    public int StudentId { get; set; }

    public Semester Semester { get; set; } = default!;
    public Section Section { get; set; } = default!;
    public Student Student { get; set; } = default!;
}
