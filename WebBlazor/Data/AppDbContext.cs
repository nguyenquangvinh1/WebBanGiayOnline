﻿using ClssLib;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Drawing;

namespace WebBlazor.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Anh_San_Pham> anh_San_Phams { get; set; }
        public DbSet<Chat_Lieu> chat_Lieus { get; set; }
        public DbSet<Co_Giay> co_Giays { get; set; }
        public DbSet<Danh_Muc> danh_Mucs { get; set; }
        public DbSet<De_Giay> de_Giays { get; set; }
        public DbSet<Dia_Chi> dia_Chis { get; set; }
        public DbSet<Hoa_Don> hoa_Dons { get; set; }
        public DbSet<Hoa_Don_Chi_Tiet> don_Chi_Tiets { get; set; }
        public DbSet<Khach_Hang> khach_Hangs { get; set; }
        public DbSet<Kich_Thuoc> kich_Thuocs { get; set; }
        public DbSet<Kieu_Dang> kieu_Dangs { get; set; }
        public DbSet<Loai_Giay> loai_Giays { get; set; }
        public DbSet<Mau_Sac> mau_Sacs { get; set; }
        public DbSet<Mui_Giay> mui_Giays { get; set; }
        public DbSet<Nhan_Vien> nhan_Viens { get; set; }
        public DbSet<Phieu_Giam_Gia> phieu_Giam_Gias { get; set; }
        public DbSet<Phieu_Giam_Gia_Khach_Hang> giam_Gia_Khach_Hangs { get; set; }
        public DbSet<Phuong_Thuc_Thanh_Toan> phuong_Thuc_Thanh_Toans { get; set; }
        public DbSet<San_Pham> san_Phams { get; set; }
        public DbSet<San_Pham_Chi_Tiet> san_Pham_Chi_Tiets { get; set; }
        public DbSet<Tai_Khoan> tai_Khoans { get; set; }
        public DbSet<Thanh_Toan> thanh_Toans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Nhan_Vien>()
                .HasOne(p => p.Tai_Khoan)
                .WithOne(n=>n.Nhan_Vien)
                .HasForeignKey<Nhan_Vien>(nv => nv.Tai_KhoanID);
            modelBuilder.Entity<Khach_Hang>()
               .HasOne(p => p.Tai_Khoan)
               .WithOne(h =>h.Khach_Hang)
               .HasForeignKey<Khach_Hang>(nv => nv.Tai_KhoanID);


        }
       


    }
}
