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
    public Error NotFound
        => new Error("Permission.NotFound", _localizer[PermissionLocalizationKeys.NotFound, LocalizationFolderNames.Permission], StatusCodes.Status404NotFound);

    public Error InvalidPermissions
       => new Error("Permission.InvalidPermissions", _localizer[PermissionLocalizationKeys.InvalidPermissions, LocalizationFolderNames.Permission], StatusCodes.Status404NotFound);

    
}
