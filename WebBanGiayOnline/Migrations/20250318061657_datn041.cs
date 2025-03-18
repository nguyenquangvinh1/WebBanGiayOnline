using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class datn041 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ten_danh_muc",
                table: "danh_Mucs",
                newName: "ten_loai_co_giay");

            migrationBuilder.AlterColumn<double>(
                name: "so_tien_thanh_toan",
                table: "thanh_Toans",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<Guid>(
                name: "Chat_LieuID",
                table: "san_Pham_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Co_GiayID",
                table: "san_Pham_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Danh_MucID",
                table: "san_Pham_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "De_GiayID",
                table: "san_Pham_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Kieu_DangID",
                table: "san_Pham_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Loai_GiayID",
                table: "san_Pham_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Mui_GiayID",
                table: "san_Pham_Chi_Tiets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_san_Pham_Chi_Tiets_Chat_LieuID",
                table: "san_Pham_Chi_Tiets",
                column: "Chat_LieuID");

            migrationBuilder.CreateIndex(
                name: "IX_san_Pham_Chi_Tiets_Co_GiayID",
                table: "san_Pham_Chi_Tiets",
                column: "Co_GiayID");

            migrationBuilder.CreateIndex(
                name: "IX_san_Pham_Chi_Tiets_Danh_MucID",
                table: "san_Pham_Chi_Tiets",
                column: "Danh_MucID");

            migrationBuilder.CreateIndex(
                name: "IX_san_Pham_Chi_Tiets_De_GiayID",
                table: "san_Pham_Chi_Tiets",
                column: "De_GiayID");

            migrationBuilder.CreateIndex(
                name: "IX_san_Pham_Chi_Tiets_Kieu_DangID",
                table: "san_Pham_Chi_Tiets",
                column: "Kieu_DangID");

            migrationBuilder.CreateIndex(
                name: "IX_san_Pham_Chi_Tiets_Loai_GiayID",
                table: "san_Pham_Chi_Tiets",
                column: "Loai_GiayID");

            migrationBuilder.CreateIndex(
                name: "IX_san_Pham_Chi_Tiets_Mui_GiayID",
                table: "san_Pham_Chi_Tiets",
                column: "Mui_GiayID");

            migrationBuilder.AddForeignKey(
                name: "FK_san_Pham_Chi_Tiets_chat_Lieus_Chat_LieuID",
                table: "san_Pham_Chi_Tiets",
                column: "Chat_LieuID",
                principalTable: "chat_Lieus",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_san_Pham_Chi_Tiets_co_Giays_Co_GiayID",
                table: "san_Pham_Chi_Tiets",
                column: "Co_GiayID",
                principalTable: "co_Giays",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_san_Pham_Chi_Tiets_danh_Mucs_Danh_MucID",
                table: "san_Pham_Chi_Tiets",
                column: "Danh_MucID",
                principalTable: "danh_Mucs",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_san_Pham_Chi_Tiets_de_Giays_De_GiayID",
                table: "san_Pham_Chi_Tiets",
                column: "De_GiayID",
                principalTable: "de_Giays",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_san_Pham_Chi_Tiets_kieu_Dangs_Kieu_DangID",
                table: "san_Pham_Chi_Tiets",
                column: "Kieu_DangID",
                principalTable: "kieu_Dangs",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_san_Pham_Chi_Tiets_loai_Giays_Loai_GiayID",
                table: "san_Pham_Chi_Tiets",
                column: "Loai_GiayID",
                principalTable: "loai_Giays",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_san_Pham_Chi_Tiets_mui_Giays_Mui_GiayID",
                table: "san_Pham_Chi_Tiets",
                column: "Mui_GiayID",
                principalTable: "mui_Giays",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_san_Pham_Chi_Tiets_chat_Lieus_Chat_LieuID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Pham_Chi_Tiets_co_Giays_Co_GiayID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Pham_Chi_Tiets_danh_Mucs_Danh_MucID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Pham_Chi_Tiets_de_Giays_De_GiayID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Pham_Chi_Tiets_kieu_Dangs_Kieu_DangID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Pham_Chi_Tiets_loai_Giays_Loai_GiayID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropForeignKey(
                name: "FK_san_Pham_Chi_Tiets_mui_Giays_Mui_GiayID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropIndex(
                name: "IX_san_Pham_Chi_Tiets_Chat_LieuID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropIndex(
                name: "IX_san_Pham_Chi_Tiets_Co_GiayID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropIndex(
                name: "IX_san_Pham_Chi_Tiets_Danh_MucID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropIndex(
                name: "IX_san_Pham_Chi_Tiets_De_GiayID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropIndex(
                name: "IX_san_Pham_Chi_Tiets_Kieu_DangID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropIndex(
                name: "IX_san_Pham_Chi_Tiets_Loai_GiayID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropIndex(
                name: "IX_san_Pham_Chi_Tiets_Mui_GiayID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropColumn(
                name: "Chat_LieuID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropColumn(
                name: "Co_GiayID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropColumn(
                name: "Danh_MucID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropColumn(
                name: "De_GiayID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropColumn(
                name: "Kieu_DangID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropColumn(
                name: "Loai_GiayID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.DropColumn(
                name: "Mui_GiayID",
                table: "san_Pham_Chi_Tiets");

            migrationBuilder.RenameColumn(
                name: "ten_loai_co_giay",
                table: "danh_Mucs",
                newName: "ten_danh_muc");

            migrationBuilder.AlterColumn<decimal>(
                name: "so_tien_thanh_toan",
                table: "thanh_Toans",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
