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
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 256);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(RegexPatterns.Password)
            .WithMessage(localizer[UserLocalizationKeys.InvalidPassword, LocalizationFolderNames.User]);

        RuleFor(x => x.NationalId)
            .NotEmpty()
            .Length(14)
            .Matches(RegexPatterns.NationalId)
            .WithMessage(localizer[UserLocalizationKeys.InvalidNationalId, LocalizationFolderNames.User]);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(RegexPatterns.EgyptianPhoneNumber)
            .WithMessage(localizer[UserLocalizationKeys.InvalidPhoneNumber, LocalizationFolderNames.User]);

        RuleFor(x => x.BirthDate)
            .LessThan(DateOnly.FromDateTime(DateTime.Today));

        RuleFor(x => x.Gender)
            .IsInEnum()
            .Must(x => x != default);

        RuleFor(x => x.Religion)
            .IsInEnum()
            .Must(x => x != default);
    }
}

