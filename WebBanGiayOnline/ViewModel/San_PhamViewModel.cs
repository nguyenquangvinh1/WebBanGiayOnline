using ClssLib;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanGiay.ViewModel
{
    public class San_PhamCTView
    {
        public Guid? ID { get; set; }
        public int gia { get; set; }
        public int so_luong { get; set; }
        public string imgUrl { get; set; }
        public int trang_thai { get; set; }
        public DateTime ngay_tao { get; set; }
        public string? Kich_Thuoc { get; set; }
        public string? Mau_Sac { get; set; }
        public string? Ten { get; set; }
    }
    public class San_PhamViewModel
    {
        public Guid ID { get; set; }
        public string ten_san_pham { get; set; }
        public string mo_ta { get; set; }
        public int? trang_thai { get; set; }
        public DateTime ngay_tao { get; set; }
        public Guid? Kieu_DangID { get; set; }
        public Guid? Danh_MucID { get; set; }
        public Guid? Loai_GiayID { get; set; }
        public Guid? Mui_GiayID { get; set; }
        public Guid? Co_GiayID { get; set; }
        public Guid? De_GiayID { get; set; }
        public Guid? Chat_LieuID { get; set; }
        public string ListCT;
    }
}
