using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_relation_sectionSchedule_lectureSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedules_AcademicYear_YearId",
                table: "LectureSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedules_Term_TermId",
                table: "LectureSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedules_YearTerm_YearTermYearId_YearTermTermId",
                table: "LectureSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSchedules_Term_TermId",
                table: "SectionSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSchedules_YearTerm_YearTermYearId_YearTermTermId",
                table: "SectionSchedules");

            migrationBuilder.DropIndex(
                name: "IX_SectionSchedules_TermId",
                table: "SectionSchedules");

            migrationBuilder.DropIndex(
                name: "IX_SectionSchedules_YearTermYearId_YearTermTermId",
                table: "SectionSchedules");

            migrationBuilder.DropIndex(
                name: "IX_LectureSchedules_TermId",
                table: "LectureSchedules");

            migrationBuilder.DropIndex(
                name: "IX_LectureSchedules_YearId",
                table: "LectureSchedules");

            migrationBuilder.DropIndex(
                name: "IX_LectureSchedules_YearTermYearId_YearTermTermId",
                table: "LectureSchedules");

            migrationBuilder.DropColumn(
                name: "YearTermTermId",
                table: "SectionSchedules");

            migrationBuilder.DropColumn(
                name: "YearTermYearId",
                table: "SectionSchedules");

            migrationBuilder.DropColumn(
                name: "YearTermTermId",
                table: "LectureSchedules");

            migrationBuilder.DropColumn(
                name: "YearTermYearId",
                table: "LectureSchedules");

            migrationBuilder.CreateIndex(
                name: "IX_SectionSchedules_YearId_TermId",
                table: "SectionSchedules",
                columns: new[] { "YearId", "TermId" });

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedules_YearId_TermId",
                table: "LectureSchedules",
                columns: new[] { "YearId", "TermId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LectureSchedules_YearTerm_YearId_TermId",
                table: "LectureSchedules",
                columns: new[] { "YearId", "TermId" },
                principalTable: "YearTerm",
                principalColumns: new[] { "YearId", "TermId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSchedules_YearTerm_YearId_TermId",
                table: "SectionSchedules",
                columns: new[] { "YearId", "TermId" },
                principalTable: "YearTerm",
                principalColumns: new[] { "YearId", "TermId" },
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedules_YearTerm_YearId_TermId",
                table: "LectureSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionSchedules_YearTerm_YearId_TermId",
                table: "SectionSchedules");

            migrationBuilder.DropIndex(
                name: "IX_SectionSchedules_YearId_TermId",
                table: "SectionSchedules");

            migrationBuilder.DropIndex(
                name: "IX_LectureSchedules_YearId_TermId",
                table: "LectureSchedules");

            migrationBuilder.AddColumn<int>(
                name: "YearTermTermId",
                table: "SectionSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearTermYearId",
                table: "SectionSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_SectionSchedules_YearTermYearId_YearTermTermId",
                table: "SectionSchedules",
                columns: new[] { "YearTermYearId", "YearTermTermId" });

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedules_TermId",
                table: "LectureSchedules",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedules_YearId",
                table: "LectureSchedules",
                column: "YearId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedules_YearTermYearId_YearTermTermId",
                table: "LectureSchedules",
                columns: new[] { "YearTermYearId", "YearTermTermId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LectureSchedules_AcademicYear_YearId",
                table: "LectureSchedules",
                column: "YearId",
                principalTable: "AcademicYear",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureSchedules_Term_TermId",
                table: "LectureSchedules",
                column: "TermId",
                principalTable: "Term",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_SectionSchedules_YearTerm_YearTermYearId_YearTermTermId",
                table: "SectionSchedules",
                columns: new[] { "YearTermYearId", "YearTermTermId" },
                principalTable: "YearTerm",
                principalColumns: new[] { "YearId", "TermId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
