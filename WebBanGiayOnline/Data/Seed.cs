using ClssLib;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiay.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            List<Vai_Tro> vai_Tros = new List<Vai_Tro>()
            {
                new Vai_Tro{ID= Guid.NewGuid(),ten_vai_tro = "Admin",trang_thai = 1,ngay_tao = DateTime.Now},
                new Vai_Tro{ID= Guid.NewGuid(),ten_vai_tro = "Nhân Viên",trang_thai = 1,ngay_tao = DateTime.Now},
                new Vai_Tro{ID= Guid.NewGuid(),ten_vai_tro = "Khách hàng",trang_thai = 1,ngay_tao = DateTime.Now}
            };
            builder.Entity<Vai_Tro>().HasData(vai_Tros);
        }
    }
}
