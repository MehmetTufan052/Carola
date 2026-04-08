using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carola.DataAccessLayer.Migrations
{
    public partial class mig_change_authorized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorizePerson",
                table: "Locations",
                newName: "AuthorizedPerson");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorizedPerson",
                table: "Locations",
                newName: "AuthorizePerson");
        }
    }
}
