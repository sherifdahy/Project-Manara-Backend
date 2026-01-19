namespace MappingOfManaraaProject.Entities.Relations;

public class SemesterExamSeatingStudent
{
    public int SemesterId { get; set; }
    public int ExamSeatingId { get; set; }
    public int StudentId { get; set; }

    public Semester Semester { get; set; } = default!;
    public ExamSeating ExamSeating { get; set; } = default!;
    public Student Student { get; set; } = default!;
}
