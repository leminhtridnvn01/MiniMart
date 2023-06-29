using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMart.Infrastructure.Migrations
{
    public partial class UpdatePaymentOrderParrent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderParrentId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderParrentId",
                table: "Payments",
                column: "OrderParrentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_OrderParrents_OrderParrentId",
                table: "Payments",
                column: "OrderParrentId",
                principalTable: "OrderParrents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_OrderParrents_OrderParrentId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_OrderParrentId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "OrderParrentId",
                table: "Payments");
        }
    }
}
