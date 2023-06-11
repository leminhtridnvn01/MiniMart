using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMart.Infrastructure.Migrations
{
    public partial class AddStoreFavouriteProductRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "FavouriteProducts",
                type: "int",
                nullable: false,
                defaultValue: 12);

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteProducts_StoreId",
                table: "FavouriteProducts",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteProducts_Stores_StoreId",
                table: "FavouriteProducts",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteProducts_Stores_StoreId",
                table: "FavouriteProducts");

            migrationBuilder.DropIndex(
                name: "IX_FavouriteProducts_StoreId",
                table: "FavouriteProducts");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "FavouriteProducts");
        }
    }
}
