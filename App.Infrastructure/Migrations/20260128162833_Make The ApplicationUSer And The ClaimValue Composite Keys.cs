using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeTheApplicationUSerAndTheClaimValueCompositeKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermissionOverride",
                table: "UserPermissionOverride");

            migrationBuilder.DropIndex(
                name: "IX_UserPermissionOverride_ApplicationUserId",
                table: "UserPermissionOverride");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserPermissionOverride");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermissionOverride",
                table: "UserPermissionOverride",
                columns: new[] { "ApplicationUserId", "ClaimValue" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermissionOverride",
                table: "UserPermissionOverride");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserPermissionOverride",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermissionOverride",
                table: "UserPermissionOverride",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionOverride_ApplicationUserId",
                table: "UserPermissionOverride",
                column: "ApplicationUserId");
        }
    }
}
