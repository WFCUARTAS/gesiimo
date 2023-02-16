using Microsoft.EntityFrameworkCore.Migrations;

namespace GESIIMO.Data.Migrations
{
    public partial class apellidos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApellidosPersona",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApellidosPersona",
                table: "AspNetUsers");
        }
    }
}
