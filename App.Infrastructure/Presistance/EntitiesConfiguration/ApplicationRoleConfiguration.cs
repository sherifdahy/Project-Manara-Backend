using App.Core.Enums;
using App.Infrastructure.Abstractions.Consts;
using App.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder
            .Property(x => x.RoleType)
            .IsRequired();

        builder.EnumInRange(x => x.RoleType);

        builder.HasIndex(r => r.NormalizedName)
            .IsUnique()
            .HasFilter("[UniversityId] IS NULL")
            .HasDatabaseName("IX_AspNetRoles_NormalizedName_Global");

        builder.HasIndex(r => new { r.NormalizedName, r.UniversityId })
            .IsUnique()
            .HasFilter("[UniversityId] IS NOT NULL")
            .HasDatabaseName("IX_AspNetRoles_NormalizedName_UniversityId");


        builder.HasData
        (
            new ApplicationRole()
            {
                Id = DefaultRoles.AdminRoleId,
                Name = DefaultRoles.Admin,
                NormalizedName = DefaultRoles.Admin.ToUpper(),
                ConcurrencyStamp = DefaultRoles.AdminRoleConcurrencyStamp,
            },
            new ApplicationRole()
            {
                Id = DefaultRoles.MemberRoleId,
                Name = DefaultRoles.Member,
                IsDefualt = true,
                NormalizedName = DefaultRoles.Member.ToUpper(),
                ConcurrencyStamp = DefaultRoles.MemberRoleConcurrencyStamp,
            },
            new ApplicationRole()
            {
                Id = DefaultRoles.SystemAdminRoleId,
                Name = DefaultRoles.SystemAdmin,
                NormalizedName = DefaultRoles.SystemAdmin.ToUpper(),
                ConcurrencyStamp = DefaultRoles.SystemAdminRoleConcurrencyStamp,
            }
        );
    }
}
