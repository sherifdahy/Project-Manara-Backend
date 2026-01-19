using App.Core.Entities.Personnel;
using App.Core.Entities.Teaching;

namespace MappingOfManaraaProject.Entities.Relations;

public class LectureStudentAttendance
{
    public int LectureId { get; set; }
    public int StudentId { get; set; }
    public int AttendanceId { get; set; }

    public Lecture Lecture { get; set; } = default!;
    public Student Student { get; set; } = default!;
    public Attendance Attendance { get; set; } = default!;
}
