namespace WebBanGiay.Models
{
    public class HangHoaVM
    {
        public Guid? ID { get; set; }
        public string? TenHH { get; set; }
        public string? Hinh { get; set; }
        public double? DonGia { get; set; }
        public string? MoTa { get; set; }

     
    }
    public class ChiTietHangHoaVM
    {
        public Guid ID { get; set; }
        public int? MaHh { get; set; }
        public string TenHH { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public string? MoTa { get; set; }
      
        public string? ChiTiet { get; set; }
        public int? DiemDanhGia { get; set; }
        public int? SoLuongTon { get; set; }
    }
}
