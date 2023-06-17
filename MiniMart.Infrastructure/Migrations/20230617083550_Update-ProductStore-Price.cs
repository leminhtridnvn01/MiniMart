using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMart.Infrastructure.Migrations
{
    public partial class UpdateProductStorePrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "ProductStores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriceDecreases",
                table: "ProductStores",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductStores");

            migrationBuilder.DropColumn(
                name: "PriceDecreases",
                table: "ProductStores");
        }
    }
}
