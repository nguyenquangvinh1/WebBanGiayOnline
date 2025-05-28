
using System.ComponentModel.DataAnnotations;
using WebBanGiay.view_mode;

namespace WebBanGiay.Areas.Admin.Models.ViewModel
{
    public class CreateCustomerViewModel
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Username không được để trống")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Username phải có từ 5 đến 50 ký tự")]
        [RegularExpression("^[a-zA-Z0-9_]+$", ErrorMessage = "Username chỉ được chứa chữ cái, số và dấu gạch dưới (_)")]
        public string UserName { get; set; }

        public string? Makh { get; set; }
        public string? PassWord { get; set; }

        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ và tên tối đa 100 ký tự")]
        [RegularExpression("^[A-Za-zÀ-ỹ\\s]{2,100}$", ErrorMessage = "Họ và tên chỉ được chứa chữ cái và dấu cách")]
        public string Ho_ten { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(NhanVien_Model), "ValidateNgaySinh", ErrorMessage = "Nhân viên phải từ 18 tuổi trở lên")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression("^0[0-9]{9}$", ErrorMessage = "Số điện thoại phải có 10 chữ số và bắt đầu bằng 0")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Số CCCD không được để trống")]
        [RegularExpression("^(\\d{9}|\\d{12})$", ErrorMessage = "Số CCCD phải có 9 số hoặc 12 số")]
        public string CCCD { get; set; }
        [Required(ErrorMessage = "Tỉnh/Thành phố không được để trống")]
        public string tinh { get; set; }
        [Required(ErrorMessage = "Quận/Huyện không được để trống")]

        public string huyen { get; set; }
        [Required(ErrorMessage = "Xã/Phường không được để trống")]
        public string xa { get; set; }
        [Required(ErrorMessage = "Địa chỉ chi tiết không được để trống")]

        public string dia_chi { get; set; }


        public int loai_dia_chi { get; set; }

        public int Gender { get; set; }

        public string FullAddress => $"{dia_chi}, {xa}, {huyen}, {tinh}";

        public DateTime Createdate { get; set; }
    }
}
