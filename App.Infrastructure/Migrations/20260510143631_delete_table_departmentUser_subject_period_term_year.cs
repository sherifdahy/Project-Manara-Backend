using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class delete_table_departmentUser_subject_period_term_year : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentUserSubjectYearTermPeriod");

            migrationBuilder.AddColumn<int>(
                name: "YearTermTermId",
                table: "LectureSchedules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearTermYearId",
                table: "LectureSchedules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SectionSchedules_TermId",
                table: "SectionSchedules",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedules_YearTermYearId_YearTermTermId",
                table: "LectureSchedules",
                columns: new[] { "YearTermYearId", "YearTermTermId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LectureSchedules_YearTerm_YearTermYearId_YearTermTermId",
                table: "LectureSchedules",
                columns: new[] { "YearTermYearId", "YearTermTermId" },
                principalTable: "YearTerm",
                principalColumns: new[] { "YearId", "TermId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSchedules_Term_TermId",
                table: "SectionSchedules",
                column: "TermId",
                principalTable: "Term",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedules_YearTerm_YearTermYearId_YearTermTermId",
                table: "LectureSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSchedules_Term_TermId",
                table: "SectionSchedules");

            migrationBuilder.DropIndex(
                name: "IX_SectionSchedules_TermId",
                table: "SectionSchedules");

            migrationBuilder.DropIndex(
                name: "IX_LectureSchedules_YearTermYearId_YearTermTermId",
                table: "LectureSchedules");

            migrationBuilder.DropColumn(
                name: "YearTermTermId",
                table: "LectureSchedules");

            migrationBuilder.DropColumn(
                name: "YearTermYearId",
                table: "LectureSchedules");

            migrationBuilder.CreateTable(
                name: "DepartmentUserSubjectYearTermPeriod",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    YearTermId = table.Column<int>(type: "int", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    YearTermYearId = table.Column<int>(type: "int", nullable: false),
                    YearTermTermId = table.Column<int>(type: "int", nullable: false),
                    TermId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentUserSubjectYearTermPeriod", x => new { x.UserId, x.SubjectId, x.YearTermId, x.PeriodId });
                    table.ForeignKey(
                        name: "FK_DepartmentUserSubjectYearTermPeriod_DepartmentUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "DepartmentUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentUserSubjectYearTermPeriod_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Period",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentUserSubjectYearTermPeriod_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentUserSubjectYearTermPeriod_Term_TermId",
                        column: x => x.TermId,
                        principalTable: "Term",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentUserSubjectYearTermPeriod_YearTerm_YearTermYearId_YearTermTermId",
                        columns: x => new { x.YearTermYearId, x.YearTermTermId },
                        principalTable: "YearTerm",
                        principalColumns: new[] { "YearId", "TermId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUserSubjectYearTermPeriod_PeriodId",
                table: "DepartmentUserSubjectYearTermPeriod",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUserSubjectYearTermPeriod_SubjectId",
                table: "DepartmentUserSubjectYearTermPeriod",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUserSubjectYearTermPeriod_TermId",
                table: "DepartmentUserSubjectYearTermPeriod",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUserSubjectYearTermPeriod_YearTermYearId_YearTermTermId",
                table: "DepartmentUserSubjectYearTermPeriod",
                columns: new[] { "YearTermYearId", "YearTermTermId" });
        }
    }
}
