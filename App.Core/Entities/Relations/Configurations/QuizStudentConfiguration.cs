using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MappingOfManaraaProject.Entities.Relations.Configurations;

public class QuizStudentConfiguration : IEntityTypeConfiguration<SemesterSubjectLectureDoctor>
{
    public void Configure(EntityTypeBuilder<SemesterSubjectLectureDoctor> builder)
    {
        builder.HasKey(x =>new {x.SemesterId,x.SubjectId,x.LectureId,x.DoctorId});
    }
}
