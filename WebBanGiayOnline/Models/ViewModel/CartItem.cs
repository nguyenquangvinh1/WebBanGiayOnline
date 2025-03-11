namespace WebBanGiay.Models.ViewModel
{
    public class CartItem
    {
        public Guid id { get; set; }
        public int Mahh { get; set; }
        public string TenHH { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }

        public decimal ThanhTien => SoLuong * DonGia;
    }
}
