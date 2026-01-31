using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RoleCalimOverrideWithRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RoleClaimOverride_FacultyId",
                table: "RoleClaimOverride",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaimOverride_AspNetRoles_RoleId",
                table: "RoleClaimOverride",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaimOverride_Faculties_FacultyId",
                table: "RoleClaimOverride",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaimOverride_AspNetRoles_RoleId",
                table: "RoleClaimOverride");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaimOverride_Faculties_FacultyId",
                table: "RoleClaimOverride");

            migrationBuilder.DropIndex(
                name: "IX_RoleClaimOverride_FacultyId",
                table: "RoleClaimOverride");
        }
    }
}
