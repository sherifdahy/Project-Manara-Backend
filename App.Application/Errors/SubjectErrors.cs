using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Errors;

public class SubjectErrors
{
    public JsonStringLocalizer _localizer { get; }

    public SubjectErrors(JsonStringLocalizer localizer)
    {
        _localizer = localizer;
    }
    public Error NotFound
        => new Error("Subject.NotFound", _localizer[SubjectLocalizationKeys.NotFound, LocalizationFolderNames.Subject], StatusCodes.Status404NotFound);

    public Error DuplicatedName
    => new Error("Subject.DuplicatedName", _localizer[SubjectLocalizationKeys.DuplicatedName, LocalizationFolderNames.Subject], StatusCodes.Status409Conflict);
}