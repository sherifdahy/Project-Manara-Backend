using App.Application.Commands.Universities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.Universities;

public class UpdateUniversityCommandValidator : AbstractValidator<UpdateUniversityCommand>
{
    public UpdateUniversityCommandValidator()
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
    }
}
