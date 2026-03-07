using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_relation_between_programUser_program_yearTerm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "ProgramUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DepartmentUserSubjectYearTermPeriod",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    YearTermId = table.Column<int>(type: "int", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    YearTermYearId = table.Column<int>(type: "int", nullable: false),
                    YearTermTermId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentUserSubjectYearTermPeriod", x => new { x.UserId, x.SubjectId, x.YearTermId, x.PeriodId });
                    table.ForeignKey(
                        name: "FK_DepartmentUserSubjectYearTermPeriod_DepartmentUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "DepartmentUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentUserSubjectYearTermPeriod_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Period",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentUserSubjectYearTermPeriod_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentUserSubjectYearTermPeriod_YearTerm_YearTermYearId_YearTermTermId",
                        columns: x => new { x.YearTermYearId, x.YearTermTermId },
                        principalTable: "YearTerm",
                        principalColumns: new[] { "YearId", "TermId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramUserProgramYearTerm",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    YearTermId = table.Column<int>(type: "int", nullable: false),
                    YearTermYearId = table.Column<int>(type: "int", nullable: false),
                    YearTermTermId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramUserProgramYearTerm", x => new { x.ProgramId, x.YearTermId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ProgramUserProgramYearTerm_ProgramUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ProgramUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramUserProgramYearTerm_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramUserProgramYearTerm_YearTerm_YearTermYearId_YearTermTermId",
                        columns: x => new { x.YearTermYearId, x.YearTermTermId },
                        principalTable: "YearTerm",
                        principalColumns: new[] { "YearId", "TermId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUsers_FacultyId",
                table: "ProgramUsers",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUserSubjectYearTermPeriod_PeriodId",
                table: "DepartmentUserSubjectYearTermPeriod",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUserSubjectYearTermPeriod_SubjectId",
                table: "DepartmentUserSubjectYearTermPeriod",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUserSubjectYearTermPeriod_YearTermYearId_YearTermTermId",
                table: "DepartmentUserSubjectYearTermPeriod",
                columns: new[] { "YearTermYearId", "YearTermTermId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUserProgramYearTerm_UserId",
                table: "ProgramUserProgramYearTerm",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUserProgramYearTerm_YearTermYearId_YearTermTermId",
                table: "ProgramUserProgramYearTerm",
                columns: new[] { "YearTermYearId", "YearTermTermId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramUsers_Faculties_FacultyId",
                table: "ProgramUsers",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramUsers_Faculties_FacultyId",
                table: "ProgramUsers");

            migrationBuilder.DropTable(
                name: "DepartmentUserSubjectYearTermPeriod");

            migrationBuilder.DropTable(
                name: "ProgramUserProgramYearTerm");

            migrationBuilder.DropIndex(
                name: "IX_ProgramUsers_FacultyId",
                table: "ProgramUsers");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "ProgramUsers");
        }
    }
}
