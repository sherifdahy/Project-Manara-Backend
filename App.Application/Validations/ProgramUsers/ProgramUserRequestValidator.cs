using App.Application.Constants;
using App.Application.Contracts.Requests.DepartmentUsers;
using App.Application.Contracts.Requests.ProgramUsers;
using App.Infrastructure.Localization;
using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.ProgramUsers;

public class ProgramUserRequestValidator : AbstractValidator<ProgramUserRequest>
{
    public ProgramUserRequestValidator(JsonStringLocalizer localizer)
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().Matches(RegexPatterns.Password);
        RuleFor(x => x.SSN).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Gender).NotEmpty().Length(1);
        RuleFor(x => x.NationalId).NotEmpty().Length(14);
        RuleFor(x => x.BirthDate).NotEmpty().LessThan(DateTime.Today);
        RuleFor(x => x.BirthDate).LessThan(DateTime.Today.AddYears(-16))
            .WithMessage(localizer[ProgramUserLocalizationKeys.InvalidBirthDate, LocalizationFolderNames.ProgramUser]);
        RuleFor(x => x.EnrollmentDate).NotEmpty().LessThanOrEqualTo(DateTime.Today);
        RuleFor(x => x.EnrollmentDate).GreaterThan(x => x.BirthDate)
            .WithMessage(localizer[ProgramUserLocalizationKeys.InvalidEnrollmentDate, LocalizationFolderNames.ProgramUser]);
        RuleFor(x => x.GPA).NotEmpty().LessThanOrEqualTo(100);
        RuleFor(x => x.Status).NotEmpty();
        RuleFor(x => x.AcademicLevel).NotEmpty();
        RuleFor(x => x.Roles).NotEmpty().Must(r => r.Distinct().Count() == r.Count());
    }
}

