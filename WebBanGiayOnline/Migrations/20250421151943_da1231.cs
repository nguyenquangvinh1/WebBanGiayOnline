using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class da1231 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("07325ef2-9493-44b6-ad16-38adb09d8477"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("8a203865-4da8-4731-a2b9-fb7b7929e11b"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("da30ac75-7739-4f24-8f2f-1bb38bb90943"));

            migrationBuilder.DeleteData(
                table: "tai_Khoans",
                keyColumn: "ID",
                keyValue: new Guid("6f5918c2-d6c2-475d-9bfd-1388afba1e6a"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("1eb8bbe3-e7a8-470e-bcab-a4036c36582c"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("5f0cece5-18dc-45a1-95dc-baa0f773f004"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("58f42d06-27cd-4232-9f51-b01ffe2389df"));

            migrationBuilder.AddColumn<string>(
                name: "ma",
                table: "san_Pham_Chi_Tiets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "phuongThucthanhtoan",
                table: "don_Chi_Tiets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "phuong_Thuc_Thanh_Toans",
                columns: new[] { "ID", "ma", "ten_phuong_thuc" },
                values: new object[,]
                {
                    { new Guid("1dab682b-fc29-45b2-b84e-48300b891e21"), "CK", "Chuyển khoản" },
                    { new Guid("bc744377-604c-42f8-8bcc-84e60c787629"), "ALL", "Cả 2" },
                    { new Guid("de2e664e-b705-4fdc-b6fb-f34bd6b8c3c6"), "TTM", "Tiền mặt" }
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("2d89a17a-4708-4164-8cfc-482f0772f11b"), null, new DateTime(2025, 4, 21, 22, 19, 39, 717, DateTimeKind.Local).AddTicks(4732), "Nhân Viên", 1 },
                    { new Guid("b6a281c2-9af8-4ae8-8dda-924c0a737d94"), null, new DateTime(2025, 4, 21, 22, 19, 39, 717, DateTimeKind.Local).AddTicks(4734), "Khách hàng", 1 },
                    { new Guid("cdbda5c6-a9be-4ce1-957b-1e8a46455831"), null, new DateTime(2025, 4, 21, 22, 19, 39, 717, DateTimeKind.Local).AddTicks(4711), "Admin", 1 }
                });

            migrationBuilder.InsertData(
                table: "tai_Khoans",
                columns: new[] { "ID", "ResetToken", "TokenExpiry", "Vai_TroID", "cccd", "email", "gioi_tinh", "hinh_anh", "ho_ten", "ma", "ngay_sinh", "ngay_sua", "ngay_tao", "pass_word", "sdt", "trang_thai", "user_name" },
                values: new object[] { new Guid("86f18d3b-d828-4930-be7b-24b3cc1c55db"), null, null, new Guid("cdbda5c6-a9be-4ce1-957b-1e8a46455831"), "123456789012", "admin_tong@fpt.edu.vn", 1, "admin_tong.png", "Admin Tổng", "ADMIN01", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 4, 21, 22, 19, 39, 717, DateTimeKind.Local).AddTicks(4961), "Admin123", "0123456789", 1, "admin_tong" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("1dab682b-fc29-45b2-b84e-48300b891e21"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("bc744377-604c-42f8-8bcc-84e60c787629"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("de2e664e-b705-4fdc-b6fb-f34bd6b8c3c6"));

            migrationBuilder.DeleteData(
                table: "tai_Khoans",
                keyColumn: "ID",
                keyValue: new Guid("86f18d3b-d828-4930-be7b-24b3cc1c55db"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("2d89a17a-4708-4164-8cfc-482f0772f11b"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("b6a281c2-9af8-4ae8-8dda-924c0a737d94"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("cdbda5c6-a9be-4ce1-957b-1e8a46455831"));

            migrationBuilder.DropColumn(
                name: "ma",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.AlterColumn<int>(
                name: "phuongThucthanhtoan",
                table: "don_Chi_Tiets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "phuong_Thuc_Thanh_Toans",
                columns: new[] { "ID", "ma", "ten_phuong_thuc" },
                values: new object[,]
                {
                    { new Guid("07325ef2-9493-44b6-ad16-38adb09d8477"), "CK", "Chuyển khoản" },
                    { new Guid("8a203865-4da8-4731-a2b9-fb7b7929e11b"), "ALL", "Cả 2" },
                    { new Guid("da30ac75-7739-4f24-8f2f-1bb38bb90943"), "TTM", "Tiền mặt" }
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("1eb8bbe3-e7a8-470e-bcab-a4036c36582c"), null, new DateTime(2025, 4, 17, 10, 18, 42, 467, DateTimeKind.Local).AddTicks(4713), "Khách hàng", 1 },
                    { new Guid("58f42d06-27cd-4232-9f51-b01ffe2389df"), null, new DateTime(2025, 4, 17, 10, 18, 42, 467, DateTimeKind.Local).AddTicks(4684), "Admin", 1 },
                    { new Guid("5f0cece5-18dc-45a1-95dc-baa0f773f004"), null, new DateTime(2025, 4, 17, 10, 18, 42, 467, DateTimeKind.Local).AddTicks(4710), "Nhân Viên", 1 }
                });

            migrationBuilder.InsertData(
                table: "tai_Khoans",
                columns: new[] { "ID", "ResetToken", "TokenExpiry", "Vai_TroID", "cccd", "email", "gioi_tinh", "hinh_anh", "ho_ten", "ma", "ngay_sinh", "ngay_sua", "ngay_tao", "pass_word", "sdt", "trang_thai", "user_name" },
                values: new object[] { new Guid("6f5918c2-d6c2-475d-9bfd-1388afba1e6a"), null, null, new Guid("58f42d06-27cd-4232-9f51-b01ffe2389df"), "123456789012", "admin_tong@fpt.edu.vn", 1, "admin_tong.png", "Admin Tổng", "ADMIN01", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 4, 17, 10, 18, 42, 467, DateTimeKind.Local).AddTicks(4999), "Admin123", "0123456789", 1, "admin_tong" });
        }
    }
}
