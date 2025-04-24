using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class dda12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("2d2418e6-6a62-4286-99dc-d5df69ea97b2"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("5dbd7772-25ee-4e7e-b6b3-1d83f83c491b"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("d79d6220-7661-4352-a4f4-a307e105a933"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("d91ed7b5-6216-4c51-b40a-564d86a511d2"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("f3bbacfe-d2b7-409d-8342-1520c1838d8c"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("f96c6d30-e33d-40d8-9135-e105b7e88bc6"));

            migrationBuilder.DeleteData(
                table: "tai_Khoans",
                keyColumn: "ID",
                keyValue: new Guid("ba8c7177-1d3f-4c96-b63e-8a0d50043047"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("4f653032-94a2-42df-8e8e-5e043cb8edbc"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("e2d99324-c43f-4c34-885f-fe65d4cf03e2"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("8fb9f3e3-ae40-4008-9efd-6c52f9de4cea"));

            migrationBuilder.DropColumn(
                name: "Status",
                table: "hoa_Dons");

            migrationBuilder.DropColumn(
                name: "discount",
                table: "hoa_Dons");

            migrationBuilder.DropColumn(
                name: "discount",
                table: "don_Chi_Tiets");

            migrationBuilder.InsertData(
                table: "phuong_Thuc_Thanh_Toans",
                columns: new[] { "ID", "ma", "ten_phuong_thuc" },
                values: new object[,]
                {
                    { new Guid("441b95ff-f542-40f5-afe6-c721eeaef0f2"), "CK", "Chuyển khoản" },
                    { new Guid("7c3aef27-ea71-4e64-a72b-1df815680eb4"), "TTNH", "Thanh toán khi nhận hàng" },
                    { new Guid("7f90be50-54da-4f9f-b6a1-442a095fea69"), "VNPAY", "Thanh toán VNPAY" },
                    { new Guid("80dcccc8-d392-4805-a4f7-831a7f02529d"), "MoMo", "Thanh toán MoMo" },
                    { new Guid("c8c26b54-1014-47be-9ea1-4eace195dc80"), "ALL", "Cả 2" },
                    { new Guid("f2ce82c6-3373-4ede-9746-4da4f9144e08"), "TTM", "Tiền mặt" }
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("351cd45d-84cc-44f9-b545-d7f31b23acb7"), null, new DateTime(2025, 4, 24, 11, 16, 30, 354, DateTimeKind.Local).AddTicks(6373), "Admin", 1 },
                    { new Guid("43d07c60-aa10-4d07-b804-35b798da30ce"), null, new DateTime(2025, 4, 24, 11, 16, 30, 354, DateTimeKind.Local).AddTicks(6402), "Khách hàng", 1 },
                    { new Guid("a918e042-2def-4821-a90a-a33eef7f1c9b"), null, new DateTime(2025, 4, 24, 11, 16, 30, 354, DateTimeKind.Local).AddTicks(6400), "Nhân Viên", 1 }
                });

            migrationBuilder.InsertData(
                table: "tai_Khoans",
                columns: new[] { "ID", "ResetToken", "TokenExpiry", "Vai_TroID", "cccd", "email", "gioi_tinh", "hinh_anh", "ho_ten", "ma", "ngay_sinh", "ngay_sua", "ngay_tao", "pass_word", "sdt", "trang_thai", "user_name" },
                values: new object[] { new Guid("4a677bc7-7a89-40af-9ebf-d728f4aa66cc"), null, null, new Guid("351cd45d-84cc-44f9-b545-d7f31b23acb7"), "123456789012", "admin_tong@fpt.edu.vn", 1, "admin_tong.png", "Admin Tổng", "ADMIN01", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 4, 24, 11, 16, 30, 354, DateTimeKind.Local).AddTicks(6590), "Admin123", "0123456789", 1, "admin_tong" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("441b95ff-f542-40f5-afe6-c721eeaef0f2"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("7c3aef27-ea71-4e64-a72b-1df815680eb4"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("7f90be50-54da-4f9f-b6a1-442a095fea69"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("80dcccc8-d392-4805-a4f7-831a7f02529d"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("c8c26b54-1014-47be-9ea1-4eace195dc80"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("f2ce82c6-3373-4ede-9746-4da4f9144e08"));

            migrationBuilder.DeleteData(
                table: "tai_Khoans",
                keyColumn: "ID",
                keyValue: new Guid("4a677bc7-7a89-40af-9ebf-d728f4aa66cc"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("43d07c60-aa10-4d07-b804-35b798da30ce"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("a918e042-2def-4821-a90a-a33eef7f1c9b"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("351cd45d-84cc-44f9-b545-d7f31b23acb7"));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "hoa_Dons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "discount",
                table: "hoa_Dons",
                type: "nvarchar(max)",
                nullable: true);

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
                    { new Guid("2d2418e6-6a62-4286-99dc-d5df69ea97b2"), "TTNH", "Thanh toán khi nhận hàng" },
                    { new Guid("5dbd7772-25ee-4e7e-b6b3-1d83f83c491b"), "ALL", "Cả 2" },
                    { new Guid("d79d6220-7661-4352-a4f4-a307e105a933"), "MoMo", "Thanh toán MoMo" },
                    { new Guid("d91ed7b5-6216-4c51-b40a-564d86a511d2"), "TTM", "Tiền mặt" },
                    { new Guid("f3bbacfe-d2b7-409d-8342-1520c1838d8c"), "CK", "Chuyển khoản" },
                    { new Guid("f96c6d30-e33d-40d8-9135-e105b7e88bc6"), "VNPAY", "Thanh toán VNPAY" }
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("4f653032-94a2-42df-8e8e-5e043cb8edbc"), null, new DateTime(2025, 4, 24, 10, 6, 11, 223, DateTimeKind.Local).AddTicks(9168), "Khách hàng", 1 },
                    { new Guid("8fb9f3e3-ae40-4008-9efd-6c52f9de4cea"), null, new DateTime(2025, 4, 24, 10, 6, 11, 223, DateTimeKind.Local).AddTicks(9134), "Admin", 1 },
                    { new Guid("e2d99324-c43f-4c34-885f-fe65d4cf03e2"), null, new DateTime(2025, 4, 24, 10, 6, 11, 223, DateTimeKind.Local).AddTicks(9155), "Nhân Viên", 1 }
                });

            migrationBuilder.InsertData(
                table: "tai_Khoans",
                columns: new[] { "ID", "ResetToken", "TokenExpiry", "Vai_TroID", "cccd", "email", "gioi_tinh", "hinh_anh", "ho_ten", "ma", "ngay_sinh", "ngay_sua", "ngay_tao", "pass_word", "sdt", "trang_thai", "user_name" },
                values: new object[] { new Guid("ba8c7177-1d3f-4c96-b63e-8a0d50043047"), null, null, new Guid("8fb9f3e3-ae40-4008-9efd-6c52f9de4cea"), "123456789012", "admin_tong@fpt.edu.vn", 1, "admin_tong.png", "Admin Tổng", "ADMIN01", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 4, 24, 10, 6, 11, 223, DateTimeKind.Local).AddTicks(9371), "Admin123", "0123456789", 1, "admin_tong" });
        }
    }
}
