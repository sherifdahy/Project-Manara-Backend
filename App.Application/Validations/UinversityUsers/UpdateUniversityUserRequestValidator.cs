using App.Application.Constants;
using App.Application.Contracts.Requests.UniversityUsers;
using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.UinversityUsers;

public class UpdateUniversityUserRequestValidator : AbstractValidator<UpdateUniversityUserRequest>
{
    public UpdateUniversityUserRequestValidator(JsonStringLocalizer localizer)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 256);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);

        RuleFor(x => x.Password)
            .Matches(RegexPatterns.Password)
            .WithMessage(localizer[UserLocalizationKeys.InvalidPassword, LocalizationFolderNames.User])
            .When(x => !string.IsNullOrWhiteSpace(x.Password));

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

        RuleFor(x => x.Roles)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Roles)
            .Must(r => r.Distinct().Count() == r.Count())
            .WithMessage(localizer[UserLocalizationKeys.DuplicateRoles, LocalizationFolderNames.User])
            .When(x => x.Roles != null);
    }
}
