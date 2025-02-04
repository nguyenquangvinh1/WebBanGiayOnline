using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class Hoa_Don
    {
        public Guid ID { get; set; }
        public decimal tong_tien { get; set; }
        public string ghi_chu { get; set; }
        public int trang_thai { get; set; }
        public string dia_chi { get; set; }
        public string sdt_nguoi_nhan { get; set; }
        public string email_nguoi_nhan { get; set; }
        public int loai_hoa_don { get; set; }
        public string ten_nguoi_nhan { get; set; }
        public DateTime thoi_gian_nhan_hang { get; set; }
        public DateTime ngay_tao { get; set; }
        public DateTime ngay_sua { get; set; }
        public string nguoi_tao { get; set; }
        public string nguoi_sua { get; set; }
        [ForeignKey("Khach_Hang")]
        public Guid? Khach_HangID { get; set; }
        [ForeignKey("Nhan_Vien")]
        public Guid? Nhan_VienID { get; set; }
        [JsonIgnore]
        public virtual Khach_Hang? Khach_Hang { get; set; }
        [JsonIgnore]
        public virtual Nhan_Vien? Nhan_Vien { get; set; }
        [JsonIgnore]
        public virtual Phieu_Giam_Gia? Giam_Gia { get; set;}
        [JsonIgnore]
        public virtual ICollection<Thanh_Toan>? Thanh_Toans { get; set; }
        [JsonIgnore]
        public virtual ICollection<Hoa_Don_Chi_Tiet> Hoa_Don_Chi_Tiets { get; set; }

    }
}
