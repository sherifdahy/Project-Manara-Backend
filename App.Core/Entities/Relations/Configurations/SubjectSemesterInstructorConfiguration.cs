using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MappingOfManaraaProject.Entities.Relations.Configurations;

public class SubjectSemesterInstructorConfiguration : IEntityTypeConfiguration<SubjectSemesterInstructor>
{
    public void Configure(EntityTypeBuilder<SubjectSemesterInstructor> builder)
    {
    }
}
