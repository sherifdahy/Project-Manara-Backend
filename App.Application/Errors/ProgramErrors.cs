using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Errors;

public class ProgramErrors
{
    public JsonStringLocalizer _localizer { get; }

    public ProgramErrors(JsonStringLocalizer localizer)
    {
        _localizer = localizer;
    }
    public Error NotFound
        => new Error("Program.NotFound", _localizer[ProgramLocalizationKeys.NotFound, LocalizationFolderNames.Program], StatusCodes.Status404NotFound);
    public Error DuplicatedName
       => new Error("Program.DuplicatedName", _localizer[ProgramLocalizationKeys.DuplicatedName, LocalizationFolderNames.Program], StatusCodes.Status409Conflict);
    public Error NotFoundForCurrentUser
       => new Error("Program.NotFoundForCurrentUser", _localizer[ProgramLocalizationKeys.NotFoundForCurrentUser, LocalizationFolderNames.Program], StatusCodes.Status409Conflict);
}
