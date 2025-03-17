namespace WebBanGiay.Models.ViewModel
{
    public class CartItem
    {
        public Guid id { get; set; }
        public string Mahh { get; set; }
        public string TenHH { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }

        public double ThanhTien => SoLuong * DonGia;
    }
}
