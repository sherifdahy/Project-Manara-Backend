using App.Infrastructure.Abstractions.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.OwnsMany(x => x.RefreshTokens)
            .ToTable("RefreshTokens") 
            .WithOwner()
            .HasForeignKey("UserId");

        builder.Property(x => x.Name).HasMaxLength(256);

        builder.Property(x => x.SSN)
            .HasMaxLength(50)
            .IsUnicode(false);

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
                PasswordHash = DefaultUsers.SystemAdminPassword
                //PasswordHash = _passwordHasher.HashPassword(null!, DefaultUsers.SystemAdminPassword),
            }
        );
    }
}
