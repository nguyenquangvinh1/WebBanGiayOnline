using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClssLib
{
    public class Phieu_Giam_Gia
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Mã phiếu giảm giá là bắt buộc")]
        [StringLength(50, ErrorMessage = "Mã không được vượt quá 50 ký tự")]
        public string ma { get; set; }

        [Required(ErrorMessage = "Tên phiếu giảm giá là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự")]
        public string ten_phieu_giam_gia { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Loại phiếu giảm giá không hợp lệ")]
        public int loai_phieu_giam_gia { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Kiểu giảm giá không hợp lệ")]
        public int kieu_giam_gia { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Giá trị giảm phải lớn hơn hoặc bằng 0")]
        public int gia_tri_giam { get; set; }

        /// <summary>
        /// Số tiền tối thiểu của đơn hàng để áp dụng mã giảm giá. Ví dụ: 300000 (tương đương 300.000 đ)
        /// </summary>
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Giá trị tối thiểu không hợp lệ")]
        public decimal? gia_tri_toi_thieu { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số tiền giảm tối đa không hợp lệ")]
        public int? so_tien_giam_toi_da { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn hoặc bằng 0")]
        public int so_luong { get; set; }

        [Range(0, 1, ErrorMessage = "Trạng thái không hợp lệ")]
        public int trang_thai { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc")]
        public DateTime ngay_bat_dau { get; set; }

        public DateTime? ngay_ket_thuc { get; set; }

        [Required(ErrorMessage = "Ngày tạo là bắt buộc")]
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

        /// <summary>
        /// Cập nhật trạng thái dựa trên ngày hết hạn
        /// </summary>
        public void UpdateTrangThai()
        {
            if (ngay_ket_thuc.HasValue && ngay_ket_thuc.Value < DateTime.Now)
            {
                trang_thai = 0; // Hết hạn
            }
            else
            {
                trang_thai = 1; // Còn hiệu lực
            }
        }




        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ngay_ket_thuc.HasValue && ngay_ket_thuc.Value < ngay_bat_dau)
            {
                yield return new ValidationResult("Ngày kết thúc không được nhỏ hơn ngày bắt đầu", new[] { nameof(ngay_ket_thuc) });
            }
        }
    }
}
