using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class change_table_name_program_schedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramSchedule");

            migrationBuilder.CreateTable(
                name: "ProgramSubjectPeriodDay",
                columns: table => new
                {
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramSubjectPeriodDay", x => new { x.ProgramId, x.SubjectId, x.PeriodId, x.DayId });
                    table.ForeignKey(
                        name: "FK_ProgramSubjectPeriodDay_Day_DayId",
                        column: x => x.DayId,
                        principalTable: "Day",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramSubjectPeriodDay_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Period",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramSubjectPeriodDay_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramSubjectPeriodDay_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSubjectPeriodDay_DayId",
                table: "ProgramSubjectPeriodDay",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSubjectPeriodDay_PeriodId",
                table: "ProgramSubjectPeriodDay",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSubjectPeriodDay_SubjectId",
                table: "ProgramSubjectPeriodDay",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramSubjectPeriodDay");

            migrationBuilder.CreateTable(
                name: "ProgramSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramSchedule_Day_DayId",
                        column: x => x.DayId,
                        principalTable: "Day",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramSchedule_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Period",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramSchedule_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramSchedule_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSchedule_DayId",
                table: "ProgramSchedule",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSchedule_PeriodId",
                table: "ProgramSchedule",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSchedule_ProgramId",
                table: "ProgramSchedule",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSchedule_SubjectId",
                table: "ProgramSchedule",
                column: "SubjectId");
        }
    }
}
