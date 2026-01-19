using App.Core.Entities.University;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class ProgramConfiguration : IEntityTypeConfiguration<Program>
{
    public void Configure(EntityTypeBuilder<Program> builder)
    {

        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(200);


        builder.Property(f => f.Code)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(f => f.Description)
            .HasMaxLength(1000);

        builder.Property(f => f.CreditHours)
                .IsRequired();

        builder.Property(f => f.DepartmentId)
            .IsRequired();
    }
}