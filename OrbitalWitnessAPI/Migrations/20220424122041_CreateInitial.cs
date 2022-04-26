using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitalWitnessAPI.Migrations
{
    public partial class CreateInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParsedSchedules",
                columns: table => new
                {
                    ParsedDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RawData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntryNumber = table.Column<int>(type: "int", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationDateAndPlanRef = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfLeaseAndTerm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LesseesTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParsedSchedules", x => x.ParsedDataId);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParsedDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Notes_ParsedSchedules_ParsedDataId",
                        column: x => x.ParsedDataId,
                        principalTable: "ParsedSchedules",
                        principalColumn: "ParsedDataId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ParsedDataId",
                table: "Notes",
                column: "ParsedDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "ParsedSchedules");
        }
    }
}
