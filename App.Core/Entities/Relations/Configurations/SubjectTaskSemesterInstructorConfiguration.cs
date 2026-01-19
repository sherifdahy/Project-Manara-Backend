using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MappingOfManaraaProject.Entities.Relations.Configurations;

public class SubjectTaskSemesterInstructorConfiguration : IEntityTypeConfiguration<SubjectTaskSemesterInstructor>
{
    public void Configure(EntityTypeBuilder<SubjectTaskSemesterInstructor> builder)
    {
    }
}
