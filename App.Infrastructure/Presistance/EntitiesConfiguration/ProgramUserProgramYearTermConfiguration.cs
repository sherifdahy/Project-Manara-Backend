using App.Core.Entities.Relations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class ProgramUserProgramYearTermConfiguration : IEntityTypeConfiguration<StudentProgramYearTerm>
{
    public void Configure(EntityTypeBuilder<StudentProgramYearTerm> builder)
    {
        builder.HasKey(x=> new { x.ProgramId,x.YearId,x.TermId, x.UserId });
    }
}
