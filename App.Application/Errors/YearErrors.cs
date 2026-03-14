using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Errors;

public class YearErrors
{
    public JsonStringLocalizer _localizer { get; }

    public YearErrors(JsonStringLocalizer localizer)
    {
        _localizer = localizer;
    }
    public Error NotFound
        => new Error("Year.NotFound", _localizer[YearLocalizationKeys.NotFound, LocalizationFolderNames.Year], StatusCodes.Status404NotFound);
    public Error TermNotFound
      => new Error("TermNotFound.NotFound", _localizer[YearLocalizationKeys.TermNotFound, LocalizationFolderNames.Year], StatusCodes.Status404NotFound);
}
