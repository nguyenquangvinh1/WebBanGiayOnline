using System.ComponentModel.DataAnnotations;
using WebBanGiay.Areas.Admin.Models.ViewModel;

namespace WebBanGiay.Models.ViewModel
{
    public class UpdateAccountViewModel
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public DiaChiViewModel DiaChi { get; set; } = new DiaChiViewModel();
    }
    public class DiaChiViewModel
    {
        public string? Tinh { get; set; }
        public string? Huyen { get; set; }
        public string? Xa { get; set; }
        public string? Diachicuthe { get; set; }

        public int loai_dia_chi { get; set; } // 1: mặc định, 2: phụ

    }

}

