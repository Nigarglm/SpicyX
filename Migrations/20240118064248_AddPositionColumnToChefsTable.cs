using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpicyX.Migrations
{
    public partial class AddPositionColumnToChefsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Chefs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Chefs");
        }
    }
}
