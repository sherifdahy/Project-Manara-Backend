using App.Core.Entities.Academic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class LectureScheduleConfiguration : IEntityTypeConfiguration<LectureSchedule>
{
    public void Configure(EntityTypeBuilder<LectureSchedule> builder)
    {
        builder.HasOne(x => x.YearTerm)
            .WithMany(x => x.LectureSchedules)
            .HasForeignKey(x => new { x.YearId, x.TermId })
            .HasPrincipalKey(x => new { x.YearId, x.TermId });
    }
}
