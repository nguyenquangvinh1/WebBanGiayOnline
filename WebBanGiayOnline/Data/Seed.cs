using System;
using System.Collections.Generic;
using ClssLib; // hoặc namespace chứa model của bạn
using Microsoft.EntityFrameworkCore;

namespace WebBanGiay.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            // Seed Vai_Tro (như cũ)
            List<Vai_Tro> vai_Tros = new List<Vai_Tro>()
            {
                new Vai_Tro{ID= Guid.NewGuid(), ten_vai_tro = "Admin", trang_thai = 1, ngay_tao = DateTime.Now},
                new Vai_Tro{ID= Guid.NewGuid(), ten_vai_tro = "Nhân Viên", trang_thai = 1, ngay_tao = DateTime.Now},
                new Vai_Tro{ID= Guid.NewGuid(), ten_vai_tro = "Khách hàng", trang_thai = 1, ngay_tao = DateTime.Now}
            };
            builder.Entity<Vai_Tro>().HasData(vai_Tros);

            // Lấy ID của vai trò Admin
            var adminRoleId = vai_Tros.FirstOrDefault(r => r.ten_vai_tro == "Admin")?.ID;

            if (adminRoleId.HasValue)
            {
                // Thêm tài khoản Admin Tổng
                builder.Entity<Tai_Khoan>().HasData(new Tai_Khoan
                {
                    ID = Guid.NewGuid(),
                    user_name = "admin_tong", // Tên tài khoản
                    pass_word = "Admin123", // Mật khẩu không mã hóa
                    ho_ten = "Admin Tổng", // Tên đầy đủ
                    ma = "ADMIN01", // Mã tài khoản
                    ngay_sinh = new DateTime(1980, 1, 1), // Ngày sinh
                    gioi_tinh = 1, // Giới tính
                    sdt = "0123456789", // Số điện thoại
                    email = "admin_tong@fpt.edu.vn", // Email
                    trang_thai = 1, // Trạng thái tài khoản
                    cccd = "123456789012", // Số CCCD
                    hinh_anh = "default-avatar.jpg", // Ảnh đại diện
                    ngay_tao = DateTime.Now, // Ngày tạo tài khoản
                    Vai_TroID = adminRoleId.Value, // Vai trò Admin
                    ResetToken = null, // Token reset mật khẩu
                    TokenExpiry = null // Thời gian hết hạn token reset mật khẩu
                });
            }

            // Seed phương thức thanh toán (ví dụ)
            List<Phuong_Thuc_Thanh_Toan> paymentMethods = new List<Phuong_Thuc_Thanh_Toan>()
            {
                new Phuong_Thuc_Thanh_Toan
                {
                    ID = Guid.NewGuid(),
                    ma = "TTM",
                    ten_phuong_thuc = "Tiền mặt"
                },
                new Phuong_Thuc_Thanh_Toan
                {
                    ID = Guid.NewGuid(),
                    ma = "CK",
                    ten_phuong_thuc = "Chuyển khoản"
                },
                new Phuong_Thuc_Thanh_Toan
                {
                    ID = Guid.NewGuid(),
                    ma = "ALL", // Hoặc "ca2" tùy theo quy ước của bạn
                    ten_phuong_thuc = "Cả 2"
                },
                 new Phuong_Thuc_Thanh_Toan
                {
                    ID = Guid.NewGuid(),
                    ma = "TTNH",
                    ten_phuong_thuc = "Thanh toán khi nhận hàng"
                },
                  new Phuong_Thuc_Thanh_Toan
                {
                    ID = Guid.NewGuid(),
                    ma = "VNPAY",
                    ten_phuong_thuc = "Thanh toán VNPAY"
                },
                   new Phuong_Thuc_Thanh_Toan
                {
                    ID = Guid.NewGuid(),
                    ma = "MoMo",
                    ten_phuong_thuc = "Thanh toán MoMo"
                },
            };
            builder.Entity<Phuong_Thuc_Thanh_Toan>().HasData(paymentMethods);
        }
    }
}