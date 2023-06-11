using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMart.Infrastructure.Migrations
{
    public partial class AddPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    TranCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
