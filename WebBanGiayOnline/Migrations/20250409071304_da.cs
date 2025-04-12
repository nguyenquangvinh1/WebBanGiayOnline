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
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("5739ff21-b69f-4c6f-b0f3-18891e48b7d7"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("5a9cc7fc-cea9-46bb-9b7a-2e043b5ddfd9"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("c991114e-faf8-4a4d-b890-28e8513a1c81"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("579d6ab5-91d9-423e-826f-49e939726d13"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("739e35d4-f486-4260-85bc-49dc704bce94"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("b4c53fb4-c740-4931-8441-ee12670e9665"));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "hoa_Dons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "phuong_Thuc_Thanh_Toans",
                columns: new[] { "ID", "ma", "ten_phuong_thuc" },
                values: new object[,]
                {
                    { new Guid("35f0daba-ba35-4881-a7c0-5c2d86e0b837"), "CK", "Chuyển khoản" },
                    { new Guid("b9f1e958-6111-4370-972d-f6a9620c4897"), "ALL", "Cả 2" },
                    { new Guid("e337de82-9296-49da-8810-db16557e4d59"), "TTM", "Tiền mặt" }
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("0c75ec3c-11e9-4d70-86b5-69c5fd6b448d"), null, new DateTime(2025, 4, 9, 14, 13, 0, 642, DateTimeKind.Local).AddTicks(2703), "Admin", 1 },
                    { new Guid("bf710196-3cc0-46b5-b9c6-ff121b2fd50c"), null, new DateTime(2025, 4, 9, 14, 13, 0, 642, DateTimeKind.Local).AddTicks(2732), "Khách hàng", 1 },
                    { new Guid("cfb90941-c6bf-4cb1-a27c-202b8acbf5de"), null, new DateTime(2025, 4, 9, 14, 13, 0, 642, DateTimeKind.Local).AddTicks(2730), "Nhân Viên", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("35f0daba-ba35-4881-a7c0-5c2d86e0b837"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("b9f1e958-6111-4370-972d-f6a9620c4897"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("e337de82-9296-49da-8810-db16557e4d59"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("0c75ec3c-11e9-4d70-86b5-69c5fd6b448d"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("bf710196-3cc0-46b5-b9c6-ff121b2fd50c"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("cfb90941-c6bf-4cb1-a27c-202b8acbf5de"));

            migrationBuilder.DropColumn(
                name: "Status",
                table: "hoa_Dons");

            migrationBuilder.InsertData(
                table: "phuong_Thuc_Thanh_Toans",
                columns: new[] { "ID", "ma", "ten_phuong_thuc" },
                values: new object[,]
                {
                    { new Guid("5739ff21-b69f-4c6f-b0f3-18891e48b7d7"), "ALL", "Cả 2" },
                    { new Guid("5a9cc7fc-cea9-46bb-9b7a-2e043b5ddfd9"), "TTM", "Tiền mặt" },
                    { new Guid("c991114e-faf8-4a4d-b890-28e8513a1c81"), "CK", "Chuyển khoản" }
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("579d6ab5-91d9-423e-826f-49e939726d13"), null, new DateTime(2025, 4, 8, 15, 39, 57, 340, DateTimeKind.Local).AddTicks(2977), "Khách hàng", 1 },
                    { new Guid("739e35d4-f486-4260-85bc-49dc704bce94"), null, new DateTime(2025, 4, 8, 15, 39, 57, 340, DateTimeKind.Local).AddTicks(2936), "Admin", 1 },
                    { new Guid("b4c53fb4-c740-4931-8441-ee12670e9665"), null, new DateTime(2025, 4, 8, 15, 39, 57, 340, DateTimeKind.Local).AddTicks(2974), "Nhân Viên", 1 }
                });
        }
    }
}
