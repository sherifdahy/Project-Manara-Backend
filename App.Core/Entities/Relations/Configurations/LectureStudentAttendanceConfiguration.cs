using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MappingOfManaraaProject.Entities.Relations.Configurations;

public class LectureStudentAttendanceConfiguration : IEntityTypeConfiguration<LectureStudentAttendance>
{
    public void Configure(EntityTypeBuilder<LectureStudentAttendance> builder)
    {
        builder.HasKey(x => new { x.LectureId, x.StudentId ,x.AttendanceId});
    }
}
