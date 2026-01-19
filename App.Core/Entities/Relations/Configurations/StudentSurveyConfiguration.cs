using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MappingOfManaraaProject.Entities.Relations.Configurations;

public class StudentSurveyConfiguration : IEntityTypeConfiguration<StudentSurvey>
{
    public void Configure(EntityTypeBuilder<StudentSurvey> builder)
    {
    }
}
