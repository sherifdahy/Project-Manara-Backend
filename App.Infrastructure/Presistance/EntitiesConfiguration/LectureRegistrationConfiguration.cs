using App.Core.Entities.Relations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class LectureRegistrationConfiguration : IEntityTypeConfiguration<LectureRegistration>
{
    public void Configure(EntityTypeBuilder<LectureRegistration> builder)
    {
        builder.HasKey(x => new { x.StudentId, x.LectureScheduleId });
    }
}
