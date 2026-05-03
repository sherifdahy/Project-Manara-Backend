using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;

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

    public Error DuplicatedYear
    => new Error("Year.DuplicatedYear", _localizer[YearLocalizationKeys.DuplicatedYear, LocalizationFolderNames.Year], StatusCodes.Status404NotFound);
    public Error TermNotFound
      => new Error("TermNotFound.NotFound", _localizer[YearLocalizationKeys.TermNotFound, LocalizationFolderNames.Year], StatusCodes.Status404NotFound);

    public Error NoActiveYearTerm
      => new Error("TermNotFound.NoActive", _localizer[YearLocalizationKeys.NoActiveYearTerm, LocalizationFolderNames.Year], StatusCodes.Status404NotFound);
}
