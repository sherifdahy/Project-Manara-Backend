using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MappingOfManaraaProject.Entities.Relations.Configurations;

public class SubjectTaskSemesterDoctorConfiguration : IEntityTypeConfiguration<SubjectTaskSemesterDoctor>
{
    public void Configure(EntityTypeBuilder<SubjectTaskSemesterDoctor> builder)
    {
    }
}
