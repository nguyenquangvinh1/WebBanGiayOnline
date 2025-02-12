using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class San_Pham_Chi_Tiet
    {
        public Guid ID { get; set; }
        public string ma { get; set; }
        public decimal gia { get; set; }
        public int so_luong { get; set; }
        public int trang_thai { get; set; }
        [JsonIgnore]
        public virtual Kieu_Dang Kieu_Dang { get; set; }
        [JsonIgnore]
        public virtual Mau_Sac Mau_Sac { get; set; }
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
        public virtual Kich_Thuoc Kich_Thuoc { get; set; }
        [JsonIgnore]
        public virtual Chat_Lieu Chat_Lieu { get; set; }
        [JsonIgnore]
        public virtual ICollection<Anh_San_Pham> Anh_San_Phams { get; set; }

    }
}
