

using App.Core.Entities.Relations;
using App.Infrastructure.Abstractions.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class UserPermissionOverrideConfiguration : IEntityTypeConfiguration<UserPermissionOverride>
{
    public void Configure(EntityTypeBuilder<UserPermissionOverride> builder)
    {

        builder.HasKey(x => new { x.ApplicationUserId, x.ClaimValue });

        builder
            .Property(x => x.ClaimValue)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.IsAllowed)
            .IsRequired();

        builder
         .Property(x => x.ApplicationUserId)
        .IsRequired();

    }
}

