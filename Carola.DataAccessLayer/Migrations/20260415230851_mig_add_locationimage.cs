using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carola.DataAccessLayer.Migrations
{
    public partial class mig_add_locationimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocationImage",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationImage",
                table: "Locations");
        }
    }
}
