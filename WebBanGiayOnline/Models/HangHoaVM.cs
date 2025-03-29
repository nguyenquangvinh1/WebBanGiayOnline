using ClssLib;

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
        public string TenHH { get; set; }
        public List<string> Hinh { get; set; }
        public string? MoTa { get; set; }
        public string? TenLoai { get; set; }
        public string? ChiTiet { get; set; }
        public int? DiemDanhGia { get; set; }
        public List<SanPhamCT_VM> lstspct {  get; set; }
        public List<HangHoaVM> lstsp {  get; set; }
        
    }
    public class SanPhamCT_VM
    {
        public Guid ID { get; set; }
        public double Gia { get; set; }
        public int SoLuong { get; set; }
        public Guid? KichThuoc { get; set; }
        public Guid? MauSac { get; set; }
        public string? hinh { get; set; }
    }
}
