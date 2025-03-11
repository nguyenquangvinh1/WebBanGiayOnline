using ClssLib;
using Microsoft.AspNetCore.Mvc;
using WebBanGiay.Data;

namespace WebBanGiay.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BHTQController : Controller
    {
        private readonly AppDbContext _context;
        public BHTQController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        //them san pham vao hoa don
        [HttpPost]
        public IActionResult AddProductToInvoice(Guid invoiceId, Guid productId, int quantity)
        {
            // Lấy hóa đơn từ DbSet hoa_Dons (đã có)
            var invoice = _context.hoa_Dons.FirstOrDefault(h => h.ID == invoiceId);
            // Lấy sản phẩm chi tiết (để truy cập thuộc tính gia) từ DbSet San_Pham_Chi_Tiets
            var productDetail = _context.san_Pham_Chi_Tiets.FirstOrDefault(p => p.ID == productId);

            if (invoice == null || productDetail == null)
                return Json(new { success = false, message = "Không tìm thấy hóa đơn hoặc sản phẩm!" });

            var chiTiet = new Hoa_Don_Chi_Tiet
            {
                ID = Guid.NewGuid(),
                Hoa_DonID = invoiceId,
                San_Pham_Chi_TietID = productId,
                so_luong = quantity,
                gia = productDetail.gia // Lấy giá từ productDetail
            };

            // Sử dụng DbSet đúng tên được khai báo trong AppDbContext
            _context.don_Chi_Tiets.Add(chiTiet);
            _context.SaveChanges();

            return Json(new { success = true, message = "Thêm sản phẩm thành công!" });
        }
        
        //hien thi danh sach san pham trong hoa don

        [HttpGet]
        public IActionResult GetInvoiceDetails(Guid invoiceId)
        {
            var products = _context.don_Chi_Tiets
                .Where(c => c.Hoa_DonID == invoiceId)
                .Select(c => new {
                    c.ID,
                    c.San_Pham_Chi_Tiet.ten_SPCT,
                    c.so_luong,
                    c.gia,
                    c.thanh_tien
                }).ToList();

            return Json(new { success = true, data = products });
        }

        //thanh toan hoa don
        [HttpPost]
        public IActionResult CheckoutInvoice(Guid invoiceId)
        {
            var invoice = _context.hoa_Dons.FirstOrDefault(h => h.ID == invoiceId);

            if (invoice == null)
                return Json(new { success = false, message = "Không tìm thấy hóa đơn!" });

            invoice.trang_thai = 1; // 1: Đã thanh toán
            _context.SaveChanges();

            return Json(new { success = true, message = "Thanh toán thành công!" });
        }

        //huy hoa don
        [HttpPost]
        public IActionResult CancelInvoice(Guid invoiceId)
        {
            var invoice = _context.hoa_Dons.FirstOrDefault(h => h.ID == invoiceId);

            if (invoice == null)
                return Json(new { success = false, message = "Không tìm thấy hóa đơn!" });

            invoice.trang_thai = 2; // 2: Đã hủy
            _context.SaveChanges();

            return Json(new { success = true, message = "Hóa đơn đã bị hủy!" });
        }






    }
}
