using App.Core.Entities.Relations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class YearTermConfiguration : IEntityTypeConfiguration<YearTerm>
{
    public void Configure(EntityTypeBuilder<YearTerm> builder)
    {
        builder.HasKey(x => new {x.YearId,x.TermId});
    }
}
