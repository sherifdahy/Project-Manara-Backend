using App.Application.Contracts.Requests.ProgramSchedules;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.ProgramSchedules;

public class ScheduleItemRequestValidator : AbstractValidator<ScheduleItemRequest>
{
    public ScheduleItemRequestValidator()
    {
        RuleFor(x => x.SubjectId).GreaterThan(0);
        RuleFor(x => x.DayId).GreaterThan(0);
        RuleFor(x => x.PeriodId).GreaterThan(0);
    }
}
