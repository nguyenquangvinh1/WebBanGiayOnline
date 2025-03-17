using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class datn32 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("232184e2-2983-4d1c-a945-49bc78e5ec44"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("417a8cb2-b755-411c-b912-478123bacbe0"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("e8f5c604-46d3-42bf-839b-70fc7f42787d"));

            migrationBuilder.AlterColumn<double>(
                name: "TongTien",
                table: "thongKes",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "gia",
                table: "san_Pham_Chi_Tiets",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "tong_tien",
                table: "hoa_Dons",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "gia",
                table: "don_Chi_Tiets",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("0f717ecd-cbb9-431e-b011-6fd2554041c1"), null, new DateTime(2025, 3, 14, 16, 57, 22, 69, DateTimeKind.Local).AddTicks(4801), "Admin", 1 },
                    { new Guid("14a92e9d-3bf5-49aa-8e40-52ab059ef6ad"), null, new DateTime(2025, 3, 14, 16, 57, 22, 69, DateTimeKind.Local).AddTicks(4832), "Nhân Viên", 1 },
                    { new Guid("4ae9a9a8-ccda-4a95-af27-b88aded8e303"), null, new DateTime(2025, 3, 14, 16, 57, 22, 69, DateTimeKind.Local).AddTicks(4835), "Khách Hàng", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("0f717ecd-cbb9-431e-b011-6fd2554041c1"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("14a92e9d-3bf5-49aa-8e40-52ab059ef6ad"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("4ae9a9a8-ccda-4a95-af27-b88aded8e303"));

            migrationBuilder.AlterColumn<decimal>(
                name: "TongTien",
                table: "thongKes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "gia",
                table: "san_Pham_Chi_Tiets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "tong_tien",
                table: "hoa_Dons",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "gia",
                table: "don_Chi_Tiets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("232184e2-2983-4d1c-a945-49bc78e5ec44"), null, new DateTime(2025, 3, 12, 16, 39, 19, 68, DateTimeKind.Local).AddTicks(5234), "Nhân Viên", 1 },
                    { new Guid("417a8cb2-b755-411c-b912-478123bacbe0"), null, new DateTime(2025, 3, 12, 16, 39, 19, 68, DateTimeKind.Local).AddTicks(5214), "Admin", 1 },
                    { new Guid("e8f5c604-46d3-42bf-839b-70fc7f42787d"), null, new DateTime(2025, 3, 12, 16, 39, 19, 68, DateTimeKind.Local).AddTicks(5248), "Khách Hàng", 1 }
                });
        }
    }
}
