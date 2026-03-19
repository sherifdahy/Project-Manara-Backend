using App.Core.Consts;
using App.Core.Entities.Academic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class DayConfiguration : IEntityTypeConfiguration<Day>
{
    public void Configure(EntityTypeBuilder<Day> builder)
    {
        builder.HasData(
            new Day { Id = DefaultDays.Saturday.Id, Value = DefaultDays.Saturday.Name },
            new Day { Id = DefaultDays.Sunday.Id, Value = DefaultDays.Sunday.Name },
            new Day { Id = DefaultDays.Monday.Id, Value = DefaultDays.Monday.Name },
            new Day { Id = DefaultDays.Tuesday.Id, Value = DefaultDays.Tuesday.Name },
            new Day { Id = DefaultDays.Wednesday.Id, Value = DefaultDays.Wednesday.Name },
            new Day { Id = DefaultDays.Thursday.Id, Value = DefaultDays.Thursday.Name },
            new Day { Id = DefaultDays.Friday.Id, Value = DefaultDays.Friday.Name }
        );
    }
}
