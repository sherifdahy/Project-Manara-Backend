using App.Application.Contracts.Responses.Periods;

namespace App.Application.Commands.Periods;

public record CreatePeriodCommand : IRequest<Result<PeriodResponse>>
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int FacultyId { get; set; }
}
