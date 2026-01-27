

using App.Application.Commands.Roles;
using App.Infrastructure.Abstractions.Consts;
using App.Infrastructure.Localization;
using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;

namespace App.Application.Validations.Roles;

public class AssignPermissionToUserValidator : AbstractValidator<AssignPermissionToUserCommand>
{
    public AssignPermissionToUserValidator(JsonStringLocalizer localizer)
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.RoleId)
            .NotEmpty();

        RuleFor(x => x.RoleClaim)
            .NotEmpty();

        RuleFor(x => x.IsAllowed)
            .NotEmpty();
    }
}
