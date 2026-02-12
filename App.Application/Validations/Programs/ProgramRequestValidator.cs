using App.Application.Contracts.Requests.Departments;
using App.Application.Contracts.Requests.Programs;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.Programs;

public class ProgramRequestValidator : AbstractValidator<ProgramRequest>
{
    public ProgramRequestValidator()
    {
        RuleFor(f => f.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(200);

        RuleFor(f => f.Code)
            .NotEmpty()
            .NotNull()
            .MaximumLength(200);

        RuleFor(f => f.Description)
            .NotEmpty()
            .NotNull()
            .MaximumLength(1000);

        RuleFor(f => f.CreditHours)
            .NotEmpty()
            .NotNull();


    }
}
