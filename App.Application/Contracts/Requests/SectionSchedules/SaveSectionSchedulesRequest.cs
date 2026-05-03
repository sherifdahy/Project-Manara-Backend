using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Requests.SectionSchedules;

public class SaveSectionSchedulesRequest
{
    public List<SectionScheduleItemRequest> Schedules { get; set; } = [];
}
