using App.Core.Entities.Relations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class ProgramUserProgramYearTermConfiguration : IEntityTypeConfiguration<ProgramUserProgramYearTerm>
{
    public void Configure(EntityTypeBuilder<ProgramUserProgramYearTerm> builder)
    {
        builder.HasKey(x=> new { x.ProgramId,x.YearTermId, x.UserId });
    }
}
