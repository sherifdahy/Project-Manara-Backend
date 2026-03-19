using App.Application.Contracts.Requests.Departments;
using App.Application.Contracts.Requests.Periods;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.Peiods;

public class PeriodRequestValidator : AbstractValidator<PeriodRequest>
{
    public PeriodRequestValidator()
    {

        RuleFor(x => x.StartTime)
            .NotEmpty()
            .LessThan(x => x.EndTime);

        RuleFor(x => x.EndTime)
            .NotEmpty()
            .GreaterThan(x => x.StartTime);

    }
}
