using WebBanGiay.Areas.Admin.Models.ViewModel;

namespace WebBanGiay.Models.ViewModel
{
    public class InforKhachhangViewmodel
    {
        public Guid? UserId { get; set; }

        public string? Ho_ten { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }


        //public string? Address { get; set; }
        //public string? ThanhPho { get; set; }
        //public string? QuanHuyen { get; set; }
        //public string? PhuongXa { get; set; }
        public List<DiachiViewModel> ListDiaChi { get; set; } = new List<DiachiViewModel>();
    }
}
