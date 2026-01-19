using App.Infrastructure.Abstractions.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<int>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder)
    {
        var permissions = Permissions.GetAllPermissions();

        var adminClaims = new List<IdentityRoleClaim<int>>();

        for(var i= 0;i<permissions.Count();i++)
        {
            adminClaims.Add
            (
                new IdentityRoleClaim<int>()
                {
                    Id =  i + 1,
                    RoleId = DefaultRoles.SystemAdminRoleId,
                    ClaimType = Permissions.Type,
                    ClaimValue = permissions[i],
                }
            );
        }

        builder.HasData(adminClaims);
    }
}
