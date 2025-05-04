using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace WebBanGiay.ViewModel
{
    public class QuickCustomerViewModel
    {
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [JsonPropertyName("HoTen")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [JsonPropertyName("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [JsonPropertyName("Email")]
        public string Email { get; set; }
    }
}
