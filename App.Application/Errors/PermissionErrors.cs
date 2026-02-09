using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Errors;

public class PermissionErrors
{
    private readonly JsonStringLocalizer _localizer;

    public PermissionErrors(JsonStringLocalizer localizer)
    {
        _localizer = localizer;
    }
    
    public Error InvalidPermissions
       => new Error("Permission.InvalidPermissions", _localizer[PermissionLocalizationKeys.InvalidPermissions, LocalizationFolderNames.Permission], StatusCodes.Status404NotFound);

    public Error InvalidType
       => new Error("Permission.InvalidType", _localizer[PermissionLocalizationKeys.InvalidType, LocalizationFolderNames.Permission], StatusCodes.Status404NotFound);

    public Error UserAlreadyHasPermission
      => new Error("Permission.UserAlreadyHasPermission", _localizer[PermissionLocalizationKeys.UserAlreadyHasPermission, LocalizationFolderNames.Permission], StatusCodes.Status409Conflict);
    public Error DuplicatedPermissionForUser
       => new Error("Permission.DuplicatedPermissionForUser", _localizer[PermissionLocalizationKeys.DuplicatedPermissionForUser, LocalizationFolderNames.Permission], StatusCodes.Status409Conflict);

    public Error OverridePermissionNotFound
       => new Error("Permission.OverridePermissionNotFound", _localizer[PermissionLocalizationKeys.OverridePermissionNotFound, LocalizationFolderNames.Permission], StatusCodes.Status409Conflict);

    public Error DuplicatedPermissionForRole
      => new Error("Permission.DuplicatedPermissionForRole", _localizer[PermissionLocalizationKeys.DuplicatedPermissionForRole, LocalizationFolderNames.Permission], StatusCodes.Status409Conflict);
}

