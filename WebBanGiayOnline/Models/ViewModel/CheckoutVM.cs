using System.ComponentModel.DataAnnotations;

namespace WebBanGiay.Models.ViewModel
{
    public class CheckoutVM
    {
        public bool GiongKhachHang { get; set; }
  
        public string? TenKhachHang { get; set; }
         public string? MaHoaDon { get; set; }


        public string tinh { get; set; }
  
        public string huyen { get; set; }

        public string xa { get; set; }

        private string? _fulldiachi;
        public string fulldiachi
        {
            get
            {
                // Tạo phần đuôi địa chỉ
                var diaChiHanhChinh = string.Join(", ", new[] { xa, huyen, tinh }.Where(s => !string.IsNullOrWhiteSpace(s)));

             
                if (string.IsNullOrWhiteSpace(_fulldiachi))
                    return diaChiHanhChinh;

                // Nếu người dùng có nhập tay → gắn thêm phần hành chính
                return $"{_fulldiachi}, {diaChiHanhChinh}";
            }
            set
            {
                _fulldiachi = value;
            }
        }
        public string?   Sdt { get; set; }
   
        public string? Email { get; set; }

        public string? GhiChu { get; set; }

       

    }
}
