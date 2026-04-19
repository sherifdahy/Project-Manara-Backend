using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addanotherUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StudentProgramYearTerm_UserId_YearId_TermId",
                table: "StudentProgramYearTerm",
                columns: new[] { "UserId", "YearId", "TermId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentProgramYearTerm_UserId_YearId_TermId",
                table: "StudentProgramYearTerm");
        }
    }
}
