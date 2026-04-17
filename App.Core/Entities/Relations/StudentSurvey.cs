using App.Core.Entities.Assessment;
using App.Core.Entities.Personnel;

namespace MappingOfManaraaProject.Entities.Relations;

public class StudentSurvey
{
    public int StudentId { get; set; }
    public int SurveyId { get; set; }

    public Student Student { get; set; } = default!;
    public Survey Survey { get; set; } = default!;
}
