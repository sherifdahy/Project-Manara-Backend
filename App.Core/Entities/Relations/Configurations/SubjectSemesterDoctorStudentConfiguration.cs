using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MappingOfManaraaProject.Entities.Relations.Configurations;

public class SubjectSemesterDoctorStudentConfiguration : IEntityTypeConfiguration<SubjectSemesterDoctorStudent>
{
    public void Configure(EntityTypeBuilder<SubjectSemesterDoctorStudent> builder)
    {
    }
}
