using App.Application.Contracts.Requests.ProgramSchedules;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Programs;

public record SaveProgramScheduleCommand : IRequest<Result>
{
    public int ProgramId { get; set; }
    public List<ScheduleItemRequest> Schedules { get; set; } = [];
}
