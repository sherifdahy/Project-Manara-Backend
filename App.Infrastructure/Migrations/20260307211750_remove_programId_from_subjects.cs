using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class remove_programId_from_subjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Drop Foreign Key first
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Programs_ProgramId",
                table: "Subjects");

            // 2. Drop Index second
            migrationBuilder.DropIndex(
                name: "IX_Subjects_ProgramId",
                table: "Subjects");

            // 3. Now Drop the Column
            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Subjects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "Subjects",
                nullable: false,
                defaultValue: 0);
        }
    }
}
