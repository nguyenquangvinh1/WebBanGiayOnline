using System;
using System.ComponentModel.DataAnnotations;

namespace WebBanGiay.view_mode
{
    public class NhanVien_Model
    {
        public Guid Id { get; set; }
        public DateTime? ngay_sua { get; set; }

        public string? ma { get; set; }
        [Required(ErrorMessage = "Username không được để trống")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Username phải có từ 5 đến 50 ký tự")]
        [RegularExpression("^[a-zA-Z0-9_]+$", ErrorMessage = "Username chỉ được chứa chữ cái, số và dấu gạch dưới (_)")]
        public string user_name { get; set; }

        public string dia_chi_chi_tiet { get; set; }

        public string? pass_word { get; set; }

        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ và tên tối đa 100 ký tự")]
        [RegularExpression("^[A-Za-zÀ-ỹ\\s]{2,100}$", ErrorMessage = "Họ và tên chỉ được chứa chữ cái và dấu cách")]
        public string ho_ten { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(NhanVien_Model), "ValidateNgaySinh", ErrorMessage = "Nhân viên phải từ 18 tuổi trở lên")]
        public DateTime ngay_sinh { get; set; }

        [Required(ErrorMessage = "Giới tính không được để trống")]
        [Range(0, 2, ErrorMessage = "Giới tính không hợp lệ (0: Nam, 1: Nữ, 2: Khác)")]
        public int gioi_tinh { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression("^0[0-9]{9}$", ErrorMessage = "Số điện thoại phải có 10 chữ số và bắt đầu bằng 0")]
        public string sdt { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string email { get; set; }

        [Required(ErrorMessage = "Số CCCD không được để trống")]
        [RegularExpression("^(\\d{9}|\\d{12})$", ErrorMessage = "Số CCCD phải có 9 số hoặc 12 số")]
        public string cccd { get; set; }

        public int trang_thai { get; set; }

        [Required(ErrorMessage = "Tỉnh/Thành phố không được để trống")]
        public string tinh { get; set; }

        [Required(ErrorMessage = "Quận/Huyện không được để trống")]
        public string huyen { get; set; }

        [Required(ErrorMessage = "Xã/Phường không được để trống")]
        public string xa { get; set; }

        public string? hinh_anh { get; set; }

        public DateTime ngay_tao { get; set; }


        public string fulldiachi
        {
            get
            {
                var parts = new List<string>();
                if (!string.IsNullOrWhiteSpace(dia_chi_chi_tiet))
                {
                    parts.Add(dia_chi_chi_tiet);
                }
                if (!string.IsNullOrWhiteSpace(xa))
                {
                    parts.Add(xa);
                }
                if (!string.IsNullOrWhiteSpace(huyen))
                {
                    parts.Add(huyen);
                }
                if (!string.IsNullOrWhiteSpace(tinh))
                {
                    parts.Add(tinh);
                }
                return string.Join(", ", parts);
            }
        }

        //public DateTime ngay_tao { get; set; }
        //public DateTime? ngay_sua { get; set; }
        public Guid Vai_TroID { get; set; }

        // Hàm kiểm tra ngày sinh
        public static ValidationResult ValidateNgaySinh(DateTime ngaySinh, ValidationContext context)
        {
            if (ngaySinh > DateTime.Now)
            {
                return new ValidationResult("Ngày sinh không được lớn hơn ngày hiện tại");
            }
            if ((DateTime.Now.Year - ngaySinh.Year) < 18)
            {
                return new ValidationResult("Nhân viên phải từ 18 tuổi trở lên");
            }
            return ValidationResult.Success;
        }
    }
}