using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMart.Infrastructure.Migrations
{
    public partial class UpdateWardId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Citys",
                type: "int",
                nullable: false);
            migrationBuilder.AddPrimaryKey(
                name: "PK_Citys",
                table: "Citys",
                column: "Id");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Districts",
                type: "int",
                nullable: false);
            migrationBuilder.AddPrimaryKey(
                name: "PK_Districts",
                table: "Districts",
                column: "Id");
            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Citys_CityId",
                table: "Districts",
                column: "CityId",
                principalTable: "Citys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Wards",
                type: "int",
                nullable: false);
            migrationBuilder.AddPrimaryKey(
                name: "PK_Wards",
                table: "Wards",
                column: "Id");
            migrationBuilder.AddForeignKey(
                name: "FK_Wards_Districts_DistrictId",
                table: "Wards",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Citys",
                table: "Citys");
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Citys");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Citys_CityId",
                table: "Districts");
            migrationBuilder.DropPrimaryKey(
                name: "PK_Districts",
                table: "Districts");
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Wards_Districts_DistrictId",
                table: "Wards");
            migrationBuilder.DropPrimaryKey(
                name: "PK_Wards",
                table: "Wards");
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Wards");
        }
    }
}
