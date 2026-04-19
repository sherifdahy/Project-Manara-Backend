using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addIdToStudentProgramYearTerm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentProgramYearTerm",
                table: "StudentProgramYearTerm");

            migrationBuilder.DropIndex(
                name: "IX_StudentProgramYearTerm_UserId",
                table: "StudentProgramYearTerm");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StudentProgramYearTerm",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentProgramYearTerm",
                table: "StudentProgramYearTerm",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgramYearTerm_ProgramId",
                table: "StudentProgramYearTerm",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgramYearTerm_UserId_ProgramId_YearId_TermId",
                table: "StudentProgramYearTerm",
                columns: new[] { "UserId", "ProgramId", "YearId", "TermId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentProgramYearTerm",
                table: "StudentProgramYearTerm");

            migrationBuilder.DropIndex(
                name: "IX_StudentProgramYearTerm_ProgramId",
                table: "StudentProgramYearTerm");

            migrationBuilder.DropIndex(
                name: "IX_StudentProgramYearTerm_UserId_ProgramId_YearId_TermId",
                table: "StudentProgramYearTerm");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentProgramYearTerm");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentProgramYearTerm",
                table: "StudentProgramYearTerm",
                columns: new[] { "ProgramId", "YearId", "TermId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgramYearTerm_UserId",
                table: "StudentProgramYearTerm",
                column: "UserId");
        }
    }
}
