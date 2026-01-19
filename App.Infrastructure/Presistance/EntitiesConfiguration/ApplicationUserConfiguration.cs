using App.Infrastructure.Abstractions.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.OwnsMany(x => x.RefreshTokens)
            .ToTable("RefreshTokens") 
            .WithOwner()
            .HasForeignKey("UserId");

        builder.Property(x => x.FirstName).HasMaxLength(100);

        builder.Property(x => x.LastName).HasMaxLength(100);

        var _passwordHasher = new PasswordHasher<ApplicationUser>();

        builder.HasData
        (
            new ApplicationUser()
            {
                Id = DefaultUsers.SystemAdminId,
                UserName = DefaultUsers.SystemAdminEmail,
                Email = DefaultUsers.SystemAdminEmail,
                NormalizedEmail = DefaultUsers.SystemAdminEmail.ToUpper(),
                NormalizedUserName = DefaultUsers.SystemAdminEmail.ToUpper(),
                ConcurrencyStamp = DefaultUsers.SystemAdminConcurrencyStamp,
                SecurityStamp = DefaultUsers.SystemAdminSecurityStamp,
                EmailConfirmed = true,
                PasswordHash = DefaultUsers.AdminPassword
                //PasswordHash = _passwordHasher.HashPassword(null!, DefaultUsers.SystemAdminPassword),
            },
            new ApplicationUser()
            {
                Id = DefaultUsers.AdminId,
                UserName = DefaultUsers.AdminEmail,
                Email = DefaultUsers.AdminEmail,
                NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
                NormalizedUserName = DefaultUsers.AdminEmail.ToUpper(),
                ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
                SecurityStamp = DefaultUsers.AdminSecurityStamp,
                EmailConfirmed = true,
                PasswordHash = DefaultUsers.SystemAdminPassword,
                //PasswordHash = _passwordHasher.HashPassword(null!, DefaultUsers.AdminPassword),
            }
        );
    }
}
