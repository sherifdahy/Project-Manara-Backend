using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MappingOfManaraaProject.Entities.Relations.Configurations;

public class SubjectSemesterDoctorConfiguration : IEntityTypeConfiguration<SubjectSemesterDoctor>
{
    public void Configure(EntityTypeBuilder<SubjectSemesterDoctor> builder)
    {
    }
}
