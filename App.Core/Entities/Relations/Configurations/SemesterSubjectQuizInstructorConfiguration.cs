using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MappingOfManaraaProject.Entities.Relations.Configurations
{
    public class SemesterSubjectQuizInstructorConfiguration : IEntityTypeConfiguration<SemesterSubjectQuizInstructor>
    {
        public void Configure(EntityTypeBuilder<SemesterSubjectQuizInstructor> builder)
        {
            builder.HasKey(x => new { x.SemesterId,x.SubjectId,x.QuizId,x.InstructorId });
        }
    }
}
