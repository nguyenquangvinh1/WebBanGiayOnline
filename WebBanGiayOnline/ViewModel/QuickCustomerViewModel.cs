
using System.ComponentModel.DataAnnotations;
namespace WebBanGiay.ViewModel
{
    public class QuickCustomerViewModel
    {
        [Required]
        public string Ho_ten { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        // Nếu không cần địa chỉ, bạn có thể bỏ các trường này hoặc để rỗng:
        public string Tinh { get; set; } = "";
        public string Huyen { get; set; } = "";
        public string Xa { get; set; } = "";
        public string Dia_chi_chi_tiet { get; set; } = "";
    }
}
