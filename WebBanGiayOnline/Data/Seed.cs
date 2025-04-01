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
                }
            };
            builder.Entity<Phuong_Thuc_Thanh_Toan>().HasData(paymentMethods);
        }
    }
}
