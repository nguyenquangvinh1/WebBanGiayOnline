using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class datnvinh1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_don_Chi_Tiets_hoa_Dons_Hoa_DonID",
                table: "don_Chi_Tiets");

            migrationBuilder.DropForeignKey(
                name: "FK_don_Chi_Tiets_san_Pham_Chi_Tiets_San_Pham_Chi_TietID",
                table: "don_Chi_Tiets");

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("6a2e0397-5087-42f5-94f2-5163f692eac9"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("8efb16f3-4253-4aee-a92b-030fc19f0260"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("ee8b6888-6fbf-4d86-b477-658490b446d8"));

            migrationBuilder.DropColumn(
                name: "thanh_tien",
                table: "don_Chi_Tiets");

            migrationBuilder.AlterColumn<int>(
                name: "trang_thai",
                table: "don_Chi_Tiets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "nguoi_tao",
                table: "don_Chi_Tiets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "San_Pham_Chi_TietID",
                table: "don_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Hoa_DonID",
                table: "don_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("232184e2-2983-4d1c-a945-49bc78e5ec44"), null, new DateTime(2025, 3, 12, 16, 39, 19, 68, DateTimeKind.Local).AddTicks(5234), "Nhân Viên", 1 },
                    { new Guid("417a8cb2-b755-411c-b912-478123bacbe0"), null, new DateTime(2025, 3, 12, 16, 39, 19, 68, DateTimeKind.Local).AddTicks(5214), "Admin", 1 },
                    { new Guid("e8f5c604-46d3-42bf-839b-70fc7f42787d"), null, new DateTime(2025, 3, 12, 16, 39, 19, 68, DateTimeKind.Local).AddTicks(5248), "Khách Hàng", 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_don_Chi_Tiets_hoa_Dons_Hoa_DonID",
                table: "don_Chi_Tiets",
                column: "Hoa_DonID",
                principalTable: "hoa_Dons",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_don_Chi_Tiets_san_Pham_Chi_Tiets_San_Pham_Chi_TietID",
                table: "don_Chi_Tiets",
                column: "San_Pham_Chi_TietID",
                principalTable: "san_Pham_Chi_Tiets",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_don_Chi_Tiets_hoa_Dons_Hoa_DonID",
                table: "don_Chi_Tiets");

            migrationBuilder.DropForeignKey(
                name: "FK_don_Chi_Tiets_san_Pham_Chi_Tiets_San_Pham_Chi_TietID",
                table: "don_Chi_Tiets");

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("232184e2-2983-4d1c-a945-49bc78e5ec44"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("417a8cb2-b755-411c-b912-478123bacbe0"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("e8f5c604-46d3-42bf-839b-70fc7f42787d"));

            migrationBuilder.AlterColumn<int>(
                name: "trang_thai",
                table: "don_Chi_Tiets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "nguoi_tao",
                table: "don_Chi_Tiets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "San_Pham_Chi_TietID",
                table: "don_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Hoa_DonID",
                table: "don_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "thanh_tien",
                table: "don_Chi_Tiets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("6a2e0397-5087-42f5-94f2-5163f692eac9"), null, new DateTime(2025, 3, 12, 15, 47, 28, 551, DateTimeKind.Local).AddTicks(9640), "Khách Hàng", 1 },
                    { new Guid("8efb16f3-4253-4aee-a92b-030fc19f0260"), null, new DateTime(2025, 3, 12, 15, 47, 28, 551, DateTimeKind.Local).AddTicks(9627), "Nhân Viên", 1 },
                    { new Guid("ee8b6888-6fbf-4d86-b477-658490b446d8"), null, new DateTime(2025, 3, 12, 15, 47, 28, 551, DateTimeKind.Local).AddTicks(9609), "Admin", 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_don_Chi_Tiets_hoa_Dons_Hoa_DonID",
                table: "don_Chi_Tiets",
                column: "Hoa_DonID",
                principalTable: "hoa_Dons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_don_Chi_Tiets_san_Pham_Chi_Tiets_San_Pham_Chi_TietID",
                table: "don_Chi_Tiets",
                column: "San_Pham_Chi_TietID",
                principalTable: "san_Pham_Chi_Tiets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
