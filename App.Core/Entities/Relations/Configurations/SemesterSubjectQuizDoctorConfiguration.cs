using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MappingOfManaraaProject.Entities.Relations.Configurations
{
    public class SemesterSubjectQuizDoctorConfiguration : IEntityTypeConfiguration<SemesterSubjectQuizDoctor>
    {
        public void Configure(EntityTypeBuilder<SemesterSubjectQuizDoctor> builder)
        {
            builder.HasKey(x => new { x.SemesterId,x.SubjectId,x.QuizId,x.DoctorId });
        }
    }
}
