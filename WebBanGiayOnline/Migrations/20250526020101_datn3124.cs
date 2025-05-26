using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class datn3124 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_san_Phams_co_Giays_Co_GiayID",
                table: "san_Phams");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Phams_danh_Mucs_Danh_MucID",
                table: "san_Phams");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Phams_de_Giays_De_GiayID",
                table: "san_Phams");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Phams_kieu_Dangs_Kieu_DangID",
                table: "san_Phams");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Phams_mui_Giays_Mui_GiayID",
                table: "san_Phams");

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

            migrationBuilder.AlterColumn<string>(
                name: "ten_san_pham",
                table: "san_Phams",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Mui_GiayID",
                table: "san_Phams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Kieu_DangID",
                table: "san_Phams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "De_GiayID",
                table: "san_Phams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Danh_MucID",
                table: "san_Phams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Co_GiayID",
                table: "san_Phams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "so_tien_giam_toi_da",
                table: "phieu_Giam_Gias",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "gia_tri_toi_thieu",
                table: "phieu_Giam_Gias",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ghi_chu",
                table: "hoa_Dons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "phuong_Thuc_Thanh_Toans",
                columns: new[] { "ID", "ma", "ten_phuong_thuc" },
                values: new object[,]
                {
                    { new Guid("07ba264c-882a-43be-bfdf-2e2f31b843f1"), "MoMo", "Thanh toán MoMo" },
                    { new Guid("767ca49a-67fa-4b32-a369-6567242f749d"), "VNPAY", "Thanh toán VNPAY" },
                    { new Guid("9899a762-f240-49bf-83e6-6fbb6fd6613b"), "TTNH", "Thanh toán khi nhận hàng" },
                    { new Guid("9b60612e-d762-4490-b277-ceba1944857f"), "CK", "Chuyển khoản" },
                    { new Guid("a08e785d-8efe-4fd6-8f30-5538deff5928"), "ALL", "Cả 2" },
                    { new Guid("b9d0629c-3603-4c11-b47b-f6c555af55ce"), "TTM", "Tiền mặt" }
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("0499cd73-ec61-48f6-b9a3-8cdf21567af7"), null, new DateTime(2025, 5, 26, 9, 0, 58, 80, DateTimeKind.Local).AddTicks(1068), "Admin", 1 },
                    { new Guid("27726e5d-79d3-4d6d-afd5-9732eb8f5c42"), null, new DateTime(2025, 5, 26, 9, 0, 58, 80, DateTimeKind.Local).AddTicks(1085), "Khách hàng", 1 },
                    { new Guid("7610414a-957f-4562-be9f-4ce1f84d8c90"), null, new DateTime(2025, 5, 26, 9, 0, 58, 80, DateTimeKind.Local).AddTicks(1083), "Nhân Viên", 1 }
                });

            migrationBuilder.InsertData(
                table: "tai_Khoans",
                columns: new[] { "ID", "ResetToken", "TokenExpiry", "Vai_TroID", "cccd", "email", "gioi_tinh", "hinh_anh", "ho_ten", "ma", "ngay_sinh", "ngay_sua", "ngay_tao", "pass_word", "sdt", "trang_thai", "user_name" },
                values: new object[] { new Guid("ee056c12-a4d5-412f-98cf-834f622908f0"), null, null, new Guid("0499cd73-ec61-48f6-b9a3-8cdf21567af7"), "123456789012", "admin_tong@fpt.edu.vn", 1, "admin_tong.png", "Admin Tổng", "ADMIN01", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 5, 26, 9, 0, 58, 80, DateTimeKind.Local).AddTicks(1388), "Admin123", "0123456789", 1, "admin_tong" });

            migrationBuilder.AddForeignKey(
                name: "FK_san_Phams_co_Giays_Co_GiayID",
                table: "san_Phams",
                column: "Co_GiayID",
                principalTable: "co_Giays",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_san_Phams_danh_Mucs_Danh_MucID",
                table: "san_Phams",
                column: "Danh_MucID",
                principalTable: "danh_Mucs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_san_Phams_de_Giays_De_GiayID",
                table: "san_Phams",
                column: "De_GiayID",
                principalTable: "de_Giays",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_san_Phams_kieu_Dangs_Kieu_DangID",
                table: "san_Phams",
                column: "Kieu_DangID",
                principalTable: "kieu_Dangs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_san_Phams_mui_Giays_Mui_GiayID",
                table: "san_Phams",
                column: "Mui_GiayID",
                principalTable: "mui_Giays",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_san_Phams_co_Giays_Co_GiayID",
                table: "san_Phams");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Phams_danh_Mucs_Danh_MucID",
                table: "san_Phams");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Phams_de_Giays_De_GiayID",
                table: "san_Phams");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Phams_kieu_Dangs_Kieu_DangID",
                table: "san_Phams");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Phams_mui_Giays_Mui_GiayID",
                table: "san_Phams");

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("07ba264c-882a-43be-bfdf-2e2f31b843f1"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("767ca49a-67fa-4b32-a369-6567242f749d"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("9899a762-f240-49bf-83e6-6fbb6fd6613b"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("9b60612e-d762-4490-b277-ceba1944857f"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("a08e785d-8efe-4fd6-8f30-5538deff5928"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("b9d0629c-3603-4c11-b47b-f6c555af55ce"));

            migrationBuilder.DeleteData(
                table: "tai_Khoans",
                keyColumn: "ID",
                keyValue: new Guid("ee056c12-a4d5-412f-98cf-834f622908f0"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("27726e5d-79d3-4d6d-afd5-9732eb8f5c42"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("7610414a-957f-4562-be9f-4ce1f84d8c90"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("0499cd73-ec61-48f6-b9a3-8cdf21567af7"));

            migrationBuilder.AlterColumn<string>(
                name: "ten_san_pham",
                table: "san_Phams",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<Guid>(
                name: "Mui_GiayID",
                table: "san_Phams",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Kieu_DangID",
                table: "san_Phams",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "De_GiayID",
                table: "san_Phams",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Danh_MucID",
                table: "san_Phams",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Co_GiayID",
                table: "san_Phams",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "so_tien_giam_toi_da",
                table: "phieu_Giam_Gias",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "gia_tri_toi_thieu",
                table: "phieu_Giam_Gias",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "ghi_chu",
                table: "hoa_Dons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_san_Phams_co_Giays_Co_GiayID",
                table: "san_Phams",
                column: "Co_GiayID",
                principalTable: "co_Giays",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_san_Phams_danh_Mucs_Danh_MucID",
                table: "san_Phams",
                column: "Danh_MucID",
                principalTable: "danh_Mucs",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_san_Phams_de_Giays_De_GiayID",
                table: "san_Phams",
                column: "De_GiayID",
                principalTable: "de_Giays",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_san_Phams_kieu_Dangs_Kieu_DangID",
                table: "san_Phams",
                column: "Kieu_DangID",
                principalTable: "kieu_Dangs",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_san_Phams_mui_Giays_Mui_GiayID",
                table: "san_Phams",
                column: "Mui_GiayID",
                principalTable: "mui_Giays",
                principalColumn: "ID");
        }
    }
}
