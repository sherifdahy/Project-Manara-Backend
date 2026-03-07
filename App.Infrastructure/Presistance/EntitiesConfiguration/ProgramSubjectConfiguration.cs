using App.Core.Entities.Relations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class ProgramSubjectConfiguration : IEntityTypeConfiguration<ProgramSubject>
{
    public void Configure(EntityTypeBuilder<ProgramSubject> builder)
    {
        builder.HasKey(x => new {x.ProgramId,x.SubjectId});
    }
}
