using System.ComponentModel.DataAnnotations;

namespace WebBanGiay.Models.ViewModel
{
    public class CartItem
    {
        public Guid id { get; set; }
        public string Mahh { get; set; }
        public string TenHH { get; set; }
        public double DonGia { get; set; }
        public string? Hinh { get; set; }
        public int SoLuong { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
        public string? TenKhachHang { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [RegularExpression(@"^(0[3|5|7|8|9])+([0-9]{8})$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string Sdt { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn tỉnh/thành phố.")]
        public string tinh { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn quận/huyện.")]
        public string huyen { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn xã/phường.")]
        public string xa { get; set; }

        public string GhiChu { get; set; }
        public double ThanhTien => SoLuong * DonGia;
        public double ThanhTienGG { get; set; }



    }
}
