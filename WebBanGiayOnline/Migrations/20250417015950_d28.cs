using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class d28 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gio_Hang_Chi_Tiets");

            migrationBuilder.DropTable(
                name: "gio_Hangs");

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("1c5f236d-5c92-41a2-9c33-c3e635f03f9c"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("6a78fe7a-9e74-44f9-a33d-464a4091bcb7"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("cbb356c2-1344-4f3c-8862-79b1f5f7c9ac"));

            migrationBuilder.DeleteData(
                table: "tai_Khoans",
                keyColumn: "ID",
                keyValue: new Guid("7ce009e4-b407-436b-84cb-e129cc9c2cc4"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("1fb4e686-57a7-4b0d-acb0-8eb56dfa5b68"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("8df5a305-aa0c-4138-97dd-08e7591ff4b1"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("5db36228-5860-42aa-a4d3-9abe38e3697e"));

            migrationBuilder.AddColumn<string>(
                name: "discount",
                table: "hoa_Dons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "phuong_Thuc_Thanh_Toans",
                columns: new[] { "ID", "ma", "ten_phuong_thuc" },
                values: new object[,]
                {
                    { new Guid("b9fc7a66-a407-4e3e-baf4-ff4c2a490951"), "TTM", "Tiền mặt" },
                    { new Guid("e25880ae-fce0-4955-875e-781f17d147e2"), "CK", "Chuyển khoản" },
                    { new Guid("e686e410-925c-4c87-acc3-bc9732adac6b"), "ALL", "Cả 2" }
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("9c206d8b-54d7-46cd-b21d-c713cb8a6d4a"), null, new DateTime(2025, 4, 17, 8, 59, 47, 54, DateTimeKind.Local).AddTicks(1274), "Nhân Viên", 1 },
                    { new Guid("a318b59e-8235-49bd-9617-45ddc02ec9b5"), null, new DateTime(2025, 4, 17, 8, 59, 47, 54, DateTimeKind.Local).AddTicks(1253), "Admin", 1 },
                    { new Guid("bbe189d8-ec50-4da5-ad96-f4946ac9dd81"), null, new DateTime(2025, 4, 17, 8, 59, 47, 54, DateTimeKind.Local).AddTicks(1287), "Khách hàng", 1 }
                });

            migrationBuilder.InsertData(
                table: "tai_Khoans",
                columns: new[] { "ID", "ResetToken", "TokenExpiry", "Vai_TroID", "cccd", "email", "gioi_tinh", "hinh_anh", "ho_ten", "ma", "ngay_sinh", "ngay_sua", "ngay_tao", "pass_word", "sdt", "trang_thai", "user_name" },
                values: new object[] { new Guid("f9765ece-a7de-4f59-a1ad-910b9ca7fde7"), null, null, new Guid("a318b59e-8235-49bd-9617-45ddc02ec9b5"), "123456789012", "admin_tong@fpt.edu.vn", 1, "admin_tong.png", "Admin Tổng", "ADMIN01", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 4, 17, 8, 59, 47, 54, DateTimeKind.Local).AddTicks(1536), "Admin123", "0123456789", 1, "admin_tong" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("b9fc7a66-a407-4e3e-baf4-ff4c2a490951"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("e25880ae-fce0-4955-875e-781f17d147e2"));

            migrationBuilder.DeleteData(
                table: "phuong_Thuc_Thanh_Toans",
                keyColumn: "ID",
                keyValue: new Guid("e686e410-925c-4c87-acc3-bc9732adac6b"));

            migrationBuilder.DeleteData(
                table: "tai_Khoans",
                keyColumn: "ID",
                keyValue: new Guid("f9765ece-a7de-4f59-a1ad-910b9ca7fde7"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("9c206d8b-54d7-46cd-b21d-c713cb8a6d4a"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("bbe189d8-ec50-4da5-ad96-f4946ac9dd81"));

            migrationBuilder.DeleteData(
                table: "vai_Tros",
                keyColumn: "ID",
                keyValue: new Guid("a318b59e-8235-49bd-9617-45ddc02ec9b5"));

            migrationBuilder.DropColumn(
                name: "discount",
                table: "hoa_Dons");

            migrationBuilder.CreateTable(
                name: "gio_Hangs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phieu_Giam_GiaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tai_KhoanID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tong_tien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gio_Hangs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_gio_Hangs_phieu_Giam_Gias_Phieu_Giam_GiaID",
                        column: x => x.Phieu_Giam_GiaID,
                        principalTable: "phieu_Giam_Gias",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gio_Hangs_tai_Khoans_Tai_KhoanID",
                        column: x => x.Tai_KhoanID,
                        principalTable: "tai_Khoans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gio_Hang_Chi_Tiets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gio_HangID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    San_Pham_Chi_TietID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    so_luong = table.Column<int>(type: "int", nullable: false),
                    thanh_tien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gio_Hang_Chi_Tiets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_gio_Hang_Chi_Tiets_gio_Hangs_Gio_HangID",
                        column: x => x.Gio_HangID,
                        principalTable: "gio_Hangs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gio_Hang_Chi_Tiets_san_Pham_Chi_Tiets_San_Pham_Chi_TietID",
                        column: x => x.San_Pham_Chi_TietID,
                        principalTable: "san_Pham_Chi_Tiets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "phuong_Thuc_Thanh_Toans",
                columns: new[] { "ID", "ma", "ten_phuong_thuc" },
                values: new object[,]
                {
                    { new Guid("1c5f236d-5c92-41a2-9c33-c3e635f03f9c"), "CK", "Chuyển khoản" },
                    { new Guid("6a78fe7a-9e74-44f9-a33d-464a4091bcb7"), "TTM", "Tiền mặt" },
                    { new Guid("cbb356c2-1344-4f3c-8862-79b1f5f7c9ac"), "ALL", "Cả 2" }
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("1fb4e686-57a7-4b0d-acb0-8eb56dfa5b68"), null, new DateTime(2025, 4, 15, 14, 53, 45, 685, DateTimeKind.Local).AddTicks(9096), "Nhân Viên", 1 },
                    { new Guid("5db36228-5860-42aa-a4d3-9abe38e3697e"), null, new DateTime(2025, 4, 15, 14, 53, 45, 685, DateTimeKind.Local).AddTicks(9074), "Admin", 1 },
                    { new Guid("8df5a305-aa0c-4138-97dd-08e7591ff4b1"), null, new DateTime(2025, 4, 15, 14, 53, 45, 685, DateTimeKind.Local).AddTicks(9098), "Khách hàng", 1 }
                });

            migrationBuilder.InsertData(
                table: "tai_Khoans",
                columns: new[] { "ID", "ResetToken", "TokenExpiry", "Vai_TroID", "cccd", "email", "gioi_tinh", "hinh_anh", "ho_ten", "ma", "ngay_sinh", "ngay_sua", "ngay_tao", "pass_word", "sdt", "trang_thai", "user_name" },
                values: new object[] { new Guid("7ce009e4-b407-436b-84cb-e129cc9c2cc4"), null, null, new Guid("5db36228-5860-42aa-a4d3-9abe38e3697e"), "123456789012", "admin_tong@fpt.edu.vn", 1, "admin_tong.png", "Admin Tổng", "ADMIN01", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 4, 15, 14, 53, 45, 685, DateTimeKind.Local).AddTicks(9293), "Admin123", "0123456789", 1, "admin_tong" });

            migrationBuilder.CreateIndex(
                name: "IX_gio_Hang_Chi_Tiets_Gio_HangID",
                table: "gio_Hang_Chi_Tiets",
                column: "Gio_HangID");

            migrationBuilder.CreateIndex(
                name: "IX_gio_Hang_Chi_Tiets_San_Pham_Chi_TietID",
                table: "gio_Hang_Chi_Tiets",
                column: "San_Pham_Chi_TietID");

            migrationBuilder.CreateIndex(
                name: "IX_gio_Hangs_Phieu_Giam_GiaID",
                table: "gio_Hangs",
                column: "Phieu_Giam_GiaID");

            migrationBuilder.CreateIndex(
                name: "IX_gio_Hangs_Tai_KhoanID",
                table: "gio_Hangs",
                column: "Tai_KhoanID");
        }
    }
}
