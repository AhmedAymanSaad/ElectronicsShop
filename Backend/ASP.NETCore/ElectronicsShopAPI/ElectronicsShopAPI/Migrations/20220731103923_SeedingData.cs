using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronicsShopAPI.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "TV" },
                    { 2, "Laptop" },
                    { 3, "Sound System" }
                });

            migrationBuilder.InsertData(
                table: "DiscountTypes",
                columns: new[] { "DiscountTypeId", "DiscountTypeName" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "2Piece" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DiscountTypes",
                keyColumn: "DiscountTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DiscountTypes",
                keyColumn: "DiscountTypeId",
                keyValue: 2);
        }
    }
}
