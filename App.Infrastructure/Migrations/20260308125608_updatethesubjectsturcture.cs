using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatethesubjectsturcture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Subjects_ParentSubjectId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ParentSubjectId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "ParentSubjectId",
                table: "Subjects");

            migrationBuilder.CreateTable(
                name: "SubjectPrerequisite",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    PrerequisiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectPrerequisite", x => new { x.SubjectId, x.PrerequisiteId });
                    table.ForeignKey(
                        name: "FK_SubjectPrerequisite_Subjects_PrerequisiteId",
                        column: x => x.PrerequisiteId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectPrerequisite_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectPrerequisite_PrerequisiteId",
                table: "SubjectPrerequisite",
                column: "PrerequisiteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectPrerequisite");

            migrationBuilder.AddColumn<int>(
                name: "ParentSubjectId",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ParentSubjectId",
                table: "Subjects",
                column: "ParentSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Subjects_ParentSubjectId",
                table: "Subjects",
                column: "ParentSubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }
    }
}
