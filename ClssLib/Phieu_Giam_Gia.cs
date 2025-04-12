using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{

    public class Phieu_Giam_Gia
    {
        public Guid ID { get; set; }
        public string ma { get; set; }
        public string ten_phieu_giam_gia { get; set; }
        public int loai_phieu_giam_gia { get; set; }
        public int kieu_giam_gia { get; set; }
        public int gia_tri_giam { get; set; }
        public double? gia_tri_toi_thieu { get; set; }
        public int? so_tien_giam_toi_da { get; set; }
        public int so_luong { get; set; }
        public int trang_thai { get; set; }
        public DateTime ngay_bat_dau { get; set; }
        public DateTime? ngay_ket_thuc { get; set; }
        public DateTime ngay_tao { get; set; }
        public DateTime? ngay_sua { get; set; }
        public string nguoi_tao { get; set; }
        public string? nguoi_sua { get; set; }
        [JsonIgnore]

        public virtual ICollection<Phieu_Giam_Gia_Tai_Khoan>? Phieu_Giam_Gia_Tai_Khoans { get; set; }
        [JsonIgnore]
        public virtual ICollection<Hoa_Don>? Hoa_Dons { get; set; }
        public void UpdateTrangThai()
        {
            if (ngay_ket_thuc.HasValue && ngay_ket_thuc.Value < DateTime.Now)
            {
                trang_thai = 0; // Hết hạn
            }
            else
            {
                trang_thai = 1; // Còn hiệu lực
            }
        }
    }
}