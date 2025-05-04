using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{
    public class San_Pham_Chi_Tiet
    {
        public Guid ID { get; set; }

        [Display(Name = "Tên Sản Phẩm :")]
        public string ten_SPCT { get; set; }
        [Display(Name = "Giá :")]
        public double gia { get; set; }
        [Display(Name = "Số Lượng :")]
        public int so_luong { get; set; }
        [Display(Name = "Trạng Thái :")]
        public string ma { get; set; }
        public int trang_thai { get; set; }
        public DateTime ngay_tao { get; set; }
        public DateTime? ngay_sua { get; set; }
        [ForeignKey("Kich_Thuoc")]
        [Display(Name = "Kích Thước :")]
        public Guid Kich_ThuocID { get; set; }
        [ForeignKey("Mau_Sac")]

        [Display(Name = "Màu Sắc :")]
        public Guid Mau_SacID { get; set; }

        [ForeignKey("San_Pham")]
        public Guid San_PhamID { get; set; }
        [Display(Name = "Kiểu Dáng :")]
        public Guid? Kieu_DangID { get; set; }
        [ForeignKey("Danh_Muc")]
        [Display(Name = "Danh mục :")]
        public Guid? Danh_MucID { get; set; }
        [ForeignKey("Loai_Giay")]
        [Display(Name = "Loại Giày :")]
        [Required]
        public Guid Loai_GiayID { get; set; }
        [ForeignKey("Mui_Giay")]
        [Display(Name = "Mũi Giày :")]
        public Guid? Mui_GiayID { get; set; }
        [ForeignKey("Co_Giay")]
        [Display(Name = "Cổ Giày :")]
        public Guid? Co_GiayID { get; set; }
        [ForeignKey("De_Giay")]
        [Display(Name = "Đế Giày :")]
        public Guid? De_GiayID { get; set; }
        [ForeignKey("Chat_Lieu")]
        [Display(Name = "Chất Liệu :")]
        [Required]
        public Guid Chat_LieuID { get; set; }
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
        [IgnoreDataMember]
        public virtual Kich_Thuoc? Kich_Thuoc { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Mau_Sac? Mau_Sac { get; set; }
        [JsonIgnore]
        public virtual San_Pham? San_Pham { get; set; }
        [JsonIgnore]
        public virtual ICollection<Anh_San_Pham_San_Pham_Chi_Tiet>? Anh_San_Pham_San_Pham_Chi_Tiets { get; set; }
        [JsonIgnore]
        public virtual ICollection<Hoa_Don_Chi_Tiet>? Hoa_Don_Chi_Tiets { get; set; }
    }
}
