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

		[Display(Name = "Mã Giảm giá :")]
		[Required]
		public string ma { get; set; }

		[Display(Name = "Tên phiếu giảm giá :")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Hãy nhập tên phiếu giảm giá!")]
		public string ten_phieu_giam_gia { get; set; }

		[Display(Name = "Loại giảm giá :")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Hãy Chọn loại giảm giá!")]
		public int loai_phieu_giam_gia { get; set; }

		[Display(Name = "Kiểu giảm giá :")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Hãy Chọn kiểu giảm giá!")]
		public int kieu_giam_gia { get; set; }

		[Display(Name = "Giá trị giảm :")]
        [Range(0,1000000000, ErrorMessage ="Giá trị giảm phải lớn hơn 0!")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Hãy điền giá trị giảm!")]
		public int gia_tri_giam { get; set; }

		[Display(Name = "Giá trị giảm tối thiểu :")]
		[Range(0, 1000000000, ErrorMessage = "Giá trị giảm tối thiểu phải lớn hơn 0!")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Hãy điền giá trị giảm tối thiểu!")]
		public double? gia_tri_toi_thieu { get; set; }

		[Display(Name = "Số tiền giảm tối đa :")]
		[Range(0, 1000000000, ErrorMessage = "Số tiền giảm tối đa phải lớn hơn 0!")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Hãy điền số tiền giảm tối đa!")]
		public int? so_tien_giam_toi_da { get; set; }

		[Display(Name = "Số lượng :")]
		[Range(0, 1000000000, ErrorMessage = "Số lượng phải lớn hơn 0!")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Hãy điền số lượng!")]
		public int so_luong { get; set; }

		[Display(Name = "Trạng Thái :")]
		public int trang_thai { get; set; }

		[Display(Name = "Ngày bắt đầu :")]
		[DataType(DataType.Date)]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Hãy chọn ngày bắt đầu!")]
		public DateTime? ngay_bat_dau { get; set; }

        [DataType(DataType.Date)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Hãy chọn ngày kết thúc!")]
		public DateTime? ngay_ket_thuc { get; set; }
        public DateTime ngay_tao { get; set; }
        public DateTime? ngay_sua { get; set; }
        public string nguoi_tao { get; set; }
        public string? nguoi_sua { get; set; }
        [JsonIgnore]

        public virtual ICollection<Phieu_Giam_Gia_Tai_Khoan>? Phieu_Giam_Gia_Tai_Khoans { get; set; }
        [JsonIgnore]
        public virtual ICollection<Hoa_Don>? Hoa_Dons { get; set; }
        public void UpdateTrangThai()
        {
            if (ngay_ket_thuc.HasValue && ngay_ket_thuc.Value < DateTime.Now)
            {
                trang_thai = 0; // Hết hạn
            }
            else if (ngay_bat_dau.HasValue && ngay_bat_dau.Value > DateTime.Now)
            {
                trang_thai = -1; // Chưa đến thời gian áp dụng
            }
            else
            {
                trang_thai = 1; // Đang hiệu lực
            }
        }

    }
}