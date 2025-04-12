using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class da12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("3dc77bb3-f3d9-4fad-88e4-d03563bba883"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("a1030c44-5d0f-4ddb-8804-3f0527e5254b"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("e93bba89-e596-4289-b23a-0e06ca6cfa7e"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("5f037ec8-3a40-4fd2-b933-7df618115725"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("8ee14b76-e824-4f2d-b3fa-678f6984a469"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("c486acd1-1b92-4b1d-a224-1c8511c69cba"));

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "hoa_Dons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "Ship",
                table: "hoa_Dons",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.InsertData(
                table: "phuong_Thuc_Thanh_Toans",
                columns: new[] { "ID", "ma", "ten_phuong_thuc" },
                values: new object[,]
                {
                    { new Guid("0f903d1c-b36c-4b1f-9691-e9038e16108d"), "ALL", "Cả 2" },
                    { new Guid("85a7c114-4cec-4b92-a010-bb6003635aaf"), "CK", "Chuyển khoản" },
                    { new Guid("88be6a15-b65e-4a7f-b9b5-90d529918f3a"), "TTM", "Tiền mặt" }
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("4492867c-14d4-4314-985d-494f0bfb21b9"), null, new DateTime(2025, 4, 9, 15, 1, 46, 806, DateTimeKind.Local).AddTicks(9958), "Admin", 1 },
                    { new Guid("91a8b6e9-9bfc-4358-9a79-d0cb1c379eec"), null, new DateTime(2025, 4, 9, 15, 1, 46, 806, DateTimeKind.Local).AddTicks(9978), "Nhân Viên", 1 },
                    { new Guid("eb0d8d32-96d8-458a-a709-21df686204cc"), null, new DateTime(2025, 4, 9, 15, 1, 46, 806, DateTimeKind.Local).AddTicks(9980), "Khách hàng", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("0f903d1c-b36c-4b1f-9691-e9038e16108d"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("85a7c114-4cec-4b92-a010-bb6003635aaf"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("88be6a15-b65e-4a7f-b9b5-90d529918f3a"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("4492867c-14d4-4314-985d-494f0bfb21b9"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("91a8b6e9-9bfc-4358-9a79-d0cb1c379eec"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("eb0d8d32-96d8-458a-a709-21df686204cc"));

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "hoa_Dons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Ship",
                table: "hoa_Dons",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "phuong_Thuc_Thanh_Toans",
                columns: new[] { "ID", "ma", "ten_phuong_thuc" },
                values: new object[,]
                {
                    { new Guid("3dc77bb3-f3d9-4fad-88e4-d03563bba883"), "TTM", "Tiền mặt" },
                    { new Guid("a1030c44-5d0f-4ddb-8804-3f0527e5254b"), "CK", "Chuyển khoản" },
                    { new Guid("e93bba89-e596-4289-b23a-0e06ca6cfa7e"), "ALL", "Cả 2" }
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("5f037ec8-3a40-4fd2-b933-7df618115725"), null, new DateTime(2025, 4, 9, 14, 57, 34, 674, DateTimeKind.Local).AddTicks(5299), "Nhân Viên", 1 },
                    { new Guid("8ee14b76-e824-4f2d-b3fa-678f6984a469"), null, new DateTime(2025, 4, 9, 14, 57, 34, 674, DateTimeKind.Local).AddTicks(5275), "Admin", 1 },
                    { new Guid("c486acd1-1b92-4b1d-a224-1c8511c69cba"), null, new DateTime(2025, 4, 9, 14, 57, 34, 674, DateTimeKind.Local).AddTicks(5302), "Khách hàng", 1 }
                });
        }
    }
}
