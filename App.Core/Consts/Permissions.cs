using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace App.Infrastructure.Abstractions.Consts;

public class Permissions
{
    public static string Type { get; } = "permissions";


    // Role Permissions

    public const string GetRoles = "roles:read";
    public const string CreateRoles = "roles:create";
    public const string UpdateRoles = "roles:update";
    public const string ToggleStatusRoles = "roles:toggleStatus";

    //Permission Permission 
    public const string GetPermissions = "permissions:read";
    public const string UpdatePermissions = "permissions:update";
    public const string CreatePermissions = "permissions:create";
    public const string ToggleStatusPermissions = "permissions:toggleStatus";

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

    // Department Permissions

    public const string GetDepartments = "departments:read";
    public const string CreateDepartments = "departments:create";
    public const string UpdateDepartments = "departments:update";
    public const string ToggleStatusDepartments = "departments:toggleStatus";

    //Program Permissions

    public const string GetPrograms = "Programs:read";
    public const string CreatePrograms = "Programs:create";
    public const string UpdatePrograms = "Programs:update";
    public const string ToggleStatusPrograms = "Programs:toggleStatus";


    // FacultyUsers Permissions
    public const string GetFacultyUsers = "facultyUsers:read";
    public const string CreateFacultyUsers = "facultyUsers:create";
    public const string UpdateFacultyUsers = "facultyUsers:update";
    public const string ToggleStatusFacultyUsers = "facultyUsers:toggleStatus";

    // UniversityUsers Permissions
    public const string GetUniversityUsers = "universityUsers:read";
    public const string CreateUniversityUsers = "universityUsers:create";
    public const string UpdateUniversityUsers = "universityUsers:update";
    public const string ToggleStatusUniversityUsers = "universityUsers:toggleStatus";

    // Scope Permissions
    public const string GetScopes = "scopes:read";
    public const string GetScopeDetail = "scopes:readDetail";
    public const string CreateScopes = "scopes:create";
    public const string UpdateScopes = "scopes:update";
    public const string ToggleStatusScopes = "scopes:toggleStatus";
    
    public static IList<string> GetAllPermissions()
    {
        return typeof(Permissions).GetFields().Select(x=>x.GetValue(x) as string).ToList()!;
    }
}
