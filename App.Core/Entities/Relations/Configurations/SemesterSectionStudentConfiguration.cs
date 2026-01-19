using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MappingOfManaraaProject.Entities.Relations.Configurations;

public class SemesterSectionStudentConfiguration : IEntityTypeConfiguration<SemesterSectionStudent>
{
    public void Configure(EntityTypeBuilder<SemesterSectionStudent> builder)
    {
        builder.HasKey(x => new { x.SemesterId,x.SectionId,x.StudentId});
    }
}
