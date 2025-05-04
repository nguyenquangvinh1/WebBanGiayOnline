using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class _123da : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "phuongThucthanhtoan",
                table: "don_Chi_Tiets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "phuong_Thuc_Thanh_Toans",
                columns: new[] { "ID", "ma", "ten_phuong_thuc" },
                values: new object[,]
                {
                    { new Guid("0decea13-43ba-4a82-9d1e-33efd9695651"), "ALL", "Cả 2" },
                    { new Guid("23f3afeb-641e-4f83-ae55-5d6d23436a03"), "MoMo", "Thanh toán MoMo" },
                    { new Guid("6131b256-e560-4660-bce4-5d595687ff19"), "TTM", "Tiền mặt" },
                    { new Guid("6a4babc6-644c-4081-807d-521d584f6fd6"), "CK", "Chuyển khoản" },
                    { new Guid("9589e04a-0257-4d81-9a1b-68727cfb0f55"), "VNPAY", "Thanh toán VNPAY" },
                    { new Guid("a00d3cb1-8dfd-49ab-a9c3-44d9318ae40e"), "TTNH", "Thanh toán khi nhận hàng" }
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("5c0046f1-ce3a-40c9-acd8-9dc3f2af16f2"), null, new DateTime(2025, 4, 24, 8, 50, 29, 632, DateTimeKind.Local).AddTicks(2371), "Admin", 1 },
                    { new Guid("8f3291e3-54af-42ff-9629-eafe4ae51676"), null, new DateTime(2025, 4, 24, 8, 50, 29, 632, DateTimeKind.Local).AddTicks(2391), "Khách hàng", 1 },
                    { new Guid("bbfea0ba-e17d-4cf1-b0ee-301833c5c206"), null, new DateTime(2025, 4, 24, 8, 50, 29, 632, DateTimeKind.Local).AddTicks(2389), "Nhân Viên", 1 }
                });

            migrationBuilder.InsertData(
                table: "tai_Khoans",
                columns: new[] { "ID", "ResetToken", "TokenExpiry", "Vai_TroID", "cccd", "email", "gioi_tinh", "hinh_anh", "ho_ten", "ma", "ngay_sinh", "ngay_sua", "ngay_tao", "pass_word", "sdt", "trang_thai", "user_name" },
                values: new object[] { new Guid("b4d700d8-7608-4b50-bf27-c2d2efac7b5c"), null, null, new Guid("5c0046f1-ce3a-40c9-acd8-9dc3f2af16f2"), "123456789012", "admin_tong@fpt.edu.vn", 1, "admin_tong.png", "Admin Tổng", "ADMIN01", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 4, 24, 8, 50, 29, 632, DateTimeKind.Local).AddTicks(2608), "Admin123", "0123456789", 1, "admin_tong" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("0decea13-43ba-4a82-9d1e-33efd9695651"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("23f3afeb-641e-4f83-ae55-5d6d23436a03"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("6131b256-e560-4660-bce4-5d595687ff19"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("6a4babc6-644c-4081-807d-521d584f6fd6"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("9589e04a-0257-4d81-9a1b-68727cfb0f55"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("a00d3cb1-8dfd-49ab-a9c3-44d9318ae40e"));

            migrationBuilder.DeleteData(
                table: "tai_Khoans",
                keyColumn: "ID",
                keyValue: new Guid("b4d700d8-7608-4b50-bf27-c2d2efac7b5c"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("8f3291e3-54af-42ff-9629-eafe4ae51676"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("bbfea0ba-e17d-4cf1-b0ee-301833c5c206"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("5c0046f1-ce3a-40c9-acd8-9dc3f2af16f2"));

            migrationBuilder.AlterColumn<int>(
                name: "phuongThucthanhtoan",
                table: "don_Chi_Tiets",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
