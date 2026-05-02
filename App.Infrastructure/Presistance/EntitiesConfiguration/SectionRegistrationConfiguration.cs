using App.Core.Entities.Relations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class SectionRegistrationConfiguration : IEntityTypeConfiguration<SectionRegistration>
{
    public void Configure(EntityTypeBuilder<SectionRegistration> builder)
    {
        builder.HasKey(x => new { x.StudentId, x.SectionScheduleId });
    }
}
