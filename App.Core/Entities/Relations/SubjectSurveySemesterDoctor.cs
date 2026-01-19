using App.Core.Entities.Assessment;

namespace MappingOfManaraaProject.Entities.Relations;

public class SubjectSurveySemesterDoctor
{
    public int SubjectId { get; set; }
    public int SurveyId { get; set; }
    public int SemesterId { get; set; }
    public int DoctorId { get; set; }

    public Subject Subject { get; set; } = default!;
    public Survey Survey { get; set; } = default!;
    public Semester Semester { get; set; } = default!;
    public Doctor Doctor { get; set; } = default!;
}
