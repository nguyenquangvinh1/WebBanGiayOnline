

using ClssLib;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanGiay.ViewModel
{
    public class San_PhamCTView
    {
        public Guid? ID { get; set; }
        public double gia { get; set; }
        public int so_luong { get; set; }
        public string? imgUrl { get; set; }
        public int? trang_thai { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string? Kich_Thuoc { get; set; }
        public string? Mau_Sac { get; set; }
        public string? Ten { get; set; }
        public string? Kieu_Dang { get; set; }
        public string? Danh_Muc { get; set; }
        public string? Loai_Giay { get; set; }
        public string? Mui_Giay { get; set; }
        public string? Co_Giay { get; set; }
        public string? De_Giay { get; set; }
        public string? Chat_Lieu { get; set; }
    }
    public class San_PhamViewModel
    {
        public Guid ID { get; set; }
        [Display(Name = "Tên Sản Phẩm :")]
        public string ten_san_pham { get; set; }
        [Display(Name = "Mô Tả :")]
        public string mo_ta { get; set; }
        [Display(Name = "Trạng Thái :")]
        public int? trang_thai { get; set; }
        [Display(Name = "Ngày Tạo :")]
        public DateTime ngay_tao { get; set; }
        [Display(Name = "Số lượng :")]
        public int? so_luong_tong { get; set; }

    }
}
