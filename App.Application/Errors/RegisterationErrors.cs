using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;

namespace App.Application.Errors;

public class RegistrationErrors
{
    private readonly JsonStringLocalizer _localizer;

    public RegistrationErrors(JsonStringLocalizer localizer)
    {
        _localizer = localizer;
    }



    public Error DuplicatedRegistration
        => new Error(
            "Registration.DuplicatedRegistration",
            _localizer[RegistrationLocalizationKeys.DuplicatedRegistration, LocalizationFolderNames.Registration],
            StatusCodes.Status409Conflict
        );


    public Error lectureScheduleNotFound
    => new Error(
        "Registration.lectureScheduleNotFound",
        _localizer[RegistrationLocalizationKeys.lectureScheduleNotFound, LocalizationFolderNames.Registration],
        StatusCodes.Status409Conflict
    );

    public Error MaxSlotFinish
        => new Error(
            "Registration.MaxSlotFinish",
            _localizer[RegistrationLocalizationKeys.MaxSlotFinish, LocalizationFolderNames.Registration],
            StatusCodes.Status409Conflict
        );


    public Error WrongProgram
        => new Error(
            "Registration.WrongProgram",
            _localizer[RegistrationLocalizationKeys.WrongProgram, LocalizationFolderNames.Registration],
            StatusCodes.Status409Conflict
        );

    public Error WrongTermOrYear
        => new Error(
            "Registration.WrongTermOrYear",
            _localizer[RegistrationLocalizationKeys.WrongTermOrYear, LocalizationFolderNames.Registration],
            StatusCodes.Status409Conflict
        );


    public Error InvalidSubject
        => new Error(
            "Registration.InvalidSubject",
            _localizer[RegistrationLocalizationKeys.InvalidSubject, LocalizationFolderNames.Registration],
            StatusCodes.Status409Conflict
        );

    public Error InvalidRegistration
        => new Error(
            "Registration.InvalidRegistration",
            _localizer[RegistrationLocalizationKeys.InvalidRegistration, LocalizationFolderNames.Registration],
            StatusCodes.Status409Conflict
        );

}
