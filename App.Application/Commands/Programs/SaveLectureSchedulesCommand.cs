using App.Application.Contracts.Requests.LectureSchedules;

namespace App.Application.Commands.Programs;

public record SaveLectureSchedulesCommand(int ProgramId,List<LectureScheduleItemRequest> Schedules) : IRequest<Result>;
