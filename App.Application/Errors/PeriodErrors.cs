using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Errors;

public class PeriodErrors
{
    public JsonStringLocalizer _localizer { get; }

    public PeriodErrors(JsonStringLocalizer localizer)
    {
        _localizer = localizer;
    }
    public Error NotFound
        => new Error("Period.NotFound", _localizer[PeriodLocalizationKeys.NotFound, LocalizationFolderNames.Period], StatusCodes.Status404NotFound);

    public Error DuplicatedPeriod =>
            new("Period.DuplicatedPeriod", _localizer[PeriodLocalizationKeys.DuplicatedPeriod, LocalizationFolderNames.Period], StatusCodes.Status409Conflict);

   
}
