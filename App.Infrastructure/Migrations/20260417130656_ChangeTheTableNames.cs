using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTheTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramUsers_Programs_ProgramId",
                table: "ProgramUsers");

            migrationBuilder.DropTable(
                name: "ProgramUserProgramYearTerm");

            migrationBuilder.DropIndex(
                name: "IX_ProgramUsers_ProgramId",
                table: "ProgramUsers");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "ProgramUsers");

            migrationBuilder.CreateTable(
                name: "StudentProgramYearTerm",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    YearId = table.Column<int>(type: "int", nullable: false),
                    TermId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProgramYearTerm", x => new { x.ProgramId, x.YearId, x.TermId, x.UserId });
                    table.ForeignKey(
                        name: "FK_StudentProgramYearTerm_ProgramUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ProgramUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentProgramYearTerm_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentProgramYearTerm_YearTerm_YearId_TermId",
                        columns: x => new { x.YearId, x.TermId },
                        principalTable: "YearTerm",
                        principalColumns: new[] { "YearId", "TermId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgramYearTerm_UserId",
                table: "StudentProgramYearTerm",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgramYearTerm_YearId_TermId",
                table: "StudentProgramYearTerm",
                columns: new[] { "YearId", "TermId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentProgramYearTerm");

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "ProgramUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProgramUserProgramYearTerm",
                columns: table => new
                {
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    YearId = table.Column<int>(type: "int", nullable: false),
                    TermId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramUserProgramYearTerm", x => new { x.ProgramId, x.YearId, x.TermId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ProgramUserProgramYearTerm_ProgramUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ProgramUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramUserProgramYearTerm_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramUserProgramYearTerm_YearTerm_YearId_TermId",
                        columns: x => new { x.YearId, x.TermId },
                        principalTable: "YearTerm",
                        principalColumns: new[] { "YearId", "TermId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUsers_ProgramId",
                table: "ProgramUsers",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUserProgramYearTerm_UserId",
                table: "ProgramUserProgramYearTerm",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramUserProgramYearTerm_YearId_TermId",
                table: "ProgramUserProgramYearTerm",
                columns: new[] { "YearId", "TermId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramUsers_Programs_ProgramId",
                table: "ProgramUsers",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id");
        }
    }
}
