using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NationalId = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Religion = table.Column<int>(type: "int", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Day",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Day", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scopes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ParentScopeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scopes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scopes_Scopes_ParentScopeId",
                        column: x => x.ParentScopeId,
                        principalTable: "Scopes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Term",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Term", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    YearOfEstablishment = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.Id);
                    table.CheckConstraint("CK_University_YearOfEstablishment", "[YearOfEstablishment] >= 1800 AND [YearOfEstablishment] <= YEAR(GETDATE())");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => new { x.UserId, x.Id });
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaimOverrides",
                columns: table => new
                {
                    ClaimValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaimOverrides", x => new { x.ApplicationUserId, x.ClaimValue });
                    table.ForeignKey(
                        name: "FK_UserClaimOverrides_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ScopeId = table.Column<int>(type: "int", nullable: false),
                    ParentRoleId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoles_AspNetRoles_ParentRoleId",
                        column: x => x.ParentRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetRoles_Scopes_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "Scopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faculties_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UniversityUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversityUsers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UniversityUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UniversityUsers_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AcademicYear",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYear", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicYear_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    HeadOfDepartment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacultyUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyUsers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_FacultyUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FacultyUsers_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Period",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Period", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Period_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaimOverrides",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaimOverrides", x => new { x.RoleId, x.FacultyId, x.ClaimValue });
                    table.ForeignKey(
                        name: "FK_RoleClaimOverrides_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleClaimOverrides_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditHours = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "YearTerm",
                columns: table => new
                {
                    YearId = table.Column<int>(type: "int", nullable: false),
                    TermId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearTerm", x => new { x.YearId, x.TermId });
                    table.ForeignKey(
                        name: "FK_YearTerm_AcademicYear_YearId",
                        column: x => x.YearId,
                        principalTable: "AcademicYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YearTerm_Term_TermId",
                        column: x => x.TermId,
                        principalTable: "Term",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentUsers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_DepartmentUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentUsers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreditHours = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Programs_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectPrerequisites",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    PrerequisiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectPrerequisites", x => new { x.SubjectId, x.PrerequisiteId });
                    table.ForeignKey(
                        name: "FK_SubjectPrerequisites_Subjects_PrerequisiteId",
                        column: x => x.PrerequisiteId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectPrerequisites_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LectureSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    YearId = table.Column<int>(type: "int", nullable: false),
                    TermId = table.Column<int>(type: "int", nullable: false),
                    MaxSlots = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LectureSchedules_Day_DayId",
                        column: x => x.DayId,
                        principalTable: "Day",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LectureSchedules_DepartmentUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "DepartmentUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LectureSchedules_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Period",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LectureSchedules_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LectureSchedules_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LectureSchedules_YearTerm_YearId_TermId",
                        columns: x => new { x.YearId, x.TermId },
                        principalTable: "YearTerm",
                        principalColumns: new[] { "YearId", "TermId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgramSubject",
                columns: table => new
                {
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramSubject", x => new { x.ProgramId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_ProgramSubject_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramSubject_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SectionSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    YearId = table.Column<int>(type: "int", nullable: false),
                    TermId = table.Column<int>(type: "int", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    MaxSlots = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionSchedules_Day_DayId",
                        column: x => x.DayId,
                        principalTable: "Day",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SectionSchedules_DepartmentUsers_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "DepartmentUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SectionSchedules_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Period",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SectionSchedules_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SectionSchedules_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SectionSchedules_YearTerm_YearId_TermId",
                        columns: x => new { x.YearId, x.TermId },
                        principalTable: "YearTerm",
                        principalColumns: new[] { "YearId", "TermId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentProgramYearTerm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    YearId = table.Column<int>(type: "int", nullable: false),
                    TermId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProgramYearTerm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentProgramYearTerm_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentProgramYearTerm_Students_UserId",
                        column: x => x.UserId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentProgramYearTerm_YearTerm_YearId_TermId",
                        columns: x => new { x.YearId, x.TermId },
                        principalTable: "YearTerm",
                        principalColumns: new[] { "YearId", "TermId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LectureRegistrations",
                columns: table => new
                {
                    LectureScheduleId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    GPA = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureRegistrations", x => new { x.StudentId, x.LectureScheduleId });
                    table.ForeignKey(
                        name: "FK_LectureRegistrations_LectureSchedules_LectureScheduleId",
                        column: x => x.LectureScheduleId,
                        principalTable: "LectureSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LectureRegistrations_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SectionRegistrations",
                columns: table => new
                {
                    SectionScheduleId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionRegistrations", x => new { x.StudentId, x.SectionScheduleId });
                    table.ForeignKey(
                        name: "FK_SectionRegistrations_SectionSchedules_SectionScheduleId",
                        column: x => x.SectionScheduleId,
                        principalTable: "SectionSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SectionRegistrations_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "Gender", "IsDeleted", "LockoutEnabled", "LockoutEnd", "Name", "NationalId", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Religion", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 100, 0, new DateOnly(1, 1, 1), "F0DD1622-6D6B-4654-9F52-82EDE53E5AD8", "system-admin@manara.org", true, 0, false, false, null, "", "", "SYSTEM-ADMIN@MANARA.ORG", "SYSTEM-ADMIN@MANARA.ORG", "AQAAAAIAAYagAAAAEE9EyJUN4Xz2bvn2+h3p+oAlYKNgZ0pdEOC/OGIcSmBUG2cPPtftxmy87pluEQ6pLw==", null, false, 0, "5965A8A5-ACBA-46EE-8612-4F0771FDFAB8", false, "system-admin@manara.org" });

            migrationBuilder.InsertData(
                table: "Day",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 1, "Saturday" },
                    { 2, "Sunday" },
                    { 3, "Monday" },
                    { 4, "Tuesday" },
                    { 5, "Wednesday" },
                    { 6, "Thursday" },
                    { 7, "Friday" }
                });

            migrationBuilder.InsertData(
                table: "Scopes",
                columns: new[] { "Id", "IsDeleted", "Name", "ParentScopeId" },
                values: new object[] { 1, false, "System", null });

            migrationBuilder.InsertData(
                table: "Term",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "First Term" },
                    { 2, "Second Term" },
                    { 3, "Summer Term" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Description", "IsDeleted", "Name", "NormalizedName", "ParentRoleId", "ScopeId" },
                values: new object[] { 100, "SYS_ADMIN", "AE6C6754-0862-4EA2-8868-BF5C27E7AEF9", "System Administrator with full access", false, "SystemAdmin", "SYSTEMADMIN", null, 1 });

            migrationBuilder.InsertData(
                table: "Scopes",
                columns: new[] { "Id", "IsDeleted", "Name", "ParentScopeId" },
                values: new object[] { 2, false, "University", 1 });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "roles:read", 100 },
                    { 2, "permissions", "roles:create", 100 },
                    { 3, "permissions", "roles:update", 100 },
                    { 4, "permissions", "roles:toggleStatus", 100 },
                    { 5, "permissions", "permissions:read", 100 },
                    { 6, "permissions", "permissions:update", 100 },
                    { 7, "permissions", "permissions:create", 100 },
                    { 8, "permissions", "permissions:toggleStatus", 100 },
                    { 9, "permissions", "universities:read", 100 },
                    { 10, "permissions", "universities:create", 100 },
                    { 11, "permissions", "universities:update", 100 },
                    { 12, "permissions", "universities:toggleStatus", 100 },
                    { 13, "permissions", "faculties:read", 100 },
                    { 14, "permissions", "faculties:create", 100 },
                    { 15, "permissions", "faculties:update", 100 },
                    { 16, "permissions", "faculties:toggleStatus", 100 },
                    { 17, "permissions", "departments:read", 100 },
                    { 18, "permissions", "departments:create", 100 },
                    { 19, "permissions", "departments:update", 100 },
                    { 20, "permissions", "departments:toggleStatus", 100 },
                    { 21, "permissions", "Programs:read", 100 },
                    { 22, "permissions", "Programs:create", 100 },
                    { 23, "permissions", "Programs:update", 100 },
                    { 24, "permissions", "Programs:toggleStatus", 100 },
                    { 25, "permissions", "programSubjects:read", 100 },
                    { 26, "permissions", "programSubjects:add", 100 },
                    { 27, "permissions", "programSubjects:remove", 100 },
                    { 28, "permissions", "programLecturesSchedule:read", 100 },
                    { 29, "permissions", "programLecturesSchedule:save", 100 },
                    { 30, "permissions", "programSectionsSchedule:read", 100 },
                    { 31, "permissions", "programSectionsSchedule:save", 100 },
                    { 32, "permissions", "universityUsers:read", 100 },
                    { 33, "permissions", "universityUsers:create", 100 },
                    { 34, "permissions", "universityUsers:update", 100 },
                    { 35, "permissions", "universityUsers:toggleStatus", 100 },
                    { 36, "permissions", "facultyUsers:read", 100 },
                    { 37, "permissions", "facultyUsers:create", 100 },
                    { 38, "permissions", "facultyUsers:update", 100 },
                    { 39, "permissions", "facultyUsers:toggleStatus", 100 },
                    { 40, "permissions", "departmentUsers:read", 100 },
                    { 41, "permissions", "departmentUsers:create", 100 },
                    { 42, "permissions", "departmentUsers:update", 100 },
                    { 43, "permissions", "departmentUsers:toggleStatus", 100 },
                    { 44, "permissions", "programUsers:read", 100 },
                    { 45, "permissions", "programUsers:create", 100 },
                    { 46, "permissions", "programUsers:update", 100 },
                    { 47, "permissions", "programUsers:toggleStatus", 100 },
                    { 48, "permissions", "scopes:read", 100 },
                    { 49, "permissions", "scopes:readDetail", 100 },
                    { 50, "permissions", "scopes:create", 100 },
                    { 51, "permissions", "scopes:update", 100 },
                    { 52, "permissions", "scopes:toggleStatus", 100 },
                    { 53, "permissions", "subjects:read", 100 },
                    { 54, "permissions", "subjects:create", 100 },
                    { 55, "permissions", "subjects:update", 100 },
                    { 56, "permissions", "subjects:toggleStatus", 100 },
                    { 57, "permissions", "years:read", 100 },
                    { 58, "permissions", "years:create", 100 },
                    { 59, "permissions", "years:update", 100 },
                    { 60, "permissions", "years:toggleStatus", 100 },
                    { 61, "permissions", "periods:read", 100 },
                    { 62, "permissions", "periods:create", 100 },
                    { 63, "permissions", "periods:update", 100 },
                    { 64, "permissions", "periods:toggleStatus", 100 },
                    { 65, "permissions", "enrollments:read", 100 },
                    { 66, "permissions", "enrollments:create", 100 },
                    { 67, "permissions", "enrollments:update", 100 },
                    { 68, "permissions", "enrollments:toggleStatus", 100 },
                    { 69, "permissions", "studentsPortal:read", 100 },
                    { 70, "permissions", "studentsPortal:create", 100 },
                    { 71, "permissions", "studentsPortal:update", 100 },
                    { 72, "permissions", "studentsPortal:updateGrade", 100 },
                    { 73, "permissions", "studentsPortal:toggleStatus", 100 }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Description", "IsDeleted", "Name", "NormalizedName", "ParentRoleId", "ScopeId" },
                values: new object[] { 101, "UNI_ADMIN", "B1A2C3D4-5E6F-7890-ABCD-EF1234567890", "University Administrator", false, "UniversityAdmin", "UNIVERSITYADMIN", null, 2 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 100, 100 });

            migrationBuilder.InsertData(
                table: "Scopes",
                columns: new[] { "Id", "IsDeleted", "Name", "ParentScopeId" },
                values: new object[] { 3, false, "Faculty", 2 });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Description", "IsDeleted", "Name", "NormalizedName", "ParentRoleId", "ScopeId" },
                values: new object[] { 102, "FAC_ADMIN", "D3C4E5F6-7081-9012-CDEF-345678901234", "Faculty Administrator", false, "FacultyAdmin", "FACULTYADMIN", null, 3 });

            migrationBuilder.InsertData(
                table: "Scopes",
                columns: new[] { "Id", "IsDeleted", "Name", "ParentScopeId" },
                values: new object[] { 4, false, "Department", 3 });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Description", "IsDeleted", "Name", "NormalizedName", "ParentRoleId", "ScopeId" },
                values: new object[,]
                {
                    { 103, "FAC_COORD", "E4D5F6A7-8192-0123-DEF4-456789012345", "Faculty Coordinator", false, "FacultyCoordinator", "FACULTYCOORDINATOR", 102, 3 },
                    { 104, "ACAD_ADV", "F5E6A7B8-9203-1234-EF45-567890123456", "Academic Advisor", false, "AcademicAdvisor", "ACADEMICADVISOR", 102, 3 },
                    { 105, "DEPT_ADMIN", "A6F7B8C9-0314-2345-F456-678901234567", "Department Administrator", false, "DepartmentHead", "DEPARTMENTHEAD", null, 4 }
                });

            migrationBuilder.InsertData(
                table: "Scopes",
                columns: new[] { "Id", "IsDeleted", "Name", "ParentScopeId" },
                values: new object[] { 5, false, "Program", 4 });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Description", "IsDeleted", "Name", "NormalizedName", "ParentRoleId", "ScopeId" },
                values: new object[,]
                {
                    { 106, "DOCTOR", "B7A8C9D0-1425-3456-A567-789012345678", "Doctor/Professor", false, "Doctor", "DOCTOR", 105, 4 },
                    { 110, "MAIN_STUDENT", "F1E2A3B4-5869-789A-E901-123456789012", "Main Stream Student", false, "MainStreamStudent", "MAINSTREAMSTUDENT", null, 5 },
                    { 111, "GPA_STUDENT", "A2F3B4C5-6970-89AB-F012-234567890123", "GPA Student", false, "GPAStudent", "GPASTUDENT", null, 5 },
                    { 107, "INSTRUCTOR", "C8B9D0E1-2536-4567-B678-890123456789", "Instructor/Teaching Assistant", false, "Instructor", "INSTRUCTOR", 106, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYear_FacultyId",
                table: "AcademicYear",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_Code",
                table: "AspNetRoles",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_ParentRoleId",
                table: "AspNetRoles",
                column: "ParentRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_ScopeId",
                table: "AspNetRoles",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FacultyId",
                table: "Departments",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUsers_DepartmentId",
                table: "DepartmentUsers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_UniversityId",
                table: "Faculties",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyUsers_FacultyId",
                table: "FacultyUsers",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureRegistrations_LectureScheduleId",
                table: "LectureRegistrations",
                column: "LectureScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedules_DayId",
                table: "LectureSchedules",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedules_DoctorId",
                table: "LectureSchedules",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedules_PeriodId",
                table: "LectureSchedules",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedules_ProgramId",
                table: "LectureSchedules",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedules_SubjectId",
                table: "LectureSchedules",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedules_YearId_TermId",
                table: "LectureSchedules",
                columns: new[] { "YearId", "TermId" });

            migrationBuilder.CreateIndex(
                name: "IX_Period_FacultyId",
                table: "Period",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_DepartmentId",
                table: "Programs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSubject_SubjectId",
                table: "ProgramSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaimOverrides_FacultyId",
                table: "RoleClaimOverrides",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Scopes_ParentScopeId",
                table: "Scopes",
                column: "ParentScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionRegistrations_SectionScheduleId",
                table: "SectionRegistrations",
                column: "SectionScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSchedules_DayId",
                table: "SectionSchedules",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSchedules_InstructorId",
                table: "SectionSchedules",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSchedules_PeriodId",
                table: "SectionSchedules",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSchedules_ProgramId",
                table: "SectionSchedules",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSchedules_SubjectId",
                table: "SectionSchedules",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSchedules_YearId_TermId",
                table: "SectionSchedules",
                columns: new[] { "YearId", "TermId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgramYearTerm_ProgramId",
                table: "StudentProgramYearTerm",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgramYearTerm_UserId_ProgramId",
                table: "StudentProgramYearTerm",
                columns: new[] { "UserId", "ProgramId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgramYearTerm_UserId_ProgramId_YearId_TermId",
                table: "StudentProgramYearTerm",
                columns: new[] { "UserId", "ProgramId", "YearId", "TermId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgramYearTerm_UserId_YearId_TermId",
                table: "StudentProgramYearTerm",
                columns: new[] { "UserId", "YearId", "TermId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgramYearTerm_YearId_TermId",
                table: "StudentProgramYearTerm",
                columns: new[] { "YearId", "TermId" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_FacultyId",
                table: "Students",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectPrerequisites_PrerequisiteId",
                table: "SubjectPrerequisites",
                column: "PrerequisiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_FacultyId",
                table: "Subjects",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Universities_Name",
                table: "Universities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UniversityUsers_UniversityId",
                table: "UniversityUsers",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_YearTerm_TermId",
                table: "YearTerm",
                column: "TermId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FacultyUsers");

            migrationBuilder.DropTable(
                name: "LectureRegistrations");

            migrationBuilder.DropTable(
                name: "ProgramSubject");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "RoleClaimOverrides");

            migrationBuilder.DropTable(
                name: "SectionRegistrations");

            migrationBuilder.DropTable(
                name: "StudentProgramYearTerm");

            migrationBuilder.DropTable(
                name: "SubjectPrerequisites");

            migrationBuilder.DropTable(
                name: "UniversityUsers");

            migrationBuilder.DropTable(
                name: "UserClaimOverrides");

            migrationBuilder.DropTable(
                name: "LectureSchedules");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "SectionSchedules");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Scopes");

            migrationBuilder.DropTable(
                name: "Day");

            migrationBuilder.DropTable(
                name: "DepartmentUsers");

            migrationBuilder.DropTable(
                name: "Period");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "YearTerm");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "AcademicYear");

            migrationBuilder.DropTable(
                name: "Term");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Universities");
        }
    }
}
