

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class SubjectPrerequisiteConfiguration : IEntityTypeConfiguration<SubjectPrerequisite>
{
    public void Configure(EntityTypeBuilder<SubjectPrerequisite> builder)
    {
        builder.HasKey(sp => new { sp.SubjectId, sp.PrerequisiteId });

        builder.HasOne(sp => sp.Subject)
              .WithMany(s => s.Prerequisites)
              .HasForeignKey(sp => sp.SubjectId)
              .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(sp => sp.Prerequisite)
              .WithMany()
              .HasForeignKey(sp => sp.PrerequisiteId)
              .OnDelete(DeleteBehavior.Restrict);
    }
}
