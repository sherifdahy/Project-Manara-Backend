using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;

namespace App.Application.Errors;

public  class FacultyErrors
{
    public JsonStringLocalizer _localizer { get; }

    public FacultyErrors(JsonStringLocalizer localizer)
    {
        _localizer = localizer;
    }
    public  Error NotFound
        => new Error("Faculty.NotFound", _localizer[FacultyLocalizationKeys.NotFound,LocalizationFolderNames.Faculty], StatusCodes.Status404NotFound);

    public   Error InvalidPermissions
       => new Error("Faculty.InvalidPermissions", _localizer[FacultyLocalizationKeys.InvalidPermissions, LocalizationFolderNames.Faculty], StatusCodes.Status404NotFound);

    public   Error InvalidId
        => new Error("Faculty.InvalidId", _localizer[FacultyLocalizationKeys.InvalidId, LocalizationFolderNames.Faculty], StatusCodes.Status400BadRequest);

    public   Error DuplicatedName =>
            new("Faculty.DuplicatedName", _localizer[FacultyLocalizationKeys.DuplicatedName, LocalizationFolderNames.Faculty], StatusCodes.Status409Conflict);

}
