using App.Application.Contracts.Requests.SectionSchedules;

namespace App.Application.Commands.Programs;

public record SaveSectionSchedulesCommand (int ProgramId,List<SectionScheduleItemRequest> Schedules) : IRequest<Result>;
