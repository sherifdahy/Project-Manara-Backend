using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelationUniversityRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UniversityId",
                table: "AspNetRoles",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 100,
                column: "UniversityId",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 101,
                column: "UniversityId",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 102,
                column: "UniversityId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_UniversityId",
                table: "AspNetRoles",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_Universities_UniversityId",
                table: "AspNetRoles",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_Universities_UniversityId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_UniversityId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "AspNetRoles");
        }
    }
}
