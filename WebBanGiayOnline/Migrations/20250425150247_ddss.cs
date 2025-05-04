using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class ddss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ghi_chu",
                table: "tai_Khoan_Hoa_Dons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "thao_tac",
                table: "tai_Khoan_Hoa_Dons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "phuong_Thuc_Thanh_Toans",
                columns: new[] { "ID", "ma", "ten_phuong_thuc" },
                values: new object[,]
                {
                    { new Guid("32b17115-037e-4893-81fd-e9f5f07b18f6"), "CK", "Chuyển khoản" },
                    { new Guid("5b24f60d-3c23-467b-8f37-b186b30bc4f4"), "ALL", "Cả 2" },
                    { new Guid("7a1828c2-e64c-4eca-b632-d210c5352f33"), "TTNH", "Thanh toán khi nhận hàng" },
                    { new Guid("b34a2747-2008-4f5f-a2a3-7220f3a656f6"), "VNPAY", "Thanh toán VNPAY" },
                    { new Guid("c4d5897d-dc1c-4fa1-be28-ddeaf5985f04"), "MoMo", "Thanh toán MoMo" },
                    { new Guid("d04a3590-610e-4686-9e6e-cf0c2770b148"), "TTM", "Tiền mặt" }
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("28997333-8f12-4523-85f2-2727ac0eaa04"), null, new DateTime(2025, 4, 25, 22, 2, 43, 273, DateTimeKind.Local).AddTicks(8473), "Admin", 1 },
                    { new Guid("47deca85-52fa-4f48-9371-f632db88acdd"), null, new DateTime(2025, 4, 25, 22, 2, 43, 273, DateTimeKind.Local).AddTicks(8500), "Khách hàng", 1 },
                    { new Guid("51c4f63f-defa-4740-b73b-e139d286b16c"), null, new DateTime(2025, 4, 25, 22, 2, 43, 273, DateTimeKind.Local).AddTicks(8498), "Nhân Viên", 1 }
                });

            migrationBuilder.InsertData(
                table: "tai_Khoans",
                columns: new[] { "ID", "ResetToken", "TokenExpiry", "Vai_TroID", "cccd", "email", "gioi_tinh", "hinh_anh", "ho_ten", "ma", "ngay_sinh", "ngay_sua", "ngay_tao", "pass_word", "sdt", "trang_thai", "user_name" },
                values: new object[] { new Guid("47ee1203-5084-4835-ac9a-50f5a1e44b70"), null, null, new Guid("28997333-8f12-4523-85f2-2727ac0eaa04"), "123456789012", "admin_tong@fpt.edu.vn", 1, "admin_tong.png", "Admin Tổng", "ADMIN01", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 4, 25, 22, 2, 43, 273, DateTimeKind.Local).AddTicks(8687), "Admin123", "0123456789", 1, "admin_tong" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("32b17115-037e-4893-81fd-e9f5f07b18f6"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("5b24f60d-3c23-467b-8f37-b186b30bc4f4"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("7a1828c2-e64c-4eca-b632-d210c5352f33"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("b34a2747-2008-4f5f-a2a3-7220f3a656f6"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("c4d5897d-dc1c-4fa1-be28-ddeaf5985f04"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("d04a3590-610e-4686-9e6e-cf0c2770b148"));

            migrationBuilder.DeleteData(
                table: "tai_Khoans",
                keyColumn: "ID",
                keyValue: new Guid("47ee1203-5084-4835-ac9a-50f5a1e44b70"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("47deca85-52fa-4f48-9371-f632db88acdd"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("51c4f63f-defa-4740-b73b-e139d286b16c"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("28997333-8f12-4523-85f2-2727ac0eaa04"));

            migrationBuilder.DropColumn(
                name: "ghi_chu",
                table: "tai_Khoan_Hoa_Dons");

            migrationBuilder.DropColumn(
                name: "thao_tac",
                table: "tai_Khoan_Hoa_Dons");

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
    }
}
