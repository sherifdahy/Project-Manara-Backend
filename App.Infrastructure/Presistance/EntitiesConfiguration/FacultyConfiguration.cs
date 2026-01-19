using App.Core.Entities.University;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> builder)
    {
        builder.HasIndex(f => f.Name).IsUnique();

        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(f => f.Description)
            .HasMaxLength(1000);


        builder.Property(f => f.Address)
            .HasMaxLength(200);

        builder.Property(f => f.Email)
            .HasMaxLength(200);

        builder.Property(f => f.Website)
            .HasMaxLength(200);

        builder.Property(f => f.UniversityId)
            .IsRequired();
    }
}
