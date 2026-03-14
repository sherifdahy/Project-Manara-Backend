

using App.Application.Contracts.Requests.Departments;
using App.Application.Contracts.Requests.Years;
using App.Infrastructure.Localization;
using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;

namespace App.Application.Validations.Years;

public class YearRequestValidator : AbstractValidator<YearRequest>
{
    public YearRequestValidator(JsonStringLocalizer localizer)
    {
        RuleFor(f => f.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(200);

        RuleFor(f => f.StartDate)
            .NotEmpty()
            .NotNull()
            .LessThan(f => f.EndDate)
            .WithMessage(localizer[YearLocalizationKeys.LessThanDate, LocalizationFolderNames.Year]);


        RuleFor(f => f.EndDate)
            .NotEmpty()
            .NotNull()
            .GreaterThan(f => f.StartDate)
            .WithMessage(localizer[YearLocalizationKeys.GreaterThanDate, LocalizationFolderNames.Year]);

        RuleFor(f => f.ActiveTermId)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);

    }
}

