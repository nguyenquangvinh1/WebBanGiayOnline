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
            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("61986f3e-ca0b-47ac-8273-2400e2e76e23"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("9a0cfc85-e35f-4df6-8442-db180f64f360"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("ac017e57-ae7e-431a-a9db-b49c227e3723"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("1a1072ce-83b8-43f7-8d02-3a3985ecf7b1"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("5db64725-2fc6-4c59-a22c-dc64a690ef35"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("eb2fba68-1952-46a6-8a49-08c8024bc06b"));

            migrationBuilder.DropColumn(
                name: "phuongThucthanhtoan",
                table: "hoa_Dons");

            migrationBuilder.AddColumn<int>(
                name: "phuongThucthanhtoan",
                table: "don_Chi_Tiets",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "phuongThucthanhtoan",
                table: "don_Chi_Tiets");

            migrationBuilder.AddColumn<int>(
                name: "phuongThucthanhtoan",
                table: "hoa_Dons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "phuong_Thuc_Thanh_Toans",
                columns: new[] { "ID", "ma", "ten_phuong_thuc" },
                values: new object[,]
                {
                    { new Guid("61986f3e-ca0b-47ac-8273-2400e2e76e23"), "CK", "Chuyển khoản" },
                    { new Guid("9a0cfc85-e35f-4df6-8442-db180f64f360"), "TTM", "Tiền mặt" },
                    { new Guid("ac017e57-ae7e-431a-a9db-b49c227e3723"), "ALL", "Cả 2" }
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("1a1072ce-83b8-43f7-8d02-3a3985ecf7b1"), null, new DateTime(2025, 4, 8, 15, 21, 38, 987, DateTimeKind.Local).AddTicks(4439), "Nhân Viên", 1 },
                    { new Guid("5db64725-2fc6-4c59-a22c-dc64a690ef35"), null, new DateTime(2025, 4, 8, 15, 21, 38, 987, DateTimeKind.Local).AddTicks(4442), "Khách hàng", 1 },
                    { new Guid("eb2fba68-1952-46a6-8a49-08c8024bc06b"), null, new DateTime(2025, 4, 8, 15, 21, 38, 987, DateTimeKind.Local).AddTicks(4411), "Admin", 1 }
                });
        }
    }
}
