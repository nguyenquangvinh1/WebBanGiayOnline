using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class datn213 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_san_Pham_Chi_Tiets_chat_Lieus_Chat_LieuID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Pham_Chi_Tiets_kich_Thuocs_Kich_ThuocID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Pham_Chi_Tiets_loai_Giays_Loai_GiayID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Pham_Chi_Tiets_mau_Sacs_Mau_SacID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Phams_chat_Lieus_Chat_LieuID",
                table: "san_Phams");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Phams_loai_Giays_Loai_GiayID",
                table: "san_Phams");

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("00af5af2-61a4-4fd1-b52a-c5cd41f05e21"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("36cc2035-1654-43df-b007-db6ecd8279ba"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("7c1c0552-d14b-4271-9a8a-18392d4fc50c"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Loai_GiayID",
                table: "san_Phams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Chat_LieuID",
                table: "san_Phams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Mau_SacID",
                table: "san_Pham_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Loai_GiayID",
                table: "san_Pham_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Kich_ThuocID",
                table: "san_Pham_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Chat_LieuID",
                table: "san_Pham_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "gia_tri_toi_thieu",
                table: "phieu_Giam_Gias",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("1c421636-97ab-47a9-9c84-cdade25ce685"), null, new DateTime(2025, 4, 1, 14, 11, 26, 183, DateTimeKind.Local).AddTicks(8241), "Nhân Viên", 1 },
                    { new Guid("441b66b0-b788-4d26-9cd7-ee072f11c647"), null, new DateTime(2025, 4, 1, 14, 11, 26, 183, DateTimeKind.Local).AddTicks(8221), "Admin", 1 },
                    { new Guid("59078116-f05d-40f0-8cd3-a582d00a8229"), null, new DateTime(2025, 4, 1, 14, 11, 26, 183, DateTimeKind.Local).AddTicks(8243), "Khách hàng", 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_san_Pham_Chi_Tiets_chat_Lieus_Chat_LieuID",
                table: "san_Pham_Chi_Tiets",
                column: "Chat_LieuID",
                principalTable: "chat_Lieus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_san_Pham_Chi_Tiets_kich_Thuocs_Kich_ThuocID",
                table: "san_Pham_Chi_Tiets",
                column: "Kich_ThuocID",
                principalTable: "kich_Thuocs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_san_Pham_Chi_Tiets_loai_Giays_Loai_GiayID",
                table: "san_Pham_Chi_Tiets",
                column: "Loai_GiayID",
                principalTable: "loai_Giays",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_san_Pham_Chi_Tiets_mau_Sacs_Mau_SacID",
                table: "san_Pham_Chi_Tiets",
                column: "Mau_SacID",
                principalTable: "mau_Sacs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_san_Phams_chat_Lieus_Chat_LieuID",
                table: "san_Phams",
                column: "Chat_LieuID",
                principalTable: "chat_Lieus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_san_Phams_loai_Giays_Loai_GiayID",
                table: "san_Phams",
                column: "Loai_GiayID",
                principalTable: "loai_Giays",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_san_Pham_Chi_Tiets_chat_Lieus_Chat_LieuID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Pham_Chi_Tiets_kich_Thuocs_Kich_ThuocID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Pham_Chi_Tiets_loai_Giays_Loai_GiayID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Pham_Chi_Tiets_mau_Sacs_Mau_SacID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Phams_chat_Lieus_Chat_LieuID",
                table: "san_Phams");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Phams_loai_Giays_Loai_GiayID",
                table: "san_Phams");

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("1c421636-97ab-47a9-9c84-cdade25ce685"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("441b66b0-b788-4d26-9cd7-ee072f11c647"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("59078116-f05d-40f0-8cd3-a582d00a8229"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Loai_GiayID",
                table: "san_Phams",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Chat_LieuID",
                table: "san_Phams",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Mau_SacID",
                table: "san_Pham_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Loai_GiayID",
                table: "san_Pham_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Kich_ThuocID",
                table: "san_Pham_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Chat_LieuID",
                table: "san_Pham_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "gia_tri_toi_thieu",
                table: "phieu_Giam_Gias",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("00af5af2-61a4-4fd1-b52a-c5cd41f05e21"), null, new DateTime(2025, 3, 26, 10, 47, 45, 866, DateTimeKind.Local).AddTicks(8737), "Admin", 1 },
                    { new Guid("36cc2035-1654-43df-b007-db6ecd8279ba"), null, new DateTime(2025, 3, 26, 10, 47, 45, 866, DateTimeKind.Local).AddTicks(8757), "Nhân Viên", 1 },
                    { new Guid("7c1c0552-d14b-4271-9a8a-18392d4fc50c"), null, new DateTime(2025, 3, 26, 10, 47, 45, 866, DateTimeKind.Local).AddTicks(8759), "Khách hàng", 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_san_Pham_Chi_Tiets_chat_Lieus_Chat_LieuID",
                table: "san_Pham_Chi_Tiets",
                column: "Chat_LieuID",
                principalTable: "chat_Lieus",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_san_Pham_Chi_Tiets_kich_Thuocs_Kich_ThuocID",
                table: "san_Pham_Chi_Tiets",
                column: "Kich_ThuocID",
                principalTable: "kich_Thuocs",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_san_Pham_Chi_Tiets_loai_Giays_Loai_GiayID",
                table: "san_Pham_Chi_Tiets",
                column: "Loai_GiayID",
                principalTable: "loai_Giays",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_san_Pham_Chi_Tiets_mau_Sacs_Mau_SacID",
                table: "san_Pham_Chi_Tiets",
                column: "Mau_SacID",
                principalTable: "mau_Sacs",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_san_Phams_chat_Lieus_Chat_LieuID",
                table: "san_Phams",
                column: "Chat_LieuID",
                principalTable: "chat_Lieus",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_san_Phams_loai_Giays_Loai_GiayID",
                table: "san_Phams",
                column: "Loai_GiayID",
                principalTable: "loai_Giays",
                principalColumn: "ID");
        }
    }
}
