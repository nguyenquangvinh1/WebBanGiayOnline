namespace WebBanGiay.Areas.Admin.Models.ViewModel
{
    public class EditCustomerViewModel
    {
        public Guid idCustomer { get; set; }

        public string hoten { get; set; }

        public string phone { get; set; }

        public string email { get; set; }

        public List<DiachiViewModel> ListDiaChi { get; set; } = new List<DiachiViewModel>();

    }
}
