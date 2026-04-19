using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class programUserYearTerm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramUserProgramYearTerm_YearTerm_YearTermYearId_YearTermTermId",
                table: "ProgramUserProgramYearTerm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramUserProgramYearTerm",
                table: "ProgramUserProgramYearTerm");

            migrationBuilder.DropIndex(
                name: "IX_ProgramUserProgramYearTerm_YearTermYearId_YearTermTermId",
                table: "ProgramUserProgramYearTerm");

            migrationBuilder.DropColumn(
                name: "YearTermId",
                table: "ProgramUserProgramYearTerm");

            migrationBuilder.RenameColumn(
                name: "YearTermYearId",
                table: "ProgramUserProgramYearTerm",
                newName: "TermId");

            migrationBuilder.RenameColumn(
                name: "YearTermTermId",
                table: "ProgramUserProgramYearTerm",
                newName: "YearId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramUserProgramYearTerm",
                table: "ProgramUserProgramYearTerm",
                columns: new[] { "ProgramId", "YearId", "TermId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUserProgramYearTerm_YearId_TermId",
                table: "ProgramUserProgramYearTerm",
                columns: new[] { "YearId", "TermId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramUserProgramYearTerm_YearTerm_YearId_TermId",
                table: "ProgramUserProgramYearTerm",
                columns: new[] { "YearId", "TermId" },
                principalTable: "YearTerm",
                principalColumns: new[] { "YearId", "TermId" },
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramUserProgramYearTerm_YearTerm_YearId_TermId",
                table: "ProgramUserProgramYearTerm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramUserProgramYearTerm",
                table: "ProgramUserProgramYearTerm");

            migrationBuilder.DropIndex(
                name: "IX_ProgramUserProgramYearTerm_YearId_TermId",
                table: "ProgramUserProgramYearTerm");

            migrationBuilder.RenameColumn(
                name: "TermId",
                table: "ProgramUserProgramYearTerm",
                newName: "YearTermYearId");

            migrationBuilder.RenameColumn(
                name: "YearId",
                table: "ProgramUserProgramYearTerm",
                newName: "YearTermTermId");

            migrationBuilder.AddColumn<int>(
                name: "YearTermId",
                table: "ProgramUserProgramYearTerm",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramUserProgramYearTerm",
                table: "ProgramUserProgramYearTerm",
                columns: new[] { "ProgramId", "YearTermId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUserProgramYearTerm_YearTermYearId_YearTermTermId",
                table: "ProgramUserProgramYearTerm",
                columns: new[] { "YearTermYearId", "YearTermTermId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramUserProgramYearTerm_YearTerm_YearTermYearId_YearTermTermId",
                table: "ProgramUserProgramYearTerm",
                columns: new[] { "YearTermYearId", "YearTermTermId" },
                principalTable: "YearTerm",
                principalColumns: new[] { "YearId", "TermId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
