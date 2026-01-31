using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTheRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaimOverride_AspNetRoles_RoleId",
                table: "RoleClaimOverride");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaimOverride_Faculties_FacultyId",
                table: "RoleClaimOverride");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaimOverride_AspNetUsers_ApplicationUserId",
                table: "UserClaimOverride");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClaimOverride",
                table: "UserClaimOverride");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleClaimOverride",
                table: "RoleClaimOverride");

            migrationBuilder.RenameTable(
                name: "UserClaimOverride",
                newName: "UserClaimOverrides");

            migrationBuilder.RenameTable(
                name: "RoleClaimOverride",
                newName: "RoleClaimOverrides");

            migrationBuilder.RenameIndex(
                name: "IX_RoleClaimOverride_FacultyId",
                table: "RoleClaimOverrides",
                newName: "IX_RoleClaimOverrides_FacultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClaimOverrides",
                table: "UserClaimOverrides",
                columns: new[] { "ApplicationUserId", "ClaimValue" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleClaimOverrides",
                table: "RoleClaimOverrides",
                columns: new[] { "RoleId", "FacultyId", "ClaimValue" });

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaimOverrides_AspNetRoles_RoleId",
                table: "RoleClaimOverrides",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaimOverrides_Faculties_FacultyId",
                table: "RoleClaimOverrides",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaimOverrides_AspNetUsers_ApplicationUserId",
                table: "UserClaimOverrides",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaimOverrides_AspNetRoles_RoleId",
                table: "RoleClaimOverrides");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaimOverrides_Faculties_FacultyId",
                table: "RoleClaimOverrides");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaimOverrides_AspNetUsers_ApplicationUserId",
                table: "UserClaimOverrides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClaimOverrides",
                table: "UserClaimOverrides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleClaimOverrides",
                table: "RoleClaimOverrides");

            migrationBuilder.RenameTable(
                name: "UserClaimOverrides",
                newName: "UserClaimOverride");

            migrationBuilder.RenameTable(
                name: "RoleClaimOverrides",
                newName: "RoleClaimOverride");

            migrationBuilder.RenameIndex(
                name: "IX_RoleClaimOverrides_FacultyId",
                table: "RoleClaimOverride",
                newName: "IX_RoleClaimOverride_FacultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClaimOverride",
                table: "UserClaimOverride",
                columns: new[] { "ApplicationUserId", "ClaimValue" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleClaimOverride",
                table: "RoleClaimOverride",
                columns: new[] { "RoleId", "FacultyId", "ClaimValue" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaimOverride_AspNetUsers_ApplicationUserId",
                table: "UserClaimOverride",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
