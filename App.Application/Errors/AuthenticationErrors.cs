using App.Application.Abstractions;
using App.Infrastructure.Localization;
using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;
using Microsoft.AspNetCore.Http;

namespace App.Application.Errors;

public  class AuthenticationErrors 
{
    private readonly JsonStringLocalizer _localizer;

    public AuthenticationErrors(JsonStringLocalizer localizer)
    {
        _localizer = localizer;

    }
    public  Error InvalidCredentials => 
        new Error("User.InvalidCredentials", _localizer[AuthenticationLocalizationKeys.InvalidCredentials,LocalizationFolderNames.Authentication], StatusCodes.Status401Unauthorized);

    public Error DisabledUser => 
        new Error("User.DisabledUser", _localizer[AuthenticationLocalizationKeys.DisabledUser, LocalizationFolderNames.Authentication], StatusCodes.Status400BadRequest);

    //public   Error EmailNotConfirmed => 
    //    new Error("User.EmailNotConfirmed", _localizer[LocalizationKeyNames.EmailNotConfirmed, LocalizationFolderNames.Authentication], StatusCodes.Status401Unauthorized);

    public   Error LockedUser => 
        new Error("User.LockedUser", _localizer[AuthenticationLocalizationKeys.LockedUser, LocalizationFolderNames.Authentication], StatusCodes.Status400BadRequest);

    public   Error DuplicatedEmail => 
        new Error("User.DuplicatedEmail", _localizer[AuthenticationLocalizationKeys.DuplicatedEmail, LocalizationFolderNames.Authentication], StatusCodes.Status400BadRequest);

    public   Error InvalidToken => 
        new Error("User.InvalidToken", _localizer[AuthenticationLocalizationKeys.InvalidToken, LocalizationFolderNames.Authentication], StatusCodes.Status401Unauthorized);

    public   Error InvalidCode => 
        new Error("User.InvalidCode", _localizer[AuthenticationLocalizationKeys.InvalidCode, LocalizationFolderNames.Authentication], StatusCodes.Status400BadRequest);
}
