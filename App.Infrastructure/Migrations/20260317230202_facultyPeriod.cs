using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class facultyPeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Period",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Period_FacultyId",
                table: "Period",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Period_Faculties_FacultyId",
                table: "Period",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Period_Faculties_FacultyId",
                table: "Period");

            migrationBuilder.DropIndex(
                name: "IX_Period_FacultyId",
                table: "Period");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Period");
        }
    }
}
