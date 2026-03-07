using App.Core.Entities.Relations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class DepartmentUserSubjectYearTermPeriodConfiguration : IEntityTypeConfiguration<DepartmentUserSubjectYearTermPeriod>
{
    public void Configure(EntityTypeBuilder<DepartmentUserSubjectYearTermPeriod> builder)
    {
        builder.HasKey(builder => new { builder.UserId, builder.SubjectId, builder.YearTermId, builder.PeriodId });
    }
}
