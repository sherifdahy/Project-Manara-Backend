using App.Application.Abstractions;
using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;
using Microsoft.AspNetCore.Http;

namespace App.Application.Errors;

public  class RoleErrors
{
    private readonly JsonStringLocalizer _localizer;

    public RoleErrors(JsonStringLocalizer localizer)
    {
        _localizer = localizer;
    }
    public Error ModificationForbidden
        => new Error("Role.ModificationForbidden", _localizer[RoleLocalizationKeys.ModificationForbidden, LocalizationFolderNames.Role], StatusCodes.Status403Forbidden);
    public   Error NotFound
        => new Error("Role.NotFound", _localizer[RoleLocalizationKeys.NotFound,LocalizationFolderNames.Role], StatusCodes.Status404NotFound);

    public  Error InvalidPermissions
       => new Error("Role.InvalidPermissions", _localizer[RoleLocalizationKeys.InvalidPermissions, LocalizationFolderNames.Role], StatusCodes.Status404NotFound);

    public  Error InvalidId
        => new Error("Role.InvalidId", _localizer[RoleLocalizationKeys.InvalidId, LocalizationFolderNames.Role], StatusCodes.Status400BadRequest);

    public  Error Duplicated 
        => new("Role.DuplicatedRole", _localizer[RoleLocalizationKeys.DuplicatedRole, LocalizationFolderNames.Role], StatusCodes.Status409Conflict);
}
