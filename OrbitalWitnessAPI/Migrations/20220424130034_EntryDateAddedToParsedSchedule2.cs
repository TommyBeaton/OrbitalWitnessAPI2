using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitalWitnessAPI.Migrations
{
    public partial class EntryDateAddedToParsedSchedule2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EntryDate",
                table: "ParsedSchedules",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryDate",
                table: "ParsedSchedules");
        }
    }
}
