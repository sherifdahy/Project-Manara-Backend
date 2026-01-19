namespace MappingOfManaraaProject.Entities.Relations;

public class SemesterSectionDoctor
{
    public int SemesterId { get; set; }
    public int SectionId { get; set; }
    public int DoctorId { get; set; }

    public Semester Semester { get; set; } = default!;
    public Section Section { get; set; } = default!;
    public Doctor Doctor { get; set; } = default!;
}
