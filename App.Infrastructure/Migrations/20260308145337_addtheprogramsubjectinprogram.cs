using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtheprogramsubjectinprogram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectPrerequisite_Subjects_PrerequisiteId",
                table: "SubjectPrerequisite");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectPrerequisite_Subjects_SubjectId",
                table: "SubjectPrerequisite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectPrerequisite",
                table: "SubjectPrerequisite");

            migrationBuilder.RenameTable(
                name: "SubjectPrerequisite",
                newName: "SubjectPrerequisites");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectPrerequisite_PrerequisiteId",
                table: "SubjectPrerequisites",
                newName: "IX_SubjectPrerequisites_PrerequisiteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectPrerequisites",
                table: "SubjectPrerequisites",
                columns: new[] { "SubjectId", "PrerequisiteId" });

            // ❌ شيلنا الـ InsertData لأن الداتا موجودة أصلاً

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectPrerequisites_Subjects_PrerequisiteId",
                table: "SubjectPrerequisites",
                column: "PrerequisiteId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectPrerequisites_Subjects_SubjectId",
                table: "SubjectPrerequisites",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectPrerequisites_Subjects_PrerequisiteId",
                table: "SubjectPrerequisites");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectPrerequisites_Subjects_SubjectId",
                table: "SubjectPrerequisites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectPrerequisites",
                table: "SubjectPrerequisites");

            // ❌ شيلنا الـ DeleteData كمان

            migrationBuilder.RenameTable(
                name: "SubjectPrerequisites",
                newName: "SubjectPrerequisite");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectPrerequisites_PrerequisiteId",
                table: "SubjectPrerequisite",
                newName: "IX_SubjectPrerequisite_PrerequisiteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectPrerequisite",
                table: "SubjectPrerequisite",
                columns: new[] { "SubjectId", "PrerequisiteId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectPrerequisite_Subjects_PrerequisiteId",
                table: "SubjectPrerequisite",
                column: "PrerequisiteId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectPrerequisite_Subjects_SubjectId",
                table: "SubjectPrerequisite",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}