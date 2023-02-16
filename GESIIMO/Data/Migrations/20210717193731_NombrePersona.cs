using Microsoft.EntityFrameworkCore.Migrations;

namespace GESIIMO.Data.Migrations
{
    public partial class NombrePersona : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombrePersona",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombrePersona",
                table: "AspNetUsers");
        }
    }
}
