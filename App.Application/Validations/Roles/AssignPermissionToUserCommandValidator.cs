

using App.Application.Commands.Roles;
using App.Infrastructure.Abstractions.Consts;
using App.Infrastructure.Localization;
using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;

namespace App.Application.Validations.Roles;

public class AssignPermissionToUserCommandValidator : AbstractValidator<AssignPermissionToUserCommand>
{
    public AssignPermissionToUserCommandValidator(JsonStringLocalizer localizer)
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.ClaimValue)
            .NotEmpty();
           
        RuleFor(x => x.IsAllowed)
            .NotNull();
    }
}
