using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Requests.Periods;

public class PeriodRequest
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}
