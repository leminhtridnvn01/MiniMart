using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMart.Infrastructure.Migrations
{
    public partial class AddOrderparrent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderParrentId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderParrents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalPrice = table.Column<int>(type: "int", nullable: true),
                    PriceDecreases = table.Column<int>(type: "int", nullable: true),
                    DeliveryFee = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<int>(type: "int", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: true),
                    LK_OrderStatus = table.Column<int>(type: "int", nullable: true),
                    LK_PaymentMethod = table.Column<int>(type: "int", nullable: true),
                    LK_OrderType = table.Column<int>(type: "int", nullable: true),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupTimeFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PickupTimeTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderParrents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderParrents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderParrentId",
                table: "Orders",
                column: "OrderParrentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderParrents_UserId",
                table: "OrderParrents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderParrents_OrderParrentId",
                table: "Orders",
                column: "OrderParrentId",
                principalTable: "OrderParrents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderParrents_OrderParrentId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderParrents");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderParrentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderParrentId",
                table: "Orders");
        }
    }
}
