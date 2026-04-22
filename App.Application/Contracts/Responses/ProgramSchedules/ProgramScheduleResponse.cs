using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.ProgramSchedules;

public record ProgramScheduleResponse
{
    public List<ScheduleItemResponse> Schedules { get; set; } = [];
}
