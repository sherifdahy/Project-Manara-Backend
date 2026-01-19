using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MappingOfManaraaProject.Entities.Relations.Configurations;

public class SemesterSectionDoctorConfiguration : IEntityTypeConfiguration<SemesterSectionDoctor>
{
    public void Configure(EntityTypeBuilder<SemesterSectionDoctor> builder)
    {
        builder.HasKey(x => new { x.SemesterId,x.SectionId,x.DoctorId });
    }
}
