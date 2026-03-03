using App.Application.Constants;
using App.Application.Contracts.Requests.DepartmentUsers;
using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;

namespace App.Application.Validations.DepartmentUsers;

public class DepartmentUserRequestValidator : AbstractValidator<DepartmentUserRequest>
{
    public DepartmentUserRequestValidator(JsonStringLocalizer localizer)
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

        RuleFor(x => x.Roles)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Roles)
            .Must(r => r.Distinct().Count() == r.Count())
            .WithMessage(localizer[UserLocalizationKeys.DuplicateRoles, LocalizationFolderNames.User])
            .When(x => x.Roles != null);
    }

}

