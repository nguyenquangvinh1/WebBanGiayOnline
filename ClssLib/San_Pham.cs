using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class San_Pham
    {
        public Guid ID { get; set; }
        public string ten_san_pham { get; set; }
        public string mo_ta { get; set; }
        public int trang_thai { get; set; }
        public DateTime ngay_tao { get; set; }
        public DateTime ngay_sua { get; set; }
        public string nguoi_tao { get; set; }
        public string nguoi_sua { get; set; }

        [JsonIgnore]
        public virtual Kieu_Dang Kieu_Dang { get; set; }
        [JsonIgnore]
        public virtual Danh_Muc Danh_Muc { get; set; }
        [JsonIgnore]
        public virtual Loai_Giay Loai_Giay { get; set; }
        [JsonIgnore]
        public virtual Mui_Giay Mui_Giay { get; set; }
        [JsonIgnore]
        public virtual Co_Giay Co_Giay { get; set; }
        [JsonIgnore]
        public virtual De_Giay De_Giay { get; set; }
        [JsonIgnore]
        public virtual Chat_Lieu Chat_Lieu { get; set; }
        [JsonIgnore]
        public virtual ICollection<San_Pham_Chi_Tiet> San_Pham_Chi_Tiets { get; set; }
        [JsonIgnore]
        public virtual ICollection<Anh_San_Pham> Anh_San_Phams { get; set; }
    }
}
