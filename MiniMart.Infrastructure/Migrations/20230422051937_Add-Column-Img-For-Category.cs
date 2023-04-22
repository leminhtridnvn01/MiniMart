using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMart.Infrastructure.Migrations
{
    public partial class AddColumnImgForCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Categories");
        }
    }
}
