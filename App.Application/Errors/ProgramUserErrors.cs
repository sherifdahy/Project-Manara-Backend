using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;

namespace App.Application.Errors;

public class ProgramUserErrors
{
    private readonly JsonStringLocalizer _localizer;

    public ProgramUserErrors(JsonStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public Error InvalidBirthDate
       => new Error("ProgramUser.InvalidBirthDate", _localizer[ProgramUserLocalizationKeys.InvalidBirthDate, LocalizationFolderNames.ProgramUser], StatusCodes.Status400BadRequest);
    public Error InvalidEnrollmentDate
    => new Error("ProgramUser.InvalidEnrollmentDate", _localizer[ProgramUserLocalizationKeys.InvalidEnrollmentDate, LocalizationFolderNames.ProgramUser], StatusCodes.Status400BadRequest);

}

