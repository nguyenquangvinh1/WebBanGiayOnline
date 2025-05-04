using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Hoa_Don_Chi_Tiet
    {
        public Guid ID { get; set; }
        public string? ma { get; set; }
        public string? tensp { get; set; }
        public double gia { get; set; }
        public int? trang_thai { get; set; }
        public double? thanh_tien { get; set; }
        public int so_luong { get; set; }

        public string? phuongThucthanhtoan { get; set; }
        public DateTime ngay_tao { get; set; }
        public DateTime? ngay_sua { get; set; }
        public string? nguoi_tao { get; set; }
        public string? nguoi_sua { get; set; }
        [ForeignKey("Hoa_Don")]
        public Guid? Hoa_DonID { get; set; }
        [ForeignKey("San_Pham_Chi_Tiet")]
        public Guid? San_Pham_Chi_TietID { get; set; }
        [JsonIgnore]
        public virtual Hoa_Don? Hoa_Don { get; set; }
        [JsonIgnore]
        public virtual San_Pham_Chi_Tiet San_Pham_Chi_Tiet { get; set; }
        [ForeignKey("Phuong_Thuc_Thanh_Toan")]
        public Guid? Phuong_Thuc_Thanh_ToanID { get; set; }
        [JsonIgnore]
        public virtual Phuong_Thuc_Thanh_Toan? Phuong_Thuc_Thanh_Toan { get; set; }


    }
}
