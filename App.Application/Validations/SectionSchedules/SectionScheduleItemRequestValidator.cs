using App.Application.Contracts.Requests.SectionSchedules;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.SectionSchedules;

public class SectionScheduleItemRequestValidator : AbstractValidator<SectionScheduleItemRequest>
{
    public SectionScheduleItemRequestValidator()
    {
        RuleFor(x => x.SubjectId).GreaterThan(0);
        RuleFor(x => x.DayId).GreaterThan(0);
        RuleFor(x => x.PeriodId).GreaterThan(0);
        RuleFor(x => x.InstructorId).GreaterThan(0);
        RuleFor(x => x.MaxSlots).GreaterThan(0);
    }
}
