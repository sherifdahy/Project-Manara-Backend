using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class RoleClaimOverrideConfiguration : IEntityTypeConfiguration<RoleClaimOverride>
{
    public void Configure(EntityTypeBuilder<RoleClaimOverride> builder)
    {

        builder.HasKey(x => new { x.RoleId,x.FacultyId, x.ClaimValue });

        builder
            .Property(x => x.ClaimValue)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.IsAllowed)
            .IsRequired();

        builder
         .Property(x => x.RoleId)
         .IsRequired();


        builder
         .Property(x => x.FacultyId)
         .IsRequired();

    }
}
