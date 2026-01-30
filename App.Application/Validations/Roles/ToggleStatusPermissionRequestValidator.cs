using App.Application.Contracts.Requests.Roles;
using App.Infrastructure.Abstractions.Consts;
using App.Infrastructure.Localization;
using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;

namespace App.Application.Validations.Roles;

public class ToggleStatusPermissionRequestValidator : AbstractValidator<ToggleStatusPermissionRequest>
{
    public ToggleStatusPermissionRequestValidator(JsonStringLocalizer localizer)
    {
        RuleFor(x => x.ClaimValue)
            .NotEmpty()
            .NotNull();

    }
}
