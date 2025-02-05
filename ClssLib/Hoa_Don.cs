using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Tổng tiền đơn hàng :")]
        public decimal tong_tien { get; set; }
        [Display(Name = "Ghi chú :")]
        public string ghi_chu { get; set; }
        [Display(Name = "Trạng thái :")]
        public int trang_thai { get; set; }
        [Display(Name = "Địa chỉ :")]
        public string dia_chi { get; set; }
        [Display(Name = "Số điện thoại :")]
        public string sdt_nguoi_nhan { get; set; }
        [Display(Name = "Email người nhận :")]
        public string email_nguoi_nhan { get; set; }
        [Display(Name = "Loại hóa đơn :")]
        public int loai_hoa_don { get; set; }
        [Display(Name = "Người đặt :")]
        public string ten_nguoi_nhan { get; set; }
        [Display(Name = "Ngày nhận hàng :")]
        public DateTime thoi_gian_nhan_hang { get; set; }
        [Display(Name = "Ngày xuất hàng :")]
        public DateTime ngay_tao { get; set; }
        [Display(Name = "Ngày thay đổi :")]
        public DateTime ngay_sua { get; set; }
        [Display(Name = "Nhân viên :")]
        public string nguoi_tao { get; set; }
        [Display(Name = "Người sửa :")]
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
