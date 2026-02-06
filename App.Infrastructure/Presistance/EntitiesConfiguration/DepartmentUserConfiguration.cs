using App.Core.Entities.Personnel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class DepartmentUserConfiguration : IEntityTypeConfiguration<DepartmentUser>
{
    public void Configure(EntityTypeBuilder<DepartmentUser> builder)
    {
        builder.HasKey(fu => fu.UserId);

        builder.Property(fu => fu.UserId)
            .IsRequired();


        builder.Property(fu => fu.DepartmentId)
            .IsRequired();

    }
}
