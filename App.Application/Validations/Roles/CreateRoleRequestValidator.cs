using App.Application.Contracts.Requests.Roles;
using App.Infrastructure.Localization;
using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Validations.Roles;

public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
{
    public CreateRoleRequestValidator(JsonStringLocalizer localizer)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 200);

        RuleFor(x => x.IsDeleted)
            .NotNull();

        RuleFor(x => x.Permissions)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Permissions).Must(x => x.Distinct().Count() == x.Count())
            .WithMessage(localizer[AuthenticationLocalizationKeys.DuplicatedPermissions, LocalizationFolderNames.Authentication])
            .When(x => x.Permissions != null);


    }
}
