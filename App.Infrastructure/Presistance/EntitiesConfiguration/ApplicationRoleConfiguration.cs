using App.Infrastructure.Abstractions.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.Property(x => x.Code).HasMaxLength(256);
        builder.Property(x => x.Description).HasMaxLength(1000);

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
