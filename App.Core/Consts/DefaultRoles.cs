using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Abstractions.Consts;

public class DefaultRoles
{
    public const string Admin = nameof(Admin);
    public const string AdminRoleConcurrencyStamp = "51655B45-963A-4DD7-A68F-1F18B3F4BE47";
    public const int AdminRoleId = 100;

    public const string Member = nameof(Member);
    public const string MemberRoleConcurrencyStamp = "9601DE96-3D34-48D0-BA24-4D7C1A9F6C7F";
    public const int MemberRoleId = 101;

    public const string SystemAdmin = nameof(SystemAdmin);
    public const string SystemAdminRoleConcurrencyStamp = "AE6C6754-0862-4EA2-8868-BF5C27E7AEF9";
    public const int SystemAdminRoleId = 102;
}
