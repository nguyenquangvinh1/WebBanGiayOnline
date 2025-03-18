using ClssLib;

namespace WebBanGiay.Areas.Admin.Models.ViewModel
{
    public class DiachiViewModel
    {
        public Guid? idDiachi { get; set; }

        public string tinh { get; set; }

        public string huyen { get; set; }

        public string xa { get; set; }

        public string diachicuthe { get; set; }

        public int loaidiachi { get; set; }

        public Guid Tai_KhoanID { get; set; } // ID tài khoản liên kết

        public string FullAddress => $"{diachicuthe}, {xa}, {huyen}, {tinh}";


    }
}
