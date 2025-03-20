using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class datn12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
