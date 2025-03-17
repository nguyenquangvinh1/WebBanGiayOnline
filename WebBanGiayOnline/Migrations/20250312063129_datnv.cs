using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class datnv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("13a5fd0b-c459-4ecd-b82e-633f58343b7c"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("17ca9c07-9730-407e-a7bc-bdb98eb37d5c"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("a84fb1a3-e2b0-4d75-a5ad-442bb1a11463"));

            migrationBuilder.DropColumn(
                name: "SoLuongSP",
                table: "hoa_Dons");

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("3635fe21-d196-4941-a9cb-5224c25426c1"), null, new DateTime(2025, 3, 12, 13, 31, 25, 6, DateTimeKind.Local).AddTicks(5356), "Khách Hàng", 1 },
                    { new Guid("4a98188f-69eb-4b93-9118-4752aa544e35"), null, new DateTime(2025, 3, 12, 13, 31, 25, 6, DateTimeKind.Local).AddTicks(5318), "Admin", 1 },
                    { new Guid("9341619e-4091-4b06-bf4f-c860c18d7dd3"), null, new DateTime(2025, 3, 12, 13, 31, 25, 6, DateTimeKind.Local).AddTicks(5354), "Nhân Viên", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("3635fe21-d196-4941-a9cb-5224c25426c1"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("4a98188f-69eb-4b93-9118-4752aa544e35"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("9341619e-4091-4b06-bf4f-c860c18d7dd3"));

            migrationBuilder.AddColumn<int>(
                name: "SoLuongSP",
                table: "hoa_Dons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("13a5fd0b-c459-4ecd-b82e-633f58343b7c"), null, new DateTime(2025, 3, 11, 10, 40, 39, 811, DateTimeKind.Local).AddTicks(1595), "Khách Hàng", 1 },
                    { new Guid("17ca9c07-9730-407e-a7bc-bdb98eb37d5c"), null, new DateTime(2025, 3, 11, 10, 40, 39, 811, DateTimeKind.Local).AddTicks(1593), "Nhân Viên", 1 },
                    { new Guid("a84fb1a3-e2b0-4d75-a5ad-442bb1a11463"), null, new DateTime(2025, 3, 11, 10, 40, 39, 811, DateTimeKind.Local).AddTicks(1544), "Admin", 1 }
                });
        }
    }
}
