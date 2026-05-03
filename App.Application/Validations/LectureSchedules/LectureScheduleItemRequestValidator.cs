using App.Application.Contracts.Requests.LectureSchedules;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.ProgramSchedules;

public class LectureScheduleItemRequestValidator : AbstractValidator<LectureScheduleItemRequest>
{
    public LectureScheduleItemRequestValidator()
    {
        RuleFor(x => x.SubjectId).GreaterThan(0);
        RuleFor(x => x.DayId).GreaterThan(0);
        RuleFor(x => x.PeriodId).GreaterThan(0);
        RuleFor(x => x.DoctorId).GreaterThan(0);
        RuleFor(x => x.MaxSlots).GreaterThan(0);
    }
}
