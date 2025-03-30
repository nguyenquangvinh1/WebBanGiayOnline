using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClssLib
{

    public class Phieu_Giam_Gia
    {
        public Guid ID { get; set; }


        public string ma { get; set; }

        [Required(ErrorMessage = "Tên phiếu giảm giá không được để trống")]
        [MaxLength(50, ErrorMessage = "Tên phiếu giảm giá không được vượt quá 50 ký tự")]
        public string ten_phieu_giam_gia { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại phiếu giảm giá")]
        public int loai_phieu_giam_gia { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn kiểu áp dụng")]
        public int kieu_giam_gia { get; set; }

        [Required(ErrorMessage = "Giá trị không được để trống")]
        [CustomValidation(typeof(Phieu_Giam_Gia), nameof(ValidateGiaTriGiam))]
        public double gia_tri_giam { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá trị tối thiểu phải lớn hơn hoặc bằng 0")]
        public double? gia_tri_toi_thieu { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Số tiền giảm tối đa phải lớn hơn hoặc bằng 0")]
        public double? so_tien_giam_toi_da { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải là số nguyên dương")]
        public int so_luong { get; set; }


        public int trang_thai { get; set; }

        [Required(ErrorMessage = "Từ ngày không được để trống")]
        public DateTime ngay_bat_dau { get; set; }
        [Required(ErrorMessage = "Đến ngày không được để trống")]
        [DateGreaterThan("ngay_bat_dau", ErrorMessage = "Đến ngày phải lớn hơn Từ ngày")]
        public DateTime? ngay_ket_thuc { get; set; }

        public DateTime ngay_tao { get; set; }

        public DateTime? ngay_sua { get; set; }

        [Required(ErrorMessage = "Người tạo là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên người tạo không được vượt quá 50 ký tự")]
        public string nguoi_tao { get; set; }

        [StringLength(50, ErrorMessage = "Tên người sửa không được vượt quá 50 ký tự")]
        public string? nguoi_sua { get; set; }

        [JsonIgnore]
        public virtual ICollection<Phieu_Giam_Gia_Tai_Khoan>? Phieu_Giam_Gia_Tai_Khoans { get; set; }

        [JsonIgnore]
        public virtual ICollection<Hoa_Don>? Hoa_Dons { get; set; }

        public void UpdateTrangThai()
        {
            if (ngay_bat_dau <= DateTime.Now && (ngay_ket_thuc == null || ngay_ket_thuc >= DateTime.Now))
            {
                trang_thai = 1; // Đang diễn ra
            }
            else
            {
                trang_thai = 0; // Hết hạn
            }
        }
        public static ValidationResult ValidateGiaTriGiam(int giaTriGiam, ValidationContext context)
        {
            var instance = (Phieu_Giam_Gia)context.ObjectInstance;

            if (instance.loai_phieu_giam_gia == 1 && (giaTriGiam < 1 || giaTriGiam > 100))
            {
                return new ValidationResult("Giá trị phải nằm trong khoảng 1 đến 100 nếu là %");
            }

            if (instance.loai_phieu_giam_gia == 0 && giaTriGiam <= 0)
            {
                return new ValidationResult("Giá trị phải lớn hơn 0 nếu là số tiền cố định");
            }

            return ValidationResult.Success;
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ngay_ket_thuc.HasValue && ngay_ket_thuc.Value < ngay_bat_dau)
            {
                yield return new ValidationResult("Ngày kết thúc không được nhỏ hơn ngày bắt đầu", new[] { nameof(ngay_ket_thuc) });
            }
        }
    }
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var currentValue = (DateTime)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                return new ValidationResult($"Unknown property: {_comparisonProperty}");
            }

            var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if (currentValue <= comparisonValue)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}