using App.Application.Contracts.Responses.Programs;

namespace App.Application.Queries.Programs;

public record GetLectureSchedulesQuery
(
    int ProgramId
) : IRequest<Result<List<LectureScheduleItemResponse>>>;
