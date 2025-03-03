using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class datn1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("40d4fc7e-5a84-4ab0-b11e-df060c212355"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("49ff9a91-6a8e-4e23-b128-f9386d97ca16"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("5cb790ad-33b5-4e9b-9444-308b173a89ad"));

            migrationBuilder.AddColumn<decimal>(
                name: "gia",
                table: "san_Phams",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("161208c1-bf78-4785-9b4d-b86fc08dcc15"), null, new DateTime(2025, 3, 3, 13, 54, 40, 662, DateTimeKind.Local).AddTicks(909), "Khách Hàng", 1 },
                    { new Guid("203933b1-0e61-4335-a346-e6b5a0deb001"), null, new DateTime(2025, 3, 3, 13, 54, 40, 662, DateTimeKind.Local).AddTicks(906), "Nhân Viên", 1 },
                    { new Guid("d5bc8d78-51b4-47f1-a577-57600ff86b01"), null, new DateTime(2025, 3, 3, 13, 54, 40, 662, DateTimeKind.Local).AddTicks(875), "Admin", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("161208c1-bf78-4785-9b4d-b86fc08dcc15"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("203933b1-0e61-4335-a346-e6b5a0deb001"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("d5bc8d78-51b4-47f1-a577-57600ff86b01"));

            migrationBuilder.DropColumn(
                name: "gia",
                table: "san_Phams");

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("40d4fc7e-5a84-4ab0-b11e-df060c212355"), null, new DateTime(2025, 2, 28, 8, 40, 4, 775, DateTimeKind.Local).AddTicks(3843), "Nhân Viên", 1 },
                    { new Guid("49ff9a91-6a8e-4e23-b128-f9386d97ca16"), null, new DateTime(2025, 2, 28, 8, 40, 4, 775, DateTimeKind.Local).AddTicks(3845), "Khách Hàng", 1 },
                    { new Guid("5cb790ad-33b5-4e9b-9444-308b173a89ad"), null, new DateTime(2025, 2, 28, 8, 40, 4, 775, DateTimeKind.Local).AddTicks(3826), "Admin", 1 }
                });
        }
    }
}
