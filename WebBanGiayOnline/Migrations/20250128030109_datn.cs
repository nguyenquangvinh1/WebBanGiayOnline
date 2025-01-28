using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class datn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chat_Lieus",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ten_chat_lieu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_Lieus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "co_Giays",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ten_loai_co_giay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_co_Giays", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "danh_Mucs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ten_danh_muc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_danh_Mucs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "de_Giays",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ten_de_giay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_de_Giays", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "kich_Thuocs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ten_kich_thuoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kich_Thuocs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "kieu_Dangs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ten_kieu_dang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kieu_Dangs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "loai_Giays",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ten_loai_giay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loai_Giays", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "mau_Sacs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ten_mau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mau_Sacs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "mui_Giays",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ten_mui_giay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mui_Giays", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "phieu_Giam_Gias",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ten_phieu_giam_gia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    loai_phieu_giam_gia = table.Column<int>(type: "int", nullable: false),
                    kieu_giam_gia = table.Column<int>(type: "int", nullable: false),
                    gia_tri_giam = table.Column<int>(type: "int", nullable: false),
                    gia_tri_toi_thieu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    so_tien_giam_toi_da = table.Column<int>(type: "int", nullable: false),
                    so_luong = table.Column<int>(type: "int", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    ngay_bat_dau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_ket_thuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nguoi_tao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nguoi_sua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phieu_Giam_Gias", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "phuong_Thuc_Thanh_Toans",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ten_phuong_thuc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phuong_Thuc_Thanh_Toans", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "san_Phams",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ten_san_pham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nguoi_tao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nguoi_sua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_san_Phams", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tai_Khoans",
                columns: table => new
                {
                    Tai_KhoanID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ten_vai_tro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pass_word = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tai_Khoans", x => x.Tai_KhoanID);
                });

            migrationBuilder.CreateTable(
                name: "san_Pham_Chi_Tiets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    so_luong = table.Column<int>(type: "int", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    Kieu_DangID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mau_SacID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Danh_MucID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Loai_GiayID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mui_GiayID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Co_GiayID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    De_GiayID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kich_ThuocID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Chat_LieuID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_san_Pham_Chi_Tiets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_san_Pham_Chi_Tiets_chat_Lieus_Chat_LieuID",
                        column: x => x.Chat_LieuID,
                        principalTable: "chat_Lieus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_san_Pham_Chi_Tiets_co_Giays_Co_GiayID",
                        column: x => x.Co_GiayID,
                        principalTable: "co_Giays",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_san_Pham_Chi_Tiets_danh_Mucs_Danh_MucID",
                        column: x => x.Danh_MucID,
                        principalTable: "danh_Mucs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_san_Pham_Chi_Tiets_de_Giays_De_GiayID",
                        column: x => x.De_GiayID,
                        principalTable: "de_Giays",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_san_Pham_Chi_Tiets_kich_Thuocs_Kich_ThuocID",
                        column: x => x.Kich_ThuocID,
                        principalTable: "kich_Thuocs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_san_Pham_Chi_Tiets_kieu_Dangs_Kieu_DangID",
                        column: x => x.Kieu_DangID,
                        principalTable: "kieu_Dangs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_san_Pham_Chi_Tiets_loai_Giays_Loai_GiayID",
                        column: x => x.Loai_GiayID,
                        principalTable: "loai_Giays",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_san_Pham_Chi_Tiets_mau_Sacs_Mau_SacID",
                        column: x => x.Mau_SacID,
                        principalTable: "mau_Sacs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_san_Pham_Chi_Tiets_mui_Giays_Mui_GiayID",
                        column: x => x.Mui_GiayID,
                        principalTable: "mui_Giays",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "khach_Hangs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tai_KhoanID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ho_ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngay_sinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gioi_tinh = table.Column<int>(type: "int", nullable: false),
                    sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_khach_Hangs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_khach_Hangs_tai_Khoans_Tai_KhoanID",
                        column: x => x.Tai_KhoanID,
                        principalTable: "tai_Khoans",
                        principalColumn: "Tai_KhoanID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nhan_Viens",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tai_KhoanID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ho_ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngay_sinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gioi_tinh = table.Column<int>(type: "int", nullable: false),
                    sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    cccd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nhan_Viens", x => x.ID);
                    table.ForeignKey(
                        name: "FK_nhan_Viens_tai_Khoans_Tai_KhoanID",
                        column: x => x.Tai_KhoanID,
                        principalTable: "tai_Khoans",
                        principalColumn: "Tai_KhoanID");
                });

            migrationBuilder.CreateTable(
                name: "anh_San_Phams",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    anh_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    San_Pham_Chi_TietID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anh_San_Phams", x => x.ID);
                    table.ForeignKey(
                        name: "FK_anh_San_Phams_san_Pham_Chi_Tiets_San_Pham_Chi_TietID",
                        column: x => x.San_Pham_Chi_TietID,
                        principalTable: "san_Pham_Chi_Tiets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dia_Chis",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    loai_dia_chi = table.Column<int>(type: "int", nullable: false),
                    tinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    huyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    xa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Khach_HangID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dia_Chis", x => x.ID);
                    table.ForeignKey(
                        name: "FK_dia_Chis_khach_Hangs_Khach_HangID",
                        column: x => x.Khach_HangID,
                        principalTable: "khach_Hangs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "giam_Gia_Khach_Hangs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nguoi_tao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nguoi_sua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Khach_HangID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phieu_Giam_GiaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_giam_Gia_Khach_Hangs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_giam_Gia_Khach_Hangs_khach_Hangs_Khach_HangID",
                        column: x => x.Khach_HangID,
                        principalTable: "khach_Hangs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_giam_Gia_Khach_Hangs_phieu_Giam_Gias_Phieu_Giam_GiaID",
                        column: x => x.Phieu_Giam_GiaID,
                        principalTable: "phieu_Giam_Gias",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hoa_Dons",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tong_tien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ghi_chu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    dia_chi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sdt_nguoi_nhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email_nguoi_nhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    loai_hoa_don = table.Column<int>(type: "int", nullable: false),
                    ten_nguoi_nhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    thoi_gian_nhan_hang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nguoi_tao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nguoi_sua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Khach_HangID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nhan_VienID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Giam_GiaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoa_Dons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_hoa_Dons_khach_Hangs_Khach_HangID",
                        column: x => x.Khach_HangID,
                        principalTable: "khach_Hangs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hoa_Dons_nhan_Viens_Nhan_VienID",
                        column: x => x.Nhan_VienID,
                        principalTable: "nhan_Viens",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hoa_Dons_phieu_Giam_Gias_Giam_GiaID",
                        column: x => x.Giam_GiaID,
                        principalTable: "phieu_Giam_Gias",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "don_Chi_Tiets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    thanh_tien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    so_luong = table.Column<int>(type: "int", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nguoi_tao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nguoi_sua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hoa_DonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_don_Chi_Tiets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_don_Chi_Tiets_hoa_Dons_Hoa_DonID",
                        column: x => x.Hoa_DonID,
                        principalTable: "hoa_Dons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "thanh_Toans",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    so_tien_thanh_toan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ngay_thanh_toan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hoa_DonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phuong_Thuc_Thanh_ToanID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thanh_Toans", x => x.ID);
                    table.ForeignKey(
                        name: "FK_thanh_Toans_hoa_Dons_Hoa_DonID",
                        column: x => x.Hoa_DonID,
                        principalTable: "hoa_Dons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_thanh_Toans_phuong_Thuc_Thanh_Toans_Phuong_Thuc_Thanh_ToanID",
                        column: x => x.Phuong_Thuc_Thanh_ToanID,
                        principalTable: "phuong_Thuc_Thanh_Toans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_anh_San_Phams_San_Pham_Chi_TietID",
                table: "anh_San_Phams",
                column: "San_Pham_Chi_TietID");

            migrationBuilder.CreateIndex(
                name: "IX_dia_Chis_Khach_HangID",
                table: "dia_Chis",
                column: "Khach_HangID");

            migrationBuilder.CreateIndex(
                name: "IX_don_Chi_Tiets_Hoa_DonID",
                table: "don_Chi_Tiets",
                column: "Hoa_DonID");

            migrationBuilder.CreateIndex(
                name: "IX_giam_Gia_Khach_Hangs_Khach_HangID",
                table: "giam_Gia_Khach_Hangs",
                column: "Khach_HangID");

            migrationBuilder.CreateIndex(
                name: "IX_giam_Gia_Khach_Hangs_Phieu_Giam_GiaID",
                table: "giam_Gia_Khach_Hangs",
                column: "Phieu_Giam_GiaID");

            migrationBuilder.CreateIndex(
                name: "IX_hoa_Dons_Giam_GiaID",
                table: "hoa_Dons",
                column: "Giam_GiaID");

            migrationBuilder.CreateIndex(
                name: "IX_hoa_Dons_Khach_HangID",
                table: "hoa_Dons",
                column: "Khach_HangID");

            migrationBuilder.CreateIndex(
                name: "IX_hoa_Dons_Nhan_VienID",
                table: "hoa_Dons",
                column: "Nhan_VienID");

            migrationBuilder.CreateIndex(
                name: "IX_khach_Hangs_Tai_KhoanID",
                table: "khach_Hangs",
                column: "Tai_KhoanID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_nhan_Viens_Tai_KhoanID",
                table: "nhan_Viens",
                column: "Tai_KhoanID",
                unique: true,
                filter: "[Tai_KhoanID] IS NOT NULL");

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
                name: "IX_san_Pham_Chi_Tiets_Kich_ThuocID",
                table: "san_Pham_Chi_Tiets",
                column: "Kich_ThuocID");

            migrationBuilder.CreateIndex(
                name: "IX_san_Pham_Chi_Tiets_Kieu_DangID",
                table: "san_Pham_Chi_Tiets",
                column: "Kieu_DangID");

            migrationBuilder.CreateIndex(
                name: "IX_san_Pham_Chi_Tiets_Loai_GiayID",
                table: "san_Pham_Chi_Tiets",
                column: "Loai_GiayID");

            migrationBuilder.CreateIndex(
                name: "IX_san_Pham_Chi_Tiets_Mau_SacID",
                table: "san_Pham_Chi_Tiets",
                column: "Mau_SacID");

            migrationBuilder.CreateIndex(
                name: "IX_san_Pham_Chi_Tiets_Mui_GiayID",
                table: "san_Pham_Chi_Tiets",
                column: "Mui_GiayID");

            migrationBuilder.CreateIndex(
                name: "IX_thanh_Toans_Hoa_DonID",
                table: "thanh_Toans",
                column: "Hoa_DonID");

            migrationBuilder.CreateIndex(
                name: "IX_thanh_Toans_Phuong_Thuc_Thanh_ToanID",
                table: "thanh_Toans",
                column: "Phuong_Thuc_Thanh_ToanID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "anh_San_Phams");

            migrationBuilder.DropTable(
                name: "dia_Chis");

            migrationBuilder.DropTable(
                name: "don_Chi_Tiets");

            migrationBuilder.DropTable(
                name: "giam_Gia_Khach_Hangs");

            migrationBuilder.DropTable(
                name: "san_Phams");

            migrationBuilder.DropTable(
                name: "thanh_Toans");

            migrationBuilder.DropTable(
                name: "san_Pham_Chi_Tiets");

            migrationBuilder.DropTable(
                name: "hoa_Dons");

            migrationBuilder.DropTable(
                name: "phuong_Thuc_Thanh_Toans");

            migrationBuilder.DropTable(
                name: "chat_Lieus");

            migrationBuilder.DropTable(
                name: "co_Giays");

            migrationBuilder.DropTable(
                name: "danh_Mucs");

            migrationBuilder.DropTable(
                name: "de_Giays");

            migrationBuilder.DropTable(
                name: "kich_Thuocs");

            migrationBuilder.DropTable(
                name: "kieu_Dangs");

            migrationBuilder.DropTable(
                name: "loai_Giays");

            migrationBuilder.DropTable(
                name: "mau_Sacs");

            migrationBuilder.DropTable(
                name: "mui_Giays");

            migrationBuilder.DropTable(
                name: "khach_Hangs");

            migrationBuilder.DropTable(
                name: "nhan_Viens");

            migrationBuilder.DropTable(
                name: "phieu_Giam_Gias");

            migrationBuilder.DropTable(
                name: "tai_Khoans");
        }
    }
}
