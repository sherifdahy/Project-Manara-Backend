using App.Infrastructure.Abstractions.Consts;
using App.Core.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.Property(x => x.Code).HasMaxLength(256);
        builder.Property(x => x.Description).HasMaxLength(1000);

        builder.HasIndex(x => x.Code).IsUnique();

        builder.HasData
        (
            // ═══════════════════════════════════════════════════════════
            // SYSTEM SCOPE ROLES
            // ═══════════════════════════════════════════════════════════
            new ApplicationRole()
            {
                Id = DefaultRoles.SystemAdminRoleId,
                Name = DefaultRoles.SystemAdmin,
                Code = "SYS_ADMIN",
                Description = "System Administrator with full access",
                NormalizedName = DefaultRoles.SystemAdmin.ToUpper(),
                ConcurrencyStamp = DefaultRoles.SystemAdminRoleConcurrencyStamp,
                IsDefault = true,
                ScopeId = DefaultScopes.System.Id,
                ParentRoleId = null // Top level - no parent
            },

            // ═══════════════════════════════════════════════════════════
            // UNIVERSITY SCOPE ROLES
            // ═══════════════════════════════════════════════════════════
            new ApplicationRole()
            {
                Id = DefaultRoles.UniversityAdminRoleId,
                Name = DefaultRoles.UniversityAdmin,
                Code = "UNI_ADMIN",
                Description = "University Administrator",
                NormalizedName = DefaultRoles.UniversityAdmin.ToUpper(),
                ConcurrencyStamp = DefaultRoles.UniversityAdminRoleConcurrencyStamp,
                IsDefault = true,
                ScopeId = DefaultScopes.University.Id,
                ParentRoleId = DefaultRoles.SystemAdminRoleId
            },

            // ═══════════════════════════════════════════════════════════
            // FACULTY SCOPE ROLES
            // ═══════════════════════════════════════════════════════════
            new ApplicationRole()
            {
                Id = DefaultRoles.FacultyAdminRoleId,
                Name = DefaultRoles.FacultyAdmin,
                Code = "FAC_ADMIN",
                Description = "Faculty Administrator",
                NormalizedName = DefaultRoles.FacultyAdmin.ToUpper(),
                ConcurrencyStamp = DefaultRoles.FacultyAdminRoleConcurrencyStamp,
                IsDefault = true,
                ScopeId = DefaultScopes.Faculty.Id,
                ParentRoleId = DefaultRoles.UniversityAdminRoleId
            },
            new ApplicationRole()
            {
                Id = DefaultRoles.FacultyCoordinatorRoleId,
                Name = DefaultRoles.FacultyCoordinator,
                Code = "FAC_COORD",
                Description = "Faculty Coordinator",
                NormalizedName = DefaultRoles.FacultyCoordinator.ToUpper(),
                ConcurrencyStamp = DefaultRoles.FacultyCoordinatorRoleConcurrencyStamp,
                IsDefault = false,
                ScopeId = DefaultScopes.Faculty.Id,
                ParentRoleId = DefaultRoles.FacultyAdminRoleId
            },
            new ApplicationRole()
            {
                Id = DefaultRoles.AcademicAdvisorRoleId,
                Name = DefaultRoles.AcademicAdvisor,
                Code = "ACAD_ADV",
                Description = "Academic Advisor",
                NormalizedName = DefaultRoles.AcademicAdvisor.ToUpper(),
                ConcurrencyStamp = DefaultRoles.AcademicAdvisorRoleConcurrencyStamp,
                IsDefault = false,
                ScopeId = DefaultScopes.Faculty.Id,
                ParentRoleId = DefaultRoles.FacultyAdminRoleId
            },

            // ═══════════════════════════════════════════════════════════
            // DEPARTMENT SCOPE ROLES
            // ═══════════════════════════════════════════════════════════
            new ApplicationRole()
            {
                Id = DefaultRoles.DepartmentHeadRoleId,
                Name = DefaultRoles.DepartmentHead,
                Code = "DEPT_ADMIN",
                Description = "Department Administrator",
                NormalizedName = DefaultRoles.DepartmentHead.ToUpper(),
                ConcurrencyStamp = DefaultRoles.DepartmentAdminRoleConcurrencyStamp,
                IsDefault = false,
                ScopeId = DefaultScopes.Department.Id,
                ParentRoleId = DefaultRoles.FacultyAdminRoleId
            },
            new ApplicationRole()
            {
                Id = DefaultRoles.DoctorRoleId,
                Name = DefaultRoles.Doctor,
                Code = "DOCTOR",
                Description = "Doctor/Professor",
                NormalizedName = DefaultRoles.Doctor.ToUpper(),
                ConcurrencyStamp = DefaultRoles.DoctorRoleConcurrencyStamp,
                IsDefault = false,
                ScopeId = DefaultScopes.Department.Id,
                ParentRoleId = DefaultRoles.DepartmentHeadRoleId
            },
            new ApplicationRole()
            {
                Id = DefaultRoles.InstructorRoleId,
                Name = DefaultRoles.Instructor,
                Code = "INSTRUCTOR",
                Description = "Instructor/Teaching Assistant",
                NormalizedName = DefaultRoles.Instructor.ToUpper(),
                ConcurrencyStamp = DefaultRoles.InstructorRoleConcurrencyStamp,
                IsDefault = false,
                ScopeId = DefaultScopes.Department.Id,
                ParentRoleId = DefaultRoles.DoctorRoleId
            },

            // ═══════════════════════════════════════════════════════════
            // PROGRAM SCOPE ROLES
            // ═══════════════════════════════════════════════════════════
            new ApplicationRole()
            {
                Id = DefaultRoles.MainStreamStudentRoleId,
                Name = DefaultRoles.MainStreamStudent,
                Code = "MAIN_STUDENT",
                Description = "Main Stream Student",
                NormalizedName = DefaultRoles.MainStreamStudent.ToUpper(),
                ConcurrencyStamp = DefaultRoles.MainStreamStudentRoleConcurrencyStamp,
                IsDefault = false,
                ScopeId = DefaultScopes.Program.Id,
            },
            new ApplicationRole()
            {
                Id = DefaultRoles.GPAStudentRoleId,
                Name = DefaultRoles.GPAStudent,
                Code = "GPA_STUDENT",
                Description = "GPA Student",
                NormalizedName = DefaultRoles.GPAStudent.ToUpper(),
                ConcurrencyStamp = DefaultRoles.GPAStudentRoleConcurrencyStamp,
                IsDefault = false,
                ScopeId = DefaultScopes.Program.Id,
            }
        );
    }
}