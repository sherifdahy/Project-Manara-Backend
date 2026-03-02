using App.Core.Entities.Personnel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class ProgramUserConfiguration : IEntityTypeConfiguration<ProgramUser>
{
    public void Configure(EntityTypeBuilder<ProgramUser> builder)
    {

        builder.HasKey(fu => fu.UserId);

        builder.Property(fu => fu.UserId)
            .IsRequired();

        builder.Property(fu => fu.ProgramId)
            .IsRequired();
    }
}
