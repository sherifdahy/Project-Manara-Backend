using App.Infrastructure.Abstractions.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
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
