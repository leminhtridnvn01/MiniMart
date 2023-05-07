using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMart.Infrastructure.Migrations
{
    public partial class EditStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Stores_StoreId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_StoreId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Stores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Stores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stores_StoreId",
                table: "Stores",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Stores_StoreId",
                table: "Stores",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");
        }
    }
}
