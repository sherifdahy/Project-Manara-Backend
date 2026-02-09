using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;
using Hangfire.Storage.Monitoring;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Errors;

public class UserErrors
{
    private readonly JsonStringLocalizer _localizer;

    public UserErrors(JsonStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public Error DuplicatedEmail
        => new Error(
            "User.DuplicatedEmail",
            _localizer[UserLocalizationKeys.DuplicatedEmail, LocalizationFolderNames.User],
            StatusCodes.Status409Conflict
        );

    public Error DuplicatedSSN
        => new Error(
            "User.DuplicatedSSN",
            _localizer[UserLocalizationKeys.DuplicatedSSN, LocalizationFolderNames.User],
            StatusCodes.Status409Conflict
        );

    public Error NotFound
        => new Error(
            "User.NotFound",
            _localizer[UserLocalizationKeys.NotFound, LocalizationFolderNames.User],
            StatusCodes.Status404NotFound
        );

    public Error Forbidden
        => new Error(
            "User.Forbidden",
            _localizer[UserLocalizationKeys.Forbidden, LocalizationFolderNames.User],
            StatusCodes.Status403Forbidden
        );
}
