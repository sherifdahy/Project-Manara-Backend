using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_year_term_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedules_YearTerm_YearTermYearId_YearTermTermId",
                table: "LectureSchedules");

            migrationBuilder.DropIndex(
                name: "IX_LectureSchedules_YearTermYearId_YearTermTermId",
                table: "LectureSchedules");

            migrationBuilder.DropColumn(
                name: "YearTermTermId",
                table: "LectureSchedules");

            migrationBuilder.DropColumn(
                name: "YearTermYearId",
                table: "LectureSchedules");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedules_TermId",
                table: "LectureSchedules",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedules_YearId",
                table: "LectureSchedules",
                column: "YearId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedules_AcademicYear_YearId",
                table: "LectureSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureSchedules_Term_TermId",
                table: "LectureSchedules");

            migrationBuilder.DropIndex(
                name: "IX_LectureSchedules_TermId",
                table: "LectureSchedules");

            migrationBuilder.DropIndex(
                name: "IX_LectureSchedules_YearId",
                table: "LectureSchedules");

            migrationBuilder.AddColumn<int>(
                name: "YearTermTermId",
                table: "LectureSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearTermYearId",
                table: "LectureSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedules_YearTermYearId_YearTermTermId",
                table: "LectureSchedules",
                columns: new[] { "YearTermYearId", "YearTermTermId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LectureSchedules_YearTerm_YearTermYearId_YearTermTermId",
                table: "LectureSchedules",
                columns: new[] { "YearTermYearId", "YearTermTermId" },
                principalTable: "YearTerm",
                principalColumns: new[] { "YearId", "TermId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
