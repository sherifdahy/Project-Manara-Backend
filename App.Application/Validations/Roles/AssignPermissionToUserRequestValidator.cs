

using App.Application.Commands.Roles;
using App.Application.Contracts.Roles;
using App.Infrastructure.Abstractions.Consts;
using App.Infrastructure.Localization;
using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;

namespace App.Application.Validations.Roles;

public class AssignPermissionToUserRequestValidator : AbstractValidator<AssignPermissionToUserRequest>
{
    public AssignPermissionToUserRequestValidator(JsonStringLocalizer localizer)
    {

        RuleFor(x => x.ClaimValue)
            .NotEmpty();
           
        RuleFor(x => x.IsAllowed)
            .NotNull();
    }
}
