using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SyncWithLatestChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Period_Day_DayId",
                table: "Period");

            migrationBuilder.DropIndex(
                name: "IX_Period_DayId",
                table: "Period");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "Period");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "Period",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Period_DayId",
                table: "Period",
                column: "DayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Period_Day_DayId",
                table: "Period",
                column: "DayId",
                principalTable: "Day",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
