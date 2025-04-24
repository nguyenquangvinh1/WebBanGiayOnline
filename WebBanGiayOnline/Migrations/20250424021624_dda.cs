using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class dda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "Phuong_Thuc_Thanh_ToanID",
                table: "don_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "phuong_Thuc_Thanh_Toans",
                columns: new[] { "ID", "ma", "ten_phuong_thuc" },
                values: new object[,]
                {
                    { new Guid("00c1ae6c-b6b5-446a-b746-848454320636"), "VNPAY", "Thanh toán VNPAY" },
                    { new Guid("12ad78d3-e94a-4660-b485-d54caed08831"), "TTNH", "Thanh toán khi nhận hàng" },
                    { new Guid("36f291b7-4f85-4c24-85f3-d1a447cb9c64"), "ALL", "Cả 2" },
                    { new Guid("aecf8f9a-b9db-4b30-81f8-0de83a25e721"), "MoMo", "Thanh toán MoMo" },
                    { new Guid("c84e9176-2220-46bf-b4ea-9ca14392352f"), "CK", "Chuyển khoản" },
                    { new Guid("ee6bf25d-0c32-4028-8b0c-dd11744fe62a"), "TTM", "Tiền mặt" }
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("03930eac-2202-46f2-9a10-4e59c6703b83"), null, new DateTime(2025, 4, 24, 9, 16, 22, 625, DateTimeKind.Local).AddTicks(9386), "Admin", 1 },
                    { new Guid("8d30c078-678f-4974-b983-b8d85d9806a4"), null, new DateTime(2025, 4, 24, 9, 16, 22, 625, DateTimeKind.Local).AddTicks(9417), "Khách hàng", 1 },
                    { new Guid("d7d29a63-0b47-4932-95b3-18e548c8402e"), null, new DateTime(2025, 4, 24, 9, 16, 22, 625, DateTimeKind.Local).AddTicks(9414), "Nhân Viên", 1 }
                });

            migrationBuilder.InsertData(
                table: "tai_Khoans",
                columns: new[] { "ID", "ResetToken", "TokenExpiry", "Vai_TroID", "cccd", "email", "gioi_tinh", "hinh_anh", "ho_ten", "ma", "ngay_sinh", "ngay_sua", "ngay_tao", "pass_word", "sdt", "trang_thai", "user_name" },
                values: new object[] { new Guid("50d13286-b2bc-4f8d-be75-28b527d112a4"), null, null, new Guid("03930eac-2202-46f2-9a10-4e59c6703b83"), "123456789012", "admin_tong@fpt.edu.vn", 1, "admin_tong.png", "Admin Tổng", "ADMIN01", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 4, 24, 9, 16, 22, 625, DateTimeKind.Local).AddTicks(9781), "Admin123", "0123456789", 1, "admin_tong" });

            migrationBuilder.CreateIndex(
                name: "IX_don_Chi_Tiets_Phuong_Thuc_Thanh_ToanID",
                table: "don_Chi_Tiets",
                column: "Phuong_Thuc_Thanh_ToanID");

            migrationBuilder.AddForeignKey(
                name: "FK_don_Chi_Tiets_phuong_Thuc_Thanh_Toans_Phuong_Thuc_Thanh_ToanID",
                table: "don_Chi_Tiets",
                column: "Phuong_Thuc_Thanh_ToanID",
                principalTable: "phuong_Thuc_Thanh_Toans",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_don_Chi_Tiets_phuong_Thuc_Thanh_Toans_Phuong_Thuc_Thanh_ToanID",
                table: "don_Chi_Tiets");

            migrationBuilder.DropIndex(
                name: "IX_don_Chi_Tiets_Phuong_Thuc_Thanh_ToanID",
                table: "don_Chi_Tiets");

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("00c1ae6c-b6b5-446a-b746-848454320636"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("12ad78d3-e94a-4660-b485-d54caed08831"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("36f291b7-4f85-4c24-85f3-d1a447cb9c64"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("aecf8f9a-b9db-4b30-81f8-0de83a25e721"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("c84e9176-2220-46bf-b4ea-9ca14392352f"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("ee6bf25d-0c32-4028-8b0c-dd11744fe62a"));

            migrationBuilder.DeleteData(
                table: "tai_Khoans",
                keyColumn: "ID",
                keyValue: new Guid("50d13286-b2bc-4f8d-be75-28b527d112a4"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("8d30c078-678f-4974-b983-b8d85d9806a4"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("d7d29a63-0b47-4932-95b3-18e548c8402e"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("03930eac-2202-46f2-9a10-4e59c6703b83"));

            migrationBuilder.DropColumn(
                name: "Phuong_Thuc_Thanh_ToanID",
                table: "don_Chi_Tiets");

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
    }
}
