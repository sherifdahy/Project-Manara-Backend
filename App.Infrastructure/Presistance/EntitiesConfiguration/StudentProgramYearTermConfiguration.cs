using App.Core.Entities.Relations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class StudentProgramYearTermConfiguration : IEntityTypeConfiguration<StudentProgramYearTerm>
{
    public void Configure(EntityTypeBuilder<StudentProgramYearTerm> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.UserId, x.ProgramId, x.YearId, x.TermId })
               .IsUnique();

        builder.HasOne(x => x.YearTerm)
               .WithMany(y => y.StudentProgramYearTerms)
               .HasForeignKey(x => new { x.YearId, x.TermId });
    }
}
