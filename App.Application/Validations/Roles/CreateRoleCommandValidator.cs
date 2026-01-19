using App.Application.Commands.Roles;
using App.Infrastructure.Abstractions.Consts;
using App.Infrastructure.Localization;
using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.Roles;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator(JsonStringLocalizer localizer)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 200);


        RuleFor(x => x.Permissions)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Permissions).Must(x => x.Distinct().Count() == x.Count())
            .WithMessage(localizer[AuthenticationLocalizationKeys.DuplicatedPermissions, LocalizationFolderNames.Authentication])
            .When(x => x.Permissions != null);

        RuleForEach(x => x.Permissions).Must(p => Permissions.GetAllPermissions().Contains(p));
    }
}
