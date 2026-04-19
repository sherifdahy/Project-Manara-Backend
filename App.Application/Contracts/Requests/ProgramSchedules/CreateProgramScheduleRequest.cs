using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Requests.ProgramSchedules;

public class CreateProgramScheduleRequest
{
    public List<ScheduleItemRequest> Schedules { get; set; } = [];
}