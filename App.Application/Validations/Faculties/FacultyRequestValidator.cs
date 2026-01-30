using App.Application.Commands.Faculties;
using App.Application.Contracts.Faculties;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.Faculties;

public class FacultyRequestValidator : AbstractValidator<FacultyRequest>
{
    public FacultyRequestValidator()
    {
        RuleFor(f => f.UniversityId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(f => f.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(f => f.Description)
            .NotEmpty()
            .MaximumLength(1000);


        RuleFor(f => f.Address)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(f => f.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);

        RuleFor(f => f.Website)
            .NotEmpty()
            .MaximumLength(200);
    }
}
