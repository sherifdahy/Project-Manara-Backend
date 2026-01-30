using App.Application.Contracts.Requests.Universities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.Universities;

public class UniversityRequestValidator : AbstractValidator<UniversityRequest>
{
    public UniversityRequestValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty()
        .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);

        RuleFor(x => x.Website)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(x => x.YearOfEstablishment)
            .NotEmpty()
            .NotNull()
            .InclusiveBetween(1800, DateTime.Now.Year);
    }
}
