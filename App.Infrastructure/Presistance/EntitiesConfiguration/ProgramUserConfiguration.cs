using App.Core.Entities.Personnel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class ProgramUserConfiguration : IEntityTypeConfiguration<ProgramUser>
{
    public void Configure(EntityTypeBuilder<ProgramUser> builder)
    {

        builder.HasKey(fu => fu.UserId);

        builder.Property(fu => fu.UserId)
            .IsRequired();


        builder.Property(fu => fu.ProgramId)
            .IsRequired();


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

    }
}
