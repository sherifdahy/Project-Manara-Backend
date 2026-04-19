using App.Core.Entities.Relations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class ProgramSubjectPeriodDayConfiguration : IEntityTypeConfiguration<ProgramSubjectPeriodDay>
{
    public void Configure(EntityTypeBuilder<ProgramSubjectPeriodDay> builder)
    {
        builder.HasKey(x => new { x.ProgramId, x.SubjectId, x.PeriodId, x.DayId });
    }
}
