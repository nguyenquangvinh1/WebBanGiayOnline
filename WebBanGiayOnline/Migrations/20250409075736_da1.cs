using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class da1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("35f0daba-ba35-4881-a7c0-5c2d86e0b837"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("b9f1e958-6111-4370-972d-f6a9620c4897"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("e337de82-9296-49da-8810-db16557e4d59"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("0c75ec3c-11e9-4d70-86b5-69c5fd6b448d"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("bf710196-3cc0-46b5-b9c6-ff121b2fd50c"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("cfb90941-c6bf-4cb1-a27c-202b8acbf5de"));

            migrationBuilder.AddColumn<double>(
                name: "Ship",
                table: "hoa_Dons",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Ship",
                table: "hoa_Dons");

            migrationBuilder.InsertData(
                table: "phuong_Thuc_Thanh_Toans",
                columns: new[] { "ID", "ma", "ten_phuong_thuc" },
                values: new object[,]
                {
                    { new Guid("35f0daba-ba35-4881-a7c0-5c2d86e0b837"), "CK", "Chuyển khoản" },
                    { new Guid("b9f1e958-6111-4370-972d-f6a9620c4897"), "ALL", "Cả 2" },
                    { new Guid("e337de82-9296-49da-8810-db16557e4d59"), "TTM", "Tiền mặt" }
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("0c75ec3c-11e9-4d70-86b5-69c5fd6b448d"), null, new DateTime(2025, 4, 9, 14, 13, 0, 642, DateTimeKind.Local).AddTicks(2703), "Admin", 1 },
                    { new Guid("bf710196-3cc0-46b5-b9c6-ff121b2fd50c"), null, new DateTime(2025, 4, 9, 14, 13, 0, 642, DateTimeKind.Local).AddTicks(2732), "Khách hàng", 1 },
                    { new Guid("cfb90941-c6bf-4cb1-a27c-202b8acbf5de"), null, new DateTime(2025, 4, 9, 14, 13, 0, 642, DateTimeKind.Local).AddTicks(2730), "Nhân Viên", 1 }
                });
        }
    }
}
