using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class d27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("b9fc7a66-a407-4e3e-baf4-ff4c2a490951"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("e25880ae-fce0-4955-875e-781f17d147e2"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("e686e410-925c-4c87-acc3-bc9732adac6b"));

            migrationBuilder.DeleteData(
                table: "tai_Khoans",
                keyColumn: "ID",
                keyValue: new Guid("f9765ece-a7de-4f59-a1ad-910b9ca7fde7"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("9c206d8b-54d7-46cd-b21d-c713cb8a6d4a"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("bbe189d8-ec50-4da5-ad96-f4946ac9dd81"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("a318b59e-8235-49bd-9617-45ddc02ec9b5"));

            migrationBuilder.AddColumn<string>(
                name: "discount",
                table: "don_Chi_Tiets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "discount",
                table: "don_Chi_Tiets");

            migrationBuilder.InsertData(
                table: "phuong_Thuc_Thanh_Toans",
                columns: new[] { "ID", "ma", "ten_phuong_thuc" },
                values: new object[,]
                {
                    { new Guid("b9fc7a66-a407-4e3e-baf4-ff4c2a490951"), "TTM", "Tiền mặt" },
                    { new Guid("e25880ae-fce0-4955-875e-781f17d147e2"), "CK", "Chuyển khoản" },
                    { new Guid("e686e410-925c-4c87-acc3-bc9732adac6b"), "ALL", "Cả 2" }
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("9c206d8b-54d7-46cd-b21d-c713cb8a6d4a"), null, new DateTime(2025, 4, 17, 8, 59, 47, 54, DateTimeKind.Local).AddTicks(1274), "Nhân Viên", 1 },
                    { new Guid("a318b59e-8235-49bd-9617-45ddc02ec9b5"), null, new DateTime(2025, 4, 17, 8, 59, 47, 54, DateTimeKind.Local).AddTicks(1253), "Admin", 1 },
                    { new Guid("bbe189d8-ec50-4da5-ad96-f4946ac9dd81"), null, new DateTime(2025, 4, 17, 8, 59, 47, 54, DateTimeKind.Local).AddTicks(1287), "Khách hàng", 1 }
                });

            migrationBuilder.InsertData(
                table: "tai_Khoans",
                columns: new[] { "ID", "ResetToken", "TokenExpiry", "Vai_TroID", "cccd", "email", "gioi_tinh", "hinh_anh", "ho_ten", "ma", "ngay_sinh", "ngay_sua", "ngay_tao", "pass_word", "sdt", "trang_thai", "user_name" },
                values: new object[] { new Guid("f9765ece-a7de-4f59-a1ad-910b9ca7fde7"), null, null, new Guid("a318b59e-8235-49bd-9617-45ddc02ec9b5"), "123456789012", "admin_tong@fpt.edu.vn", 1, "admin_tong.png", "Admin Tổng", "ADMIN01", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 4, 17, 8, 59, 47, 54, DateTimeKind.Local).AddTicks(1536), "Admin123", "0123456789", 1, "admin_tong" });
        }
    }
}
