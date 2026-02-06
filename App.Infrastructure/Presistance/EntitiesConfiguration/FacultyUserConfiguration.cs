using App.Core.Entities.Personnel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class FacultyUserConfiguration : IEntityTypeConfiguration<FacultyUser>
{
    public void Configure(EntityTypeBuilder<FacultyUser> builder)
    {
        builder.HasKey(fu => fu.UserId);

        builder.Property(fu => fu.UserId)
            .IsRequired();


        builder.Property(fu => fu.FacultyId)
            .IsRequired();

    }
}
