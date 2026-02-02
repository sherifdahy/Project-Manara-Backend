using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Localization.Constants;

public class PermissionLocalizationKeys
{

    public static readonly string InvalidPermissions = nameof(InvalidPermissions);

    public static readonly string InvalidType = nameof(InvalidType);

    public static readonly string UserAlreadyHasPermission = nameof(UserAlreadyHasPermission);

    public static readonly string DuplicatedPermissionForUser = nameof(DuplicatedPermissionForUser);

    public static readonly string OverridePermissionNotFound = nameof(OverridePermissionNotFound);

    public static readonly string DuplicatedPermissionForRole = nameof(DuplicatedPermissionForRole);
}
