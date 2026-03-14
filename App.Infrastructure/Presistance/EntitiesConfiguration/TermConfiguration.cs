using App.Core.Consts;
using App.Core.Entities.Academic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class TermConfiguration : IEntityTypeConfiguration<Term>
{
    public void Configure(EntityTypeBuilder<Term> builder)
    {
        builder.HasData(
            new Term { Id = DefaultTerms.FirstTerm.Id, Name = DefaultTerms.FirstTerm.Name },
            new Term { Id = DefaultTerms.SecondTerm.Id, Name = DefaultTerms.SecondTerm.Name },
            new Term { Id = DefaultTerms.SummerTerm.Id, Name = DefaultTerms.SummerTerm.Name }
        );
    }
}