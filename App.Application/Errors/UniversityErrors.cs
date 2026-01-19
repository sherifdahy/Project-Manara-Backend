using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;

namespace App.Application.Errors;

public  class UniversityErrors
{
    private readonly JsonStringLocalizer _localizer;

    public UniversityErrors(JsonStringLocalizer localizer)
    {
        _localizer = localizer;
    }
    public   Error NotFound
        => new Error("University.NotFound", _localizer[UniversityLocalizationKeys.NotFound,LocalizationFolderNames.University], StatusCodes.Status404NotFound);

    public  Error InvalidPermissions
       => new Error("University.InvalidPermissions", _localizer[UniversityLocalizationKeys.InvalidPermissions, LocalizationFolderNames.University], StatusCodes.Status404NotFound);

    public  Error InvalidId
        => new Error("University.InvalidId", _localizer[UniversityLocalizationKeys.InvalidId, LocalizationFolderNames.University], StatusCodes.Status400BadRequest);

    public  Error DuplicatedName 
        => new("University.DuplicatedName", _localizer[UniversityLocalizationKeys.DuplicatedName, LocalizationFolderNames.University], StatusCodes.Status409Conflict);
}
