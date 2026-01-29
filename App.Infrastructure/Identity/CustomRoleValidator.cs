using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Identity;

public class CustomRoleValidator<TRole> : RoleValidator<TRole> where TRole : ApplicationRole
{
    private readonly JsonStringLocalizer _localizer;

    public CustomRoleValidator(JsonStringLocalizer localizer)
    {
        _localizer = localizer;
    }
    public override async Task<IdentityResult> ValidateAsync(RoleManager<TRole> manager, TRole role)
    {
        var errors = new List<IdentityError>();

        var normalizedName = role.Name!.ToUpper();

        ApplicationRole? existingRole;

        if (role.UniversityId == null)
        {
            existingRole = await manager.Roles
                .FirstOrDefaultAsync(r =>
                    r.NormalizedName == normalizedName &&
                    r.UniversityId == null &&
                    r.Id != role.Id);
        }
        else
        {
            existingRole = await manager.Roles
                .FirstOrDefaultAsync(r =>
                    r.NormalizedName == normalizedName &&
                    r.UniversityId == role.UniversityId &&
                    r.Id != role.Id);
        }

        if (existingRole != null)
        {
            var message = role.UniversityId == null
                ? _localizer[RoleLocalizationKeys.DuplicatedRole, LocalizationFolderNames.Role]
                : _localizer[RoleLocalizationKeys.DuplicatedForUniversity, LocalizationFolderNames.Role];

            errors.Add(new IdentityError
            {
                Code = "DuplicateRoleName",
                Description = message
            });
        }

        return errors.Count > 0
            ? IdentityResult.Failed(errors.ToArray())
            : IdentityResult.Success;
    }
}
