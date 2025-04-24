using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class dda1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_thanh_Toans_hoa_Dons_Hoa_DonID",
                table: "thanh_Toans");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "Hoa_DonID",
                table: "thanh_Toans",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AddForeignKey(
                name: "FK_thanh_Toans_hoa_Dons_Hoa_DonID",
                table: "thanh_Toans",
                column: "Hoa_DonID",
                principalTable: "hoa_Dons",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_thanh_Toans_hoa_Dons_Hoa_DonID",
                table: "thanh_Toans");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "Hoa_DonID",
                table: "thanh_Toans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_thanh_Toans_hoa_Dons_Hoa_DonID",
                table: "thanh_Toans",
                column: "Hoa_DonID",
                principalTable: "hoa_Dons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
