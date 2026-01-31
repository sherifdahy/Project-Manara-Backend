

using App.Application.Commands.Roles;
using App.Application.Contracts.Requests.Roles;
using App.Infrastructure.Abstractions.Consts;
using App.Infrastructure.Localization;
using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;

namespace App.Application.Validations.Roles;

public class AssignPermissionRequestValidator : AbstractValidator<AssignPermissionRequest>
{
    public AssignPermissionRequestValidator(JsonStringLocalizer localizer)
    {

        RuleFor(x => x.ClaimValue)
            .NotEmpty();
           
        RuleFor(x => x.IsAllowed)
            .NotNull();
    }
}
