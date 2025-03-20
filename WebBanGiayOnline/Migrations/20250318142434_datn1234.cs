using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class datn1234 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("17a4f3cb-6405-4698-81bd-0b6b372466ed"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("8369800e-9770-4a27-bc2b-c4d1bc883c63"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("99cacc33-1117-4f57-90be-38b7913c26f3"));

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("224a2944-21fc-4036-be13-9c7f1b6ba045"), null, new DateTime(2025, 3, 18, 21, 24, 32, 247, DateTimeKind.Local).AddTicks(9681), "Admin", 1 },
                    { new Guid("84d87dac-2912-42f4-9b60-b7c9669c996a"), null, new DateTime(2025, 3, 18, 21, 24, 32, 247, DateTimeKind.Local).AddTicks(9713), "Khách hàng", 1 },
                    { new Guid("d2780ece-596e-4051-b63b-3a4c1c4a6a77"), null, new DateTime(2025, 3, 18, 21, 24, 32, 247, DateTimeKind.Local).AddTicks(9710), "Nhân Viên", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("224a2944-21fc-4036-be13-9c7f1b6ba045"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("84d87dac-2912-42f4-9b60-b7c9669c996a"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("d2780ece-596e-4051-b63b-3a4c1c4a6a77"));

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("17a4f3cb-6405-4698-81bd-0b6b372466ed"), null, new DateTime(2025, 3, 18, 21, 3, 22, 368, DateTimeKind.Local).AddTicks(937), "Khách Hàng", 1 },
                    { new Guid("8369800e-9770-4a27-bc2b-c4d1bc883c63"), null, new DateTime(2025, 3, 18, 21, 3, 22, 368, DateTimeKind.Local).AddTicks(935), "Nhân Viên", 1 },
                    { new Guid("99cacc33-1117-4f57-90be-38b7913c26f3"), null, new DateTime(2025, 3, 18, 21, 3, 22, 368, DateTimeKind.Local).AddTicks(914), "Admin", 1 }
                });
        }
    }
}
