using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Periods;

public record PeriodResponse
(
    TimeOnly StartTime,
    TimeOnly EndTime,
    bool IsDeleted
);


