using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Errors;

public class DayErrors
{
    public JsonStringLocalizer _localizer { get; }
    public DayErrors(JsonStringLocalizer localizer)
    {
        _localizer = localizer;
    }
    public Error NotFound
     => new Error("Day.NotFound", _localizer[DayLocalizationKeys.NotFound, LocalizationFolderNames.Day], StatusCodes.Status404NotFound);

}
