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

        public string? fulldiachi { get; set; }

        public string?   Sdt { get; set; }
   
        public string? Email { get; set; }

        public string? GhiChu { get; set; }

       

    }
}
