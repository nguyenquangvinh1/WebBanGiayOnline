using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class da : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("0d671287-f0a2-487e-8ac2-f833bdf70bf0"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("611e1538-ed29-4a22-b144-529766f382d7"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("e9c7bd8b-1944-43a9-ba8b-c6880c566918"));

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("517dd722-274f-4f1a-b8c7-83665e4c9e74"), null, new DateTime(2025, 3, 20, 10, 46, 54, 573, DateTimeKind.Local).AddTicks(3013), "Nhân Viên", 1 },
                    { new Guid("6261af9e-3bc0-4317-8a2d-6c523e5a5cf1"), null, new DateTime(2025, 3, 20, 10, 46, 54, 573, DateTimeKind.Local).AddTicks(2771), "Admin", 1 },
                    { new Guid("651b967b-5292-4eff-9cf7-1fc329ee5890"), null, new DateTime(2025, 3, 20, 10, 46, 54, 573, DateTimeKind.Local).AddTicks(3016), "Khách hàng", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("517dd722-274f-4f1a-b8c7-83665e4c9e74"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("6261af9e-3bc0-4317-8a2d-6c523e5a5cf1"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("651b967b-5292-4eff-9cf7-1fc329ee5890"));

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("0d671287-f0a2-487e-8ac2-f833bdf70bf0"), null, new DateTime(2025, 3, 19, 15, 57, 3, 824, DateTimeKind.Local).AddTicks(6744), "Khách hàng", 1 },
                    { new Guid("611e1538-ed29-4a22-b144-529766f382d7"), null, new DateTime(2025, 3, 19, 15, 57, 3, 824, DateTimeKind.Local).AddTicks(6738), "Nhân Viên", 1 },
                    { new Guid("e9c7bd8b-1944-43a9-ba8b-c6880c566918"), null, new DateTime(2025, 3, 19, 15, 57, 3, 824, DateTimeKind.Local).AddTicks(6709), "Admin", 1 }
                });
        }
    }
}
