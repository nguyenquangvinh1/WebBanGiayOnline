namespace WebBanGiay.view_mode
{
    public class dia_chi_view_mode
    {
        public string tinh { get; set; }
        public string huyen { get; set; }
        public string xa { get; set; }

        public string fulldiachi => $"{xa},{huyen},{tinh}";
        public DateTime ngay_tao { get; set; }
        public DateTime ngay_sua { get; set; }

    }
}
