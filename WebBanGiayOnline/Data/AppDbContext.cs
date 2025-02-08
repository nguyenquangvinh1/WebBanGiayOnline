using ClssLib;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Drawing;

namespace WebBanGiay.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Anh_San_Pham> anh_San_Phams { get; set; }
        public DbSet<Anh_San_Pham_San_Pham_Chi_Tiet> anh_San_Pham_San_Pham_Chi_Tiets { get; set; }
        public DbSet<Chat_Lieu> chat_Lieus { get; set; }
        public DbSet<Co_Giay> co_Giays { get; set; }
        public DbSet<Danh_Muc> danh_Mucs { get; set; }
        public DbSet<De_Giay> de_Giays { get; set; }
        public DbSet<Dia_Chi> dia_Chis { get; set; }
        public DbSet<Gio_Hang> gio_Hangs { get; set; }
        public DbSet<Gio_Hang_Chi_Tiet> gio_Hang_Chi_Tiets { get; set; }
        public DbSet<Hoa_Don> hoa_Dons { get; set; }
        public DbSet<Hoa_Don_Chi_Tiet> don_Chi_Tiets { get; set; }
        public DbSet<Kich_Thuoc> kich_Thuocs { get; set; }
        public DbSet<Kieu_Dang> kieu_Dangs { get; set; }
        public DbSet<Loai_Giay> loai_Giays { get; set; }
        public DbSet<Mau_Sac> mau_Sacs { get; set; }
        public DbSet<Mui_Giay> mui_Giays { get; set; }
        public DbSet<Phieu_Giam_Gia> phieu_Giam_Gias { get; set; }
        public DbSet<Phieu_Giam_Gia_Tai_Khoan> phieu_Giam_Gia_Tai_Khoans { get; set; }
        public DbSet<Phuong_Thuc_Thanh_Toan> phuong_Thuc_Thanh_Toans { get; set; }
        public DbSet<San_Pham> san_Phams { get; set; }
        public DbSet<San_Pham_Chi_Tiet> san_Pham_Chi_Tiets { get; set; }
        public DbSet<Tai_Khoan> tai_Khoans { get; set; }
        public DbSet<Tai_Khoan_Hoa_Don> tai_Khoan_Hoa_Dons { get; set; }
        public DbSet<Thanh_Toan> thanh_Toans { get; set; }
        public DbSet<Vai_Tro> vai_Tros { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
            

        }
       


    }
}
