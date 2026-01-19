using App.Core.Entities.Teaching;

namespace MappingOfManaraaProject.Entities.Relations;

public class SubjectMeetSemesterDoctor
{
    public int SubjectId { get; set; }
    public int MeetId { get; set; }
    public int SemesterId { get; set; }
    public int DoctorId { get; set; }

    public Subject Subject { get; set; } = default!;
    public Meet Meet { get; set; } = default!;
    public Semester Semester { get; set; } = default!;
    public Doctor Doctor { get; set; } = default!;
}
