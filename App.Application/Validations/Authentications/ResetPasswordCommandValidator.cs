

using App.Application.Constants;
using App.Infrastructure.Localization;
using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;

namespace App.Application.Validations.Authentications;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>    
{

    public ResetPasswordCommandValidator(JsonStringLocalizer localizer)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Code)
           .NotEmpty();

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .Matches(RegexPatterns.Password)
            .WithMessage(localizer[AuthenticationLocalizationKeys.PasswordRegex, LocalizationFolderNames.Authentication]);
    }
}
