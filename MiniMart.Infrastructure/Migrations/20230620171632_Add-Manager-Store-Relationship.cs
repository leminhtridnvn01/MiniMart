using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMart.Infrastructure.Migrations
{
    public partial class AddManagerStoreRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Stores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stores_ManagerId",
                table: "Stores",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Managers_ManagerId",
                table: "Stores",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Managers_ManagerId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_ManagerId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Stores");
        }
    }
}
