using App.Core.Entities.Academic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class SectionScheduleConfiguration : IEntityTypeConfiguration<SectionSchedule>
{
    public void Configure(EntityTypeBuilder<SectionSchedule> builder)
    {
    }
}
