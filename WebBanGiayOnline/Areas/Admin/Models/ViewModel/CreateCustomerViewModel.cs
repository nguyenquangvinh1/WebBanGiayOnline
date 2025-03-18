
    namespace WebBanGiay.Areas.Admin.Models.ViewModel
{
    public class CreateCustomerViewModel
    {

        public Guid Id { get; set; }
        public string UserName { get; set; }

        public string? Makh { get; set; }
        public string? PassWord { get; set; }
        public string Ho_ten { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CCCD { get; set; }
        public string tinh { get; set; }
        public string huyen { get; set; }
        public string xa { get; set; }
        public string dia_chi { get; set; }


        public int loai_dia_chi { get; set; }

        public int Gender { get; set; }

        public string FullAddress => $"{dia_chi}, {xa}, {huyen}, {tinh}";

        public DateTime Createdate { get; set; }
    }
}
