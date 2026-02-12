using App.Infrastructure.Localization.Constants;
using App.Infrastructure.Localization.Localizers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Errors;

public class DepartmentErrors
{
    public JsonStringLocalizer _localizer { get; }

    public DepartmentErrors(JsonStringLocalizer localizer)
    {
        _localizer = localizer;
    }
    public Error NotFound
        => new Error("Department.NotFound", _localizer[DepartmentLocalizationKeys.NotFound, LocalizationFolderNames.Department], StatusCodes.Status404NotFound);
    public Error DuplicatedName
       => new Error("Department.DuplicatedName", _localizer[DepartmentLocalizationKeys.DuplicatedName, LocalizationFolderNames.Department], StatusCodes.Status409Conflict);
    public Error NotFoundForCurrentUser
       => new Error("Department.NotFoundForCurrentUser", _localizer[DepartmentLocalizationKeys.NotFoundForCurrentUser, LocalizationFolderNames.Department], StatusCodes.Status409Conflict);

}
