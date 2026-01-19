using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Abstractions.Consts;

public class Permissions
{
    public static string Type { get; } = "permissions";


    // Role Permissions

    public const string GetRoles = "roles:read";
    public const string CreateRoles = "roles:create";
    public const string UpdateRoles = "roles:update";
    public const string ToggleStatusRoles = "roles:toggleStatus";


    // University Permissions
    public const string GetUniversities = "universities:read";
    public const string CreateUniversities = "universities:create";
    public const string UpdateUniversities = "universities:update";
    public const string ToggleStatusUniversities = "universities:toggleStatus";


    // Faculty Permissions
    public const string GetFaculties = "faculties:read";
    public const string CreateFaculties = "faculties:create";
    public const string UpdateFaculties = "faculties:update";
    public const string ToggleStatusFaculties = "faculties:toggleStatus";


    public static IList<string> GetAllPermissions()
    {
        return typeof(Permissions).GetFields().Select(x=>x.GetValue(x) as string).ToList()!;
    }
}
