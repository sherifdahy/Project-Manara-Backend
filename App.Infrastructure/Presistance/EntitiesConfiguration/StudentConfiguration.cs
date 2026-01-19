using App.Core.Entities.Personnel;
using App.Core.Entities.University;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {

        builder.Property(f => f.Gender)
            .IsRequired()
            .HasMaxLength(1);


        builder.Property(f => f.NationalId)
            .IsRequired()
            .HasMaxLength(14);

        builder.Property(f => f.BirthDate)
            .IsRequired();

        builder.Property(f => f.EnrollmentDate)
            .IsRequired();

        builder.Property(f => f.GPA)
                .IsRequired()
                .HasMaxLength(100);

        builder.Property(f => f.Status)
            .IsRequired();


        builder.Property(f => f.AcademicLevel)
            .IsRequired();

        builder.HasIndex(s => s.UserId)
               .IsUnique();

        builder.Property(f => f.ProgramId)
            .IsRequired();
    }
}
