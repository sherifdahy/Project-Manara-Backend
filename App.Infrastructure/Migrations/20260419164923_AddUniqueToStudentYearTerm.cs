using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueToStudentYearTerm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StudentProgramYearTerm_UserId_ProgramId",
                table: "StudentProgramYearTerm",
                columns: new[] { "UserId", "ProgramId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentProgramYearTerm_UserId_ProgramId",
                table: "StudentProgramYearTerm");
        }
    }
}
