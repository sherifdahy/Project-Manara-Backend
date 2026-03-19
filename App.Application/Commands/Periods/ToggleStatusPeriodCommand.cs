

namespace App.Application.Commands.Periods;

public record ToggleStatusPeriodCommand : IRequest<Result>
{
    public int FacultyId { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}
