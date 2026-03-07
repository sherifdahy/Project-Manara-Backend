using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Faculties_FacultyId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentUserSubjectYearTermPeriod_DepartmentUsers_UserId",
                table: "DepartmentUserSubjectYearTermPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentUserSubjectYearTermPeriod_Period_PeriodId",
                table: "DepartmentUserSubjectYearTermPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentUserSubjectYearTermPeriod_Subjects_SubjectId",
                table: "DepartmentUserSubjectYearTermPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentUserSubjectYearTermPeriod_YearTerm_YearTermYearId_YearTermTermId",
                table: "DepartmentUserSubjectYearTermPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramUserProgramYearTerm_ProgramUsers_UserId",
                table: "ProgramUserProgramYearTerm");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramUserProgramYearTerm_Programs_ProgramId",
                table: "ProgramUserProgramYearTerm");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramUserProgramYearTerm_YearTerm_YearTermYearId_YearTermTermId",
                table: "ProgramUserProgramYearTerm");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramUsers_Programs_ProgramId",
                table: "ProgramUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_YearTerm_AcademicYear_YearId",
                table: "YearTerm");

            migrationBuilder.DropForeignKey(
                name: "FK_YearTerm_Term_TermId",
                table: "YearTerm");

            migrationBuilder.DropTable(
                name: "FAQ");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramId",
                table: "ProgramUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TermId",
                table: "DepartmentUserSubjectYearTermPeriod",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUserSubjectYearTermPeriod_TermId",
                table: "DepartmentUserSubjectYearTermPeriod",
                column: "TermId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Faculties_FacultyId",
                table: "AcademicYear",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentUserSubjectYearTermPeriod_DepartmentUsers_UserId",
                table: "DepartmentUserSubjectYearTermPeriod",
                column: "UserId",
                principalTable: "DepartmentUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentUserSubjectYearTermPeriod_Period_PeriodId",
                table: "DepartmentUserSubjectYearTermPeriod",
                column: "PeriodId",
                principalTable: "Period",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentUserSubjectYearTermPeriod_Subjects_SubjectId",
                table: "DepartmentUserSubjectYearTermPeriod",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentUserSubjectYearTermPeriod_Term_TermId",
                table: "DepartmentUserSubjectYearTermPeriod",
                column: "TermId",
                principalTable: "Term",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentUserSubjectYearTermPeriod_YearTerm_YearTermYearId_YearTermTermId",
                table: "DepartmentUserSubjectYearTermPeriod",
                columns: new[] { "YearTermYearId", "YearTermTermId" },
                principalTable: "YearTerm",
                principalColumns: new[] { "YearId", "TermId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramUserProgramYearTerm_ProgramUsers_UserId",
                table: "ProgramUserProgramYearTerm",
                column: "UserId",
                principalTable: "ProgramUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramUserProgramYearTerm_Programs_ProgramId",
                table: "ProgramUserProgramYearTerm",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramUserProgramYearTerm_YearTerm_YearTermYearId_YearTermTermId",
                table: "ProgramUserProgramYearTerm",
                columns: new[] { "YearTermYearId", "YearTermTermId" },
                principalTable: "YearTerm",
                principalColumns: new[] { "YearId", "TermId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramUsers_Programs_ProgramId",
                table: "ProgramUsers",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_YearTerm_AcademicYear_YearId",
                table: "YearTerm",
                column: "YearId",
                principalTable: "AcademicYear",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_YearTerm_Term_TermId",
                table: "YearTerm",
                column: "TermId",
                principalTable: "Term",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Faculties_FacultyId",
                table: "AcademicYear");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentUserSubjectYearTermPeriod_DepartmentUsers_UserId",
                table: "DepartmentUserSubjectYearTermPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentUserSubjectYearTermPeriod_Period_PeriodId",
                table: "DepartmentUserSubjectYearTermPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentUserSubjectYearTermPeriod_Subjects_SubjectId",
                table: "DepartmentUserSubjectYearTermPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentUserSubjectYearTermPeriod_Term_TermId",
                table: "DepartmentUserSubjectYearTermPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentUserSubjectYearTermPeriod_YearTerm_YearTermYearId_YearTermTermId",
                table: "DepartmentUserSubjectYearTermPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramUserProgramYearTerm_ProgramUsers_UserId",
                table: "ProgramUserProgramYearTerm");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramUserProgramYearTerm_Programs_ProgramId",
                table: "ProgramUserProgramYearTerm");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramUserProgramYearTerm_YearTerm_YearTermYearId_YearTermTermId",
                table: "ProgramUserProgramYearTerm");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramUsers_Programs_ProgramId",
                table: "ProgramUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_YearTerm_AcademicYear_YearId",
                table: "YearTerm");

            migrationBuilder.DropForeignKey(
                name: "FK_YearTerm_Term_TermId",
                table: "YearTerm");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentUserSubjectYearTermPeriod_TermId",
                table: "DepartmentUserSubjectYearTermPeriod");

            migrationBuilder.DropColumn(
                name: "TermId",
                table: "DepartmentUserSubjectYearTermPeriod");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramId",
                table: "ProgramUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "FAQ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQ", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FAQ_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FAQ_SubjectId",
                table: "FAQ",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Faculties_FacultyId",
                table: "AcademicYear",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentUserSubjectYearTermPeriod_DepartmentUsers_UserId",
                table: "DepartmentUserSubjectYearTermPeriod",
                column: "UserId",
                principalTable: "DepartmentUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentUserSubjectYearTermPeriod_Period_PeriodId",
                table: "DepartmentUserSubjectYearTermPeriod",
                column: "PeriodId",
                principalTable: "Period",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentUserSubjectYearTermPeriod_Subjects_SubjectId",
                table: "DepartmentUserSubjectYearTermPeriod",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentUserSubjectYearTermPeriod_YearTerm_YearTermYearId_YearTermTermId",
                table: "DepartmentUserSubjectYearTermPeriod",
                columns: new[] { "YearTermYearId", "YearTermTermId" },
                principalTable: "YearTerm",
                principalColumns: new[] { "YearId", "TermId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramUserProgramYearTerm_ProgramUsers_UserId",
                table: "ProgramUserProgramYearTerm",
                column: "UserId",
                principalTable: "ProgramUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramUserProgramYearTerm_Programs_ProgramId",
                table: "ProgramUserProgramYearTerm",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramUserProgramYearTerm_YearTerm_YearTermYearId_YearTermTermId",
                table: "ProgramUserProgramYearTerm",
                columns: new[] { "YearTermYearId", "YearTermTermId" },
                principalTable: "YearTerm",
                principalColumns: new[] { "YearId", "TermId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramUsers_Programs_ProgramId",
                table: "ProgramUsers",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_YearTerm_AcademicYear_YearId",
                table: "YearTerm",
                column: "YearId",
                principalTable: "AcademicYear",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_YearTerm_Term_TermId",
                table: "YearTerm",
                column: "TermId",
                principalTable: "Term",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
