namespace App.Infrastructure.Abstractions.Consts;

public class DefaultRoles
{
    // System Scope Roles
    public const string SystemAdmin = nameof(SystemAdmin);
    public const string SystemAdminRoleConcurrencyStamp = "AE6C6754-0862-4EA2-8868-BF5C27E7AEF9";
    public const int SystemAdminRoleId = 100;

    // University Scope Roles
    public const string UniversityAdmin = nameof(UniversityAdmin);
    public const string UniversityAdminRoleConcurrencyStamp = "B1A2C3D4-5E6F-7890-ABCD-EF1234567890";
    public const int UniversityAdminRoleId = 101;

    // Faculty Scope Roles
    public const string FacultyAdmin = nameof(FacultyAdmin);
    public const string FacultyAdminRoleConcurrencyStamp = "D3C4E5F6-7081-9012-CDEF-345678901234";
    public const int FacultyAdminRoleId = 102;

    public const string FacultyCoordinator = nameof(FacultyCoordinator);
    public const string FacultyCoordinatorRoleConcurrencyStamp = "E4D5F6A7-8192-0123-DEF4-456789012345";
    public const int FacultyCoordinatorRoleId = 103;

    public const string AcademicAdvisor = nameof(AcademicAdvisor);
    public const string AcademicAdvisorRoleConcurrencyStamp = "F5E6A7B8-9203-1234-EF45-567890123456";
    public const int AcademicAdvisorRoleId = 104;

    // Department Scope Roles
    public const string DepartmentHead = nameof(DepartmentHead);
    public const string DepartmentAdminRoleConcurrencyStamp = "A6F7B8C9-0314-2345-F456-678901234567";
    public const int DepartmentHeadRoleId = 105;

    public const string Doctor = nameof(Doctor);
    public const string DoctorRoleConcurrencyStamp = "B7A8C9D0-1425-3456-A567-789012345678";
    public const int DoctorRoleId = 106;

    public const string Instructor = nameof(Instructor);
    public const string InstructorRoleConcurrencyStamp = "C8B9D0E1-2536-4567-B678-890123456789";
    public const int InstructorRoleId = 107;

    // Program Scope Roles
    public const string MainStreamStudent = nameof(MainStreamStudent);
    public const string MainStreamStudentRoleConcurrencyStamp = "F1E2A3B4-5869-789A-E901-123456789012";
    public const int MainStreamStudentRoleId = 110;

    public const string GPAStudent = nameof(GPAStudent);
    public const string GPAStudentRoleConcurrencyStamp = "A2F3B4C5-6970-89AB-F012-234567890123";
    public const int GPAStudentRoleId = 111;
}