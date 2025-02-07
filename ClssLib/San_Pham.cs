using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DateTime? ngay_sua { get; set; }
        public string nguoi_tao { get; set; }
        public string? nguoi_sua { get; set; }
        [ForeignKey("Kieu_Dang")]
        public Guid? Kieu_DangID { get; set; }
        [ForeignKey("Danh_Muc")]
        public Guid? Danh_MucID { get; set; }
        [ForeignKey("Loai_Giay")]
        public Guid? Loai_GiayID { get; set; }
        [ForeignKey("Mui_Giay")]
        public Guid? Mui_GiayID { get; set; }
        [ForeignKey("Co_Giay")]
        public Guid? Co_GiayID { get; set; }
        [ForeignKey("De_Giay")]
        public Guid? De_GiayID { get; set; }
        [ForeignKey("Chat_Lieu")]
        public Guid? Chat_LieuID { get; set; }
        [JsonIgnore]
        public virtual Kieu_Dang? Kieu_Dang { get; set; }
        [JsonIgnore]
        public virtual Danh_Muc? Danh_Muc { get; set; }
        [JsonIgnore]
        public virtual Loai_Giay? Loai_Giay { get; set; }
        [JsonIgnore]
        public virtual Mui_Giay? Mui_Giay { get; set; }
        [JsonIgnore]
        public virtual Co_Giay? Co_Giay { get; set; }
        [JsonIgnore]
        public virtual De_Giay? De_Giay { get; set; }
        [JsonIgnore]
        public virtual Chat_Lieu? Chat_Lieu { get; set; }
        [JsonIgnore]
        public virtual ICollection<San_Pham_Chi_Tiet>? San_Pham_Chi_Tiets { get; set; }
        [JsonIgnore]
        public virtual ICollection<Anh_San_Pham>? Anh_San_Phams { get; set; }
    }
}
