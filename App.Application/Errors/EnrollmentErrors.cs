using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;

namespace App.Application.Errors;

public class EnrollmentErrors
{
    private readonly JsonStringLocalizer _localizer;

    public EnrollmentErrors(JsonStringLocalizer localizer)
    {
        _localizer = localizer;
    }


    public Error NotFound
        => new Error(
            "Enrollment.NotFound",
            _localizer[EnrollmentLocalizationKeys.NotFound, LocalizationFolderNames.Enrollment],
            StatusCodes.Status404NotFound
        );

    public Error DifferentProgram
        => new Error(
            "Enrollment.DifferentProgram",
            _localizer[EnrollmentLocalizationKeys.DifferentProgram, LocalizationFolderNames.Enrollment],
            StatusCodes.Status404NotFound
        );

    public Error DifferentYear
        => new Error(
            "Enrollment.DifferentYear",
            _localizer[EnrollmentLocalizationKeys.DifferentYear, LocalizationFolderNames.Enrollment],
            StatusCodes.Status404NotFound
        );

    public Error DuplicatedEnrollment
        => new Error(
            "Enrollment.DuplicatedEnrollment",
            _localizer[EnrollmentLocalizationKeys.DuplicatedEnrollment, LocalizationFolderNames.Enrollment],
            StatusCodes.Status409Conflict
        );

    public Error AlreadyEnrolledInThisYearTerm
            => new Error(
                "Enrollment.AlreadyEnrolledInThisYearTerm",
                _localizer[EnrollmentLocalizationKeys.AlreadyEnrolledInThisYearTerm, LocalizationFolderNames.Enrollment],
                StatusCodes.Status409Conflict
            );
}
