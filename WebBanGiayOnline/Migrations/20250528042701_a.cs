using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chat_Lieus",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ten_chat_lieu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    ten_loai_co_giay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    ten_danh_muc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    ten_de_giay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    ten_kich_thuoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    ten_kieu_dang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    ten_loai_giay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    ma_mau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    ten_mui_giay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    gia_tri_toi_thieu = table.Column<double>(type: "float", nullable: false),
                    so_tien_giam_toi_da = table.Column<int>(type: "int", nullable: false),
                    so_luong = table.Column<int>(type: "int", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    ngay_bat_dau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_ket_thuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    nguoi_tao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nguoi_sua = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "vai_Tros",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ten_vai_tro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vai_Tros", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "san_Phams",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ten_san_pham = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    nguoi_tao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nguoi_sua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kieu_DangID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Danh_MucID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Loai_GiayID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mui_GiayID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Co_GiayID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    De_GiayID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Chat_LieuID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_san_Phams", x => x.ID);
                    table.ForeignKey(
                        name: "FK_san_Phams_chat_Lieus_Chat_LieuID",
                        column: x => x.Chat_LieuID,
                        principalTable: "chat_Lieus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_san_Phams_co_Giays_Co_GiayID",
                        column: x => x.Co_GiayID,
                        principalTable: "co_Giays",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_san_Phams_danh_Mucs_Danh_MucID",
                        column: x => x.Danh_MucID,
                        principalTable: "danh_Mucs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_san_Phams_de_Giays_De_GiayID",
                        column: x => x.De_GiayID,
                        principalTable: "de_Giays",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_san_Phams_kieu_Dangs_Kieu_DangID",
                        column: x => x.Kieu_DangID,
                        principalTable: "kieu_Dangs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_san_Phams_loai_Giays_Loai_GiayID",
                        column: x => x.Loai_GiayID,
                        principalTable: "loai_Giays",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_san_Phams_mui_Giays_Mui_GiayID",
                        column: x => x.Mui_GiayID,
                        principalTable: "mui_Giays",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hoa_Dons",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaHoaDon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tong_tien = table.Column<double>(type: "float", nullable: false),
                    ghi_chu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    dia_chi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sdt_nguoi_nhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email_nguoi_nhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ship = table.Column<double>(type: "float", nullable: true),
                    loai_hoa_don = table.Column<int>(type: "int", nullable: false),
                    ten_nguoi_nhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    thoi_gian_nhan_hang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    nguoi_tao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nguoi_sua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giam_GiaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoa_Dons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_hoa_Dons_phieu_Giam_Gias_Giam_GiaID",
                        column: x => x.Giam_GiaID,
                        principalTable: "phieu_Giam_Gias",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "tai_Khoans",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pass_word = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ho_ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngay_sinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gioi_tinh = table.Column<int>(type: "int", nullable: false),
                    sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    cccd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hinh_anh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Vai_TroID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tai_Khoans", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tai_Khoans_vai_Tros_Vai_TroID",
                        column: x => x.Vai_TroID,
                        principalTable: "vai_Tros",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "anh_San_Phams",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    anh_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    San_PhamID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anh_San_Phams", x => x.ID);
                    table.ForeignKey(
                        name: "FK_anh_San_Phams_san_Phams_San_PhamID",
                        column: x => x.San_PhamID,
                        principalTable: "san_Phams",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "san_Pham_Chi_Tiets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ten_SPCT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gia = table.Column<double>(type: "float", nullable: false),
                    so_luong = table.Column<int>(type: "int", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Kich_ThuocID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mau_SacID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    San_PhamID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kieu_DangID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Danh_MucID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Loai_GiayID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mui_GiayID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Co_GiayID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    De_GiayID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_san_Pham_Chi_Tiets_danh_Mucs_Danh_MucID",
                        column: x => x.Danh_MucID,
                        principalTable: "danh_Mucs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_san_Pham_Chi_Tiets_de_Giays_De_GiayID",
                        column: x => x.De_GiayID,
                        principalTable: "de_Giays",
                        principalColumn: "ID");
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
                        principalColumn: "ID");
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
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_san_Pham_Chi_Tiets_san_Phams_San_PhamID",
                        column: x => x.San_PhamID,
                        principalTable: "san_Phams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "thanh_Toans",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    so_tien_thanh_toan = table.Column<double>(type: "float", nullable: false),
                    ngay_thanh_toan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hoa_DonID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Phuong_Thuc_Thanh_ToanID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thanh_Toans", x => x.ID);
                    table.ForeignKey(
                        name: "FK_thanh_Toans_hoa_Dons_Hoa_DonID",
                        column: x => x.Hoa_DonID,
                        principalTable: "hoa_Dons",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_thanh_Toans_phuong_Thuc_Thanh_Toans_Phuong_Thuc_Thanh_ToanID",
                        column: x => x.Phuong_Thuc_Thanh_ToanID,
                        principalTable: "phuong_Thuc_Thanh_Toans",
                        principalColumn: "ID");
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
                    dia_chi_chi_tiet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_default = table.Column<bool>(type: "bit", nullable: true),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tai_KhoanID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dia_Chis", x => x.ID);
                    table.ForeignKey(
                        name: "FK_dia_Chis_tai_Khoans_Tai_KhoanID",
                        column: x => x.Tai_KhoanID,
                        principalTable: "tai_Khoans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "phieu_Giam_Gia_Tai_Khoans",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    nguoi_tao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nguoi_sua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tai_KhoanID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phieu_Giam_GiaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phieu_Giam_Gia_Tai_Khoans", x => x.ID);
                    table.ForeignKey(
                        name: "FK_phieu_Giam_Gia_Tai_Khoans_phieu_Giam_Gias_Phieu_Giam_GiaID",
                        column: x => x.Phieu_Giam_GiaID,
                        principalTable: "phieu_Giam_Gias",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_phieu_Giam_Gia_Tai_Khoans_tai_Khoans_Tai_KhoanID",
                        column: x => x.Tai_KhoanID,
                        principalTable: "tai_Khoans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tai_Khoan_Hoa_Dons",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vai_tro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    thao_tac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ghi_chu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tai_KhoanID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Hoa_DonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tai_Khoan_Hoa_Dons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tai_Khoan_Hoa_Dons_hoa_Dons_Hoa_DonID",
                        column: x => x.Hoa_DonID,
                        principalTable: "hoa_Dons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tai_Khoan_Hoa_Dons_tai_Khoans_Tai_KhoanID",
                        column: x => x.Tai_KhoanID,
                        principalTable: "tai_Khoans",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "anh_San_Pham_San_Pham_Chi_Tiets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Anh_San_PhamID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    San_Pham_Chi_TietID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anh_San_Pham_San_Pham_Chi_Tiets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_anh_San_Pham_San_Pham_Chi_Tiets_anh_San_Phams_Anh_San_PhamID",
                        column: x => x.Anh_San_PhamID,
                        principalTable: "anh_San_Phams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_anh_San_Pham_San_Pham_Chi_Tiets_san_Pham_Chi_Tiets_San_Pham_Chi_TietID",
                        column: x => x.San_Pham_Chi_TietID,
                        principalTable: "san_Pham_Chi_Tiets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "don_Chi_Tiets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tensp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gia = table.Column<double>(type: "float", nullable: false),
                    trang_thai = table.Column<int>(type: "int", nullable: true),
                    thanh_tien = table.Column<double>(type: "float", nullable: true),
                    so_luong = table.Column<int>(type: "int", nullable: false),
                    phuongThucthanhtoan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_sua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    nguoi_tao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nguoi_sua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hoa_DonID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    San_Pham_Chi_TietID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Phuong_Thuc_Thanh_ToanID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_don_Chi_Tiets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_don_Chi_Tiets_hoa_Dons_Hoa_DonID",
                        column: x => x.Hoa_DonID,
                        principalTable: "hoa_Dons",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_don_Chi_Tiets_phuong_Thuc_Thanh_Toans_Phuong_Thuc_Thanh_ToanID",
                        column: x => x.Phuong_Thuc_Thanh_ToanID,
                        principalTable: "phuong_Thuc_Thanh_Toans",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_don_Chi_Tiets_san_Pham_Chi_Tiets_San_Pham_Chi_TietID",
                        column: x => x.San_Pham_Chi_TietID,
                        principalTable: "san_Pham_Chi_Tiets",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "thongKes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TongTien = table.Column<double>(type: "float", nullable: false),
                    DonThanhCong = table.Column<int>(type: "int", nullable: false),
                    DonHuy = table.Column<int>(type: "int", nullable: false),
                    SoLuongSP = table.Column<int>(type: "int", nullable: false),
                    DoanhThu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hoa_DonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    San_Pham_Chi_TietID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thongKes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_thongKes_hoa_Dons_Hoa_DonID",
                        column: x => x.Hoa_DonID,
                        principalTable: "hoa_Dons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_thongKes_san_Pham_Chi_Tiets_San_Pham_Chi_TietID",
                        column: x => x.San_Pham_Chi_TietID,
                        principalTable: "san_Pham_Chi_Tiets",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "phuong_Thuc_Thanh_Toans",
                columns: new[] { "ID", "ma", "ten_phuong_thuc" },
                values: new object[,]
                {
                    { new Guid("11e1b48a-90e7-485f-8a9a-9e88c546e93b"), "MoMo", "Thanh toán MoMo" },
                    { new Guid("2333513c-21c0-49b9-bce7-2fbb4b8cdf72"), "TTNH", "Thanh toán khi nhận hàng" },
                    { new Guid("48ce0c94-c372-401f-ae2f-956cf45b5df0"), "VNPAY", "Thanh toán VNPAY" },
                    { new Guid("69385c09-b22c-43d7-8ac1-dd806f1e275c"), "ALL", "Cả 2" },
                    { new Guid("bb0fb030-94a2-4543-8187-f76df6b8ed81"), "TTM", "Tiền mặt" },
                    { new Guid("c60ee00b-f879-4efb-9da3-bb8306cf27c1"), "CK", "Chuyển khoản" }
                });

            migrationBuilder.InsertData(
                table: "vai_Tros",
                columns: new[] { "ID", "ngay_sua", "ngay_tao", "ten_vai_tro", "trang_thai" },
                values: new object[,]
                {
                    { new Guid("05944916-3c5d-41df-b384-815cf9f49fc5"), null, new DateTime(2025, 5, 28, 11, 26, 55, 841, DateTimeKind.Local).AddTicks(4792), "Khách hàng", 1 },
                    { new Guid("c04e970c-df85-4b21-8689-dfed43e327f5"), null, new DateTime(2025, 5, 28, 11, 26, 55, 841, DateTimeKind.Local).AddTicks(4790), "Nhân Viên", 1 },
                    { new Guid("cf58a99f-5259-46ab-8532-b955df4fc403"), null, new DateTime(2025, 5, 28, 11, 26, 55, 841, DateTimeKind.Local).AddTicks(4771), "Admin", 1 }
                });

            migrationBuilder.InsertData(
                table: "tai_Khoans",
                columns: new[] { "ID", "ResetToken", "TokenExpiry", "Vai_TroID", "cccd", "email", "gioi_tinh", "hinh_anh", "ho_ten", "ma", "ngay_sinh", "ngay_sua", "ngay_tao", "pass_word", "sdt", "trang_thai", "user_name" },
                values: new object[] { new Guid("12b5c169-bead-4eb6-be65-1e4128bafb09"), null, null, new Guid("cf58a99f-5259-46ab-8532-b955df4fc403"), "123456789012", "admin_tong@fpt.edu.vn", 1, "default-avatar.jpg", "Admin Tổng", "ADMIN01", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 5, 28, 11, 26, 55, 841, DateTimeKind.Local).AddTicks(4990), "Admin123", "0123456789", 1, "admin_tong" });

            migrationBuilder.CreateIndex(
                name: "IX_anh_San_Pham_San_Pham_Chi_Tiets_Anh_San_PhamID",
                table: "anh_San_Pham_San_Pham_Chi_Tiets",
                column: "Anh_San_PhamID");

            migrationBuilder.CreateIndex(
                name: "IX_anh_San_Pham_San_Pham_Chi_Tiets_San_Pham_Chi_TietID",
                table: "anh_San_Pham_San_Pham_Chi_Tiets",
                column: "San_Pham_Chi_TietID");

            migrationBuilder.CreateIndex(
                name: "IX_anh_San_Phams_San_PhamID",
                table: "anh_San_Phams",
                column: "San_PhamID");

            migrationBuilder.CreateIndex(
                name: "IX_dia_Chis_Tai_KhoanID",
                table: "dia_Chis",
                column: "Tai_KhoanID");

            migrationBuilder.CreateIndex(
                name: "IX_don_Chi_Tiets_Hoa_DonID",
                table: "don_Chi_Tiets",
                column: "Hoa_DonID");

            migrationBuilder.CreateIndex(
                name: "IX_don_Chi_Tiets_Phuong_Thuc_Thanh_ToanID",
                table: "don_Chi_Tiets",
                column: "Phuong_Thuc_Thanh_ToanID");

            migrationBuilder.CreateIndex(
                name: "IX_don_Chi_Tiets_San_Pham_Chi_TietID",
                table: "don_Chi_Tiets",
                column: "San_Pham_Chi_TietID");

            migrationBuilder.CreateIndex(
                name: "IX_hoa_Dons_Giam_GiaID",
                table: "hoa_Dons",
                column: "Giam_GiaID");

            migrationBuilder.CreateIndex(
                name: "IX_phieu_Giam_Gia_Tai_Khoans_Phieu_Giam_GiaID",
                table: "phieu_Giam_Gia_Tai_Khoans",
                column: "Phieu_Giam_GiaID");

            migrationBuilder.CreateIndex(
                name: "IX_phieu_Giam_Gia_Tai_Khoans_Tai_KhoanID",
                table: "phieu_Giam_Gia_Tai_Khoans",
                column: "Tai_KhoanID");

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
                name: "IX_san_Pham_Chi_Tiets_San_PhamID",
                table: "san_Pham_Chi_Tiets",
                column: "San_PhamID");

            migrationBuilder.CreateIndex(
                name: "IX_san_Phams_Chat_LieuID",
                table: "san_Phams",
                column: "Chat_LieuID");

            migrationBuilder.CreateIndex(
                name: "IX_san_Phams_Co_GiayID",
                table: "san_Phams",
                column: "Co_GiayID");

            migrationBuilder.CreateIndex(
                name: "IX_san_Phams_Danh_MucID",
                table: "san_Phams",
                column: "Danh_MucID");

            migrationBuilder.CreateIndex(
                name: "IX_san_Phams_De_GiayID",
                table: "san_Phams",
                column: "De_GiayID");

            migrationBuilder.CreateIndex(
                name: "IX_san_Phams_Kieu_DangID",
                table: "san_Phams",
                column: "Kieu_DangID");

            migrationBuilder.CreateIndex(
                name: "IX_san_Phams_Loai_GiayID",
                table: "san_Phams",
                column: "Loai_GiayID");

            migrationBuilder.CreateIndex(
                name: "IX_san_Phams_Mui_GiayID",
                table: "san_Phams",
                column: "Mui_GiayID");

            migrationBuilder.CreateIndex(
                name: "IX_tai_Khoan_Hoa_Dons_Hoa_DonID",
                table: "tai_Khoan_Hoa_Dons",
                column: "Hoa_DonID");

            migrationBuilder.CreateIndex(
                name: "IX_tai_Khoan_Hoa_Dons_Tai_KhoanID",
                table: "tai_Khoan_Hoa_Dons",
                column: "Tai_KhoanID");

            migrationBuilder.CreateIndex(
                name: "IX_tai_Khoans_Vai_TroID",
                table: "tai_Khoans",
                column: "Vai_TroID");

            migrationBuilder.CreateIndex(
                name: "IX_thanh_Toans_Hoa_DonID",
                table: "thanh_Toans",
                column: "Hoa_DonID");

            migrationBuilder.CreateIndex(
                name: "IX_thanh_Toans_Phuong_Thuc_Thanh_ToanID",
                table: "thanh_Toans",
                column: "Phuong_Thuc_Thanh_ToanID");

            migrationBuilder.CreateIndex(
                name: "IX_thongKes_Hoa_DonID",
                table: "thongKes",
                column: "Hoa_DonID");

            migrationBuilder.CreateIndex(
                name: "IX_thongKes_San_Pham_Chi_TietID",
                table: "thongKes",
                column: "San_Pham_Chi_TietID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "anh_San_Pham_San_Pham_Chi_Tiets");

            migrationBuilder.DropTable(
                name: "dia_Chis");

            migrationBuilder.DropTable(
                name: "don_Chi_Tiets");

            migrationBuilder.DropTable(
                name: "phieu_Giam_Gia_Tai_Khoans");

            migrationBuilder.DropTable(
                name: "tai_Khoan_Hoa_Dons");

            migrationBuilder.DropTable(
                name: "thanh_Toans");

            migrationBuilder.DropTable(
                name: "thongKes");

            migrationBuilder.DropTable(
                name: "anh_San_Phams");

            migrationBuilder.DropTable(
                name: "tai_Khoans");

            migrationBuilder.DropTable(
                name: "phuong_Thuc_Thanh_Toans");

            migrationBuilder.DropTable(
                name: "hoa_Dons");

            migrationBuilder.DropTable(
                name: "san_Pham_Chi_Tiets");

            migrationBuilder.DropTable(
                name: "vai_Tros");

            migrationBuilder.DropTable(
                name: "phieu_Giam_Gias");

            migrationBuilder.DropTable(
                name: "kich_Thuocs");

            migrationBuilder.DropTable(
                name: "mau_Sacs");

            migrationBuilder.DropTable(
                name: "san_Phams");

            migrationBuilder.DropTable(
                name: "chat_Lieus");

            migrationBuilder.DropTable(
                name: "co_Giays");

            migrationBuilder.DropTable(
                name: "danh_Mucs");

            migrationBuilder.DropTable(
                name: "de_Giays");

            migrationBuilder.DropTable(
                name: "kieu_Dangs");

            migrationBuilder.DropTable(
                name: "loai_Giays");

            migrationBuilder.DropTable(
                name: "mui_Giays");
        }
    }
}
