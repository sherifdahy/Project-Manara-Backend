using App.Application.Contracts.Responses.Periods;

namespace App.Application.Queries.Periods;

public class GetPeriodQuery : IRequest<Result<PeriodResponse>>
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int FacultyId { get; set; }

    public GetPeriodQuery(int facultyId, TimeOnly startTime,TimeOnly endTime)
    {
        FacultyId = facultyId;
        StartTime = startTime;
        EndTime = endTime;
    }
}
