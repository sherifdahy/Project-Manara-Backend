using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dayremovemigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Day_Faculties_FacultyId",
                table: "Day");

            migrationBuilder.DropIndex(
                name: "IX_Day_FacultyId",
                table: "Day");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Day");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Day",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Day_FacultyId",
                table: "Day",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Day_Faculties_FacultyId",
                table: "Day",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id");
        }
    }
}
