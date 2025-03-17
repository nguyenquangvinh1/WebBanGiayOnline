using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class datnvinh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "nguoi_tao",
                table: "hoa_Dons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("6a2e0397-5087-42f5-94f2-5163f692eac9"), null, new DateTime(2025, 3, 12, 15, 47, 28, 551, DateTimeKind.Local).AddTicks(9640), "Khách Hàng", 1 },
                    { new Guid("8efb16f3-4253-4aee-a92b-030fc19f0260"), null, new DateTime(2025, 3, 12, 15, 47, 28, 551, DateTimeKind.Local).AddTicks(9627), "Nhân Viên", 1 },
                    { new Guid("ee8b6888-6fbf-4d86-b477-658490b446d8"), null, new DateTime(2025, 3, 12, 15, 47, 28, 551, DateTimeKind.Local).AddTicks(9609), "Admin", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("6a2e0397-5087-42f5-94f2-5163f692eac9"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("8efb16f3-4253-4aee-a92b-030fc19f0260"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("ee8b6888-6fbf-4d86-b477-658490b446d8"));

            migrationBuilder.AlterColumn<string>(
                name: "nguoi_tao",
                table: "hoa_Dons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
