using App.Application.Commands.Authentications;
using App.Application.Constants;
using App.Infrastructure.Localization;
using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.Authentications;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(JsonStringLocalizer localizer)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(RegexPatterns.Password)
            .WithMessage(localizer[AuthenticationLocalizationKeys.PasswordRegex,LocalizationFolderNames.Authentication]);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .Length(3, 100);


        RuleFor(x => x.LastName)
            .NotEmpty()
            .Length(3, 100);
    }
}
