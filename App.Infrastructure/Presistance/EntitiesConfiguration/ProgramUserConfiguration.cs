using App.Core.Entities.Personnel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class ProgramUserConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(fu => fu.UserId);

        builder.Property(fu => fu.UserId).IsRequired();
    }
}
