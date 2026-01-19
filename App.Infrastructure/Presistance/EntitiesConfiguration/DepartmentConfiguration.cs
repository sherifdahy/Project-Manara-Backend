using App.Core.Entities.University;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {

        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(200);


        builder.Property(f => f.Code)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(f => f.Description)
            .HasMaxLength(1000);

        builder.Property(f => f.HeadOfDepartment)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(f => f.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(f => f.FacultyId)
            .IsRequired();
    }
}
