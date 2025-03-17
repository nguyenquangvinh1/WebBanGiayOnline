using ClssLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost]
        public IActionResult CreateInvoice()
        {
            try
            {
                var newInvoice = new Hoa_Don
                {
                    ID = Guid.NewGuid(),
                    MaHoaDon = GenerateNewHoaDonID(),
                    tong_tien = 0,
                    ghi_chu = "",             // Gán rỗng nếu chưa có ghi chú
                
                    trang_thai = 0,           // 0: Chờ xử lý
                    dia_chi = "",             // Gán rỗng nếu chưa có địa chỉ
                    sdt_nguoi_nhan = "",      // Gán rỗng
                    email_nguoi_nhan = "",    // Gán rỗng
                    loai_hoa_don = 0,         // Nếu có kiểu hóa đơn, gán mặc định
                    ten_nguoi_nhan = "",      // Gán rỗng
                    thoi_gian_nhan_hang = DateTime.Now,  // hoặc một giá trị mặc định
                    ngay_tao = DateTime.Now,
                    nguoi_tao = ""            // Gán rỗng nếu chưa có người tạo
                };

                _context.hoa_Dons.Add(newInvoice);
                _context.SaveChanges();

                return Json(new
                {
                    success = true,
                    invoiceId = newInvoice.ID,
                    message = "Tạo hóa đơn mới thành công!"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Lỗi khi tạo hóa đơn: " + ex.Message
                });
            }
        }
        //them san pham vao hoa don
        [HttpPost]
        public IActionResult AddProductToInvoice(Guid invoiceId, Guid productId, int quantity,float price)
        {
          

            var invoice = _context.hoa_Dons.FirstOrDefault(h => h.ID == invoiceId);
            
            var productDetail = _context.san_Pham_Chi_Tiets.FirstOrDefault(p => p.ID == productId);

            Console.WriteLine("InvoiceId = " + invoiceId);
            if (invoice == null)
            {
                Console.WriteLine("Invoice is null");
            }

            else
            {
                Console.WriteLine("Found invoice " + invoice.MaHoaDon);
            }


            if (productDetail == null)
            {
                Console.WriteLine("ProductDetail is null");
            }

            else
            { Console.WriteLine("Found product " + productDetail.ten_SPCT); }
                


            string newMa = "CT" + Guid.NewGuid().ToString().Substring(0, 8);

            var chiTiet = new Hoa_Don_Chi_Tiet
            {
                ID = Guid.NewGuid(),
                ma = newMa,
                Hoa_DonID = invoiceId,
                San_Pham_Chi_TietID = productId,
                so_luong = quantity,
                gia = productDetail.gia,
                thanh_tien = productDetail.gia * quantity,
                ngay_tao = DateTime.Now,
                nguoi_tao = "system",
                trang_thai = 0
            };

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

        [HttpPost]
        public JsonResult HuyDonHang(Guid? id)
        {
            var hoaDon = _context.hoa_Dons.FirstOrDefault(h => h.ID == id);
            if (hoaDon == null)
            {
                return Json(new { success = false, message = "Không tìm thấy đơn hàng!" });
            }


            if (hoaDon.trang_thai == 3 || hoaDon.trang_thai == 2) // Hoàn thành hoặc Đang giao hàng
            {
                return Json(new { success = false, message = "Không thể hủy đơn hàng đã hoàn thành hoặc đang giao hàng!" });
            }
            if (hoaDon.trang_thai == 4)
            {

                return Json(new { success = false, message = "Đơn hàng đã bị hủy, không thể thay đổi trạng thái!" });
            }
            hoaDon.tong_tien = 0;
            hoaDon.trang_thai = 4;



            _context.hoa_Dons.Update(hoaDon);
            _context.SaveChanges();
            Console.WriteLine($"Hủy đơn hàng: {hoaDon.ID} - Sau khi cập nhật: Tổng tiền = {hoaDon.tong_tien}");


            return Json(new { success = true });
        }
        private string GenerateNewHoaDonID()
        {
            // Tìm hóa đơn có ID lớn nhất
            var lastHoaDon = _context.hoa_Dons
                .OrderByDescending(h => h.MaHoaDon)
                .FirstOrDefault();


            int newID = 1;

            if (lastHoaDon != null)
            {

                string lastID = lastHoaDon.MaHoaDon.Replace("HD", "");
                if (int.TryParse(lastID, out int lastNumber))
                {
                    newID = lastNumber + 1;
                }
            }

            // Format lại thành dạng "HD001"
            return $"HD{newID:D5}"; // 5 chữ số, VD: HD00001
        }

      


        public IActionResult GetInvoiceById(Guid id)
        {
            var invoice = _context.hoa_Dons
                                  .Include(h => h.Hoa_Don_Chi_Tiets) // Lấy luôn chi tiết sản phẩm
                                  .FirstOrDefault(h => h.ID == id);

            if (invoice == null)
                return Json(new { success = false, message = "Không tìm thấy hóa đơn" });

            return Json(new
            {
                success = true,
                invoice = invoice
            });
        }





    }
}
