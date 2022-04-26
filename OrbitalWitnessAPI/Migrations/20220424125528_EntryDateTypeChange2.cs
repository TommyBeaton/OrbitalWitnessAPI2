using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitalWitnessAPI.Migrations
{
    public partial class EntryDateTypeChange2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryDate",
                table: "ParsedSchedules");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EntryDate",
                table: "ParsedSchedules",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
