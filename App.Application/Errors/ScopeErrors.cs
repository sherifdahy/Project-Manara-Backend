using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Errors;

public class ScopeErrors
{
    private readonly JsonStringLocalizer _localizer;

    public ScopeErrors(JsonStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public Error NotFound
        => new Error(
            "Scope.NotFound",
            _localizer[UserLocalizationKeys.NotFound, LocalizationFolderNames.User],
            StatusCodes.Status404NotFound
        );
}
