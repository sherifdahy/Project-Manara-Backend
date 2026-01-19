using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MappingOfManaraaProject.Entities.Relations.Configurations;

public class SubjectMeetSemesterDoctorConfiguration : IEntityTypeConfiguration<SubjectMeetSemesterDoctor>
{
    public void Configure(EntityTypeBuilder<SubjectMeetSemesterDoctor> builder)
    {
    }
}
