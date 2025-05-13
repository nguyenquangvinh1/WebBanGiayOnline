using System.Diagnostics;
using ClssLib;
using DocumentFormat.OpenXml.Math;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebBanGiay.Data;
using WebBanGiay.Models.ViewModel;
using WebBanGiay.Service;
using WebBanGiay.ViewModel;
using System.Configuration;
using System.Configuration;
using System.Configuration;
using System.Configuration;

namespace WebBanGiay.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Policy = "AdminPolicy")]
    //[Authorize(Policy = "EmployeePolicy")]
    [Authorize(Policy = "AdminOrEmployeePolicy")]
    public class BHTQController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IGhnService _ghnService;
        public BHTQController(AppDbContext context, IGhnService ghnService)
        {
            _context = context;
            _ghnService = ghnService;
        }
        public IActionResult Index()
        {
            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus.ToList(), "ID", "ten_chat_lieu");
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays.ToList(), "ID", "ten_loai_co_giay");
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs.ToList(), "ID", "ten_danh_muc");
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays.ToList(), "ID", "ten_de_giay");
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays.ToList(), "ID", "ten_mui_giay");
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs.ToList(), "ID", "ten_kieu_dang");
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays.ToList(), "ID", "ten_loai_giay");
            return View();
        }

        [HttpPost]
        public IActionResult CreateInvoice()
        {
            try
            {
                string ma = User.FindFirst("ma")?.Value ?? "Unknown";

                
                var newInvoice = new Hoa_Don
                {
                    ID = Guid.NewGuid(),
                    MaHoaDon = GenerateNewHoaDonID(),
                    tong_tien = 0,
                    ghi_chu = "",
                    //(chờ thêm sản phẩm)
                    trang_thai = 6,
                    dia_chi = "Tại quầy",
                    sdt_nguoi_nhan = "",
                    email_nguoi_nhan = "",
                    loai_hoa_don = 1,
                    ten_nguoi_nhan = "",
                    thoi_gian_nhan_hang = DateTime.Now,
                    ngay_tao = DateTime.Now,
                    nguoi_tao = ma
                };

                _context.hoa_Dons.Add(newInvoice);
                _context.SaveChanges();

                return Json(new { success = true, invoiceId = newInvoice.ID,data = newInvoice, message = "Tạo hóa đơn mới thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi tạo hóa đơn: " + ex.Message });
            }
        }
        [HttpGet]
        public IActionResult GetHoaDonTaiQuay()
        {
            var invoices = _context.hoa_Dons
                .Where(hd => hd.trang_thai == 6
                             && hd.dia_chi == "Tại quầy"
                             && hd.loai_hoa_don == 1)  // chỉ lấy hóa đơn tại quầy
                .OrderBy(hd => hd.ngay_tao)
                .Select(hd => new {
                    hd.ID,
                    hd.MaHoaDon,
                    hd.ngay_tao,
                    hd.tong_tien,
                    // Hiển thị trạng thái dưới dạng chuỗi
                    TrangThai = "Chờ xử lí",
                    // Lấy thông tin khách hàng (giả sử mỗi hóa đơn có 1 khách hàng được liên kết)
                    KhachHang = hd.Tai_Khoan_Hoa_Dons
                                 .Select(tkhd => new {
                                     id = tkhd.Tai_Khoan != null ? tkhd.Tai_Khoan.ID : (Guid?)null,

                                     Ten = tkhd.Tai_Khoan != null ?tkhd.Tai_Khoan.ho_ten : "",

                                    Email=tkhd.Tai_Khoan != null ?tkhd.Tai_Khoan.email :"",
                                    PhoneNumber=tkhd.Tai_Khoan != null ? tkhd.Tai_Khoan.sdt:"",
                                    TenVaiTro=(tkhd.Tai_Khoan != null && tkhd.vai_tro !=null)?tkhd.Tai_Khoan.Vai_Tro.ten_vai_tro : tkhd.vai_tro,
                                    NgayTao=tkhd.Tai_Khoan != null ?tkhd.Tai_Khoan.ngay_tao : tkhd.ngay_tao


                                 })
                                 .FirstOrDefault(),

            // Lấy thông tin phiếu giảm giá nếu có
            PhieuGiamGia = hd.Giam_Gia != null ? new
                    {
                        hd.Giam_Gia.ma,
                        hd.Giam_Gia.ten_phieu_giam_gia,
                        hd.Giam_Gia.gia_tri_giam,
                        hd.Giam_Gia.so_tien_giam_toi_da,
                        hd.Giam_Gia.ngay_bat_dau,
                        hd.Giam_Gia.ngay_ket_thuc,
                        hd.Giam_Gia.trang_thai
                    } : null
                })
                .ToList();
            

            return Json(new { success = true, data = invoices });
        }
        //lưu khach hang vao taikhoanhoadon khi nhấn chọn khách hàng 
        [HttpPost]
        public IActionResult LinkCustomerToInvoice(Guid invoiceId, Guid customerId)
        {
            // 1. Tìm hóa đơn
            var invoice = _context.hoa_Dons.FirstOrDefault(h => h.ID == invoiceId);
            if (invoice == null)
            {
                return Json(new { success = false, message = "Không tìm thấy hóa đơn!" });
            }

            // 2. Tìm khách hàng
            var customer = _context.tai_Khoans
                .Include(tk => tk.Vai_Tro)
                .FirstOrDefault(t => t.ID == customerId);
            if (customer == null)
            {
                return Json(new { success = false, message = "Không tìm thấy khách hàng!" });
            }

            // 3. Kiểm tra vai trò khách hàng
            if (customer.Vai_Tro == null || customer.Vai_Tro.ten_vai_tro != "Khách hàng")
            {
                return Json(new { success = false, message = "Tài khoản này không phải khách hàng!" });
            }

            // 4. Xóa các liên kết cũ (nếu có) cho invoiceId này
            var oldLinks = _context.tai_Khoan_Hoa_Dons
                .Where(x => x.Hoa_DonID == invoiceId)
                .ToList();
            if (oldLinks.Any())
            {
                _context.tai_Khoan_Hoa_Dons.RemoveRange(oldLinks);
            }
            invoice.dia_chi = "Tại Quầy";
            invoice.email_nguoi_nhan = customer.email;
            invoice.ten_nguoi_nhan = customer.ho_ten;
            invoice.sdt_nguoi_nhan = customer.sdt;
            _context.hoa_Dons.Update(invoice);


            // 5. Tạo bản ghi liên kết mới
            var link = new Tai_Khoan_Hoa_Don
            {
                ID = Guid.NewGuid(),
                Tai_KhoanID = customerId,
                Hoa_DonID = invoiceId,
                ngay_tao = DateTime.Now,
                Ten = customer.ho_ten,           // Gán tên khách hàng
                vai_tro = customer.Vai_Tro?.ten_vai_tro // Gán vai trò
            };
            _context.tai_Khoan_Hoa_Dons.Add(link);

            _context.SaveChanges();

            return Json(new { success = true, message = "Đã gắn khách hàng vào hóa đơn thành công!" });
        }

       


        [HttpPost]
        public IActionResult GetInvoicesByIDs([FromBody] GetInvoicesParam param)
        {
            try
            {
                var data = _context.hoa_Dons
                        .Where(hd => param.invoiceIds.Contains(hd.ID))
                        .Select(hd => new {
                            hd.ID,
                            hd.MaHoaDon
                        }
                        ).ToList();
                return Json(new { success = true, data = data });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = ex });
            }
        }

        //hien thi danh sach san pham trong hoa don

        [HttpGet]
        public IActionResult GetInvoiceDetails(Guid invoiceId)
        {
           
            var products = _context.don_Chi_Tiets
                .Where(c => c.Hoa_DonID == invoiceId)
                .Select(c => new {
                    c.ID,
                    tenSP = c.San_Pham_Chi_Tiet.ten_SPCT,
                    soLuong = c.so_luong,
                    gia = c.gia,
                    thanhTien = c.thanh_tien,
                    anhUrl = c.San_Pham_Chi_Tiet
                    .Anh_San_Pham_San_Pham_Chi_Tiets
                    .Select(a => a.Anh_San_Pham.anh_url)
                    .FirstOrDefault() ?? "/img/default.jpg",
                    kichThuoc = c.San_Pham_Chi_Tiet.Kich_Thuoc.ten_kich_thuoc, // Lấy tên kích thước
                    mauSac = c.San_Pham_Chi_Tiet.Mau_Sac.ma_mau, // Lấy tên màu sắc
                    trangThai = c.San_Pham_Chi_Tiet.trang_thai == 1 ? "Hoạt động" : "Không hoạt động"
                })
                .ToList();

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

        // hủy đơn hàng
        [HttpPost]
        
        public JsonResult HuyDonHang(Guid? id)// Kiểm tra
        {
            var hoaDon = _context.hoa_Dons
                                      // Lấy thông tin sản phẩm
                                  .FirstOrDefault(h => h.ID == id);
            var lsthdct = _context.don_Chi_Tiets.Where(x => x.Hoa_DonID == id).ToList();
            if (lsthdct == null)
            {
                return Json(new { success = false, message = "Không tìm thấy đơn hàng!" });
            }
            foreach (
                var chitiets in lsthdct)
            {
                var spct = _context.san_Pham_Chi_Tiets.FirstOrDefault(sp => sp.ID == chitiets.San_Pham_Chi_TietID);

                if(spct != null)
                {
                    spct.so_luong += chitiets.so_luong;
                    _context.san_Pham_Chi_Tiets.Update(spct);

                }
                _context.don_Chi_Tiets.Remove(chitiets);
                Console.WriteLine("da xoa san pham" + spct.ID);
                
            }


            // Cập nhật trạng thái đơn hàng
            hoaDon.tong_tien = 0;
            hoaDon.trang_thai = 4; // Đã hủy
            _context.hoa_Dons.Update(hoaDon);

            // Lưu thay đổi
            _context.SaveChanges();

            return Json(new { success = true ,Message="Hủy đơn hàng thành công"});
        }

        // Phương thức để cập nhật số lượng sản phẩm chi tiết
        private void UpdateSanPhamChiTiet(IEnumerable<Hoa_Don_Chi_Tiet> hoaDonChiTiets)
        {
            foreach (var chiTiet in hoaDonChiTiets)
            {
                var sanPhamct = _context.san_Pham_Chi_Tiets.FirstOrDefault(sp => sp.ID == chiTiet.San_Pham_Chi_TietID);
                if (sanPhamct != null)
                {
                    sanPhamct.so_luong += chiTiet.so_luong; // Cộng số lượng sản phẩm vào kho
                    _context.san_Pham_Chi_Tiets.Update(sanPhamct); // Cập nhật lại sản phẩm chi tiết
                }
            }
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

        

        [HttpPost]
        public IActionResult UpdateInvoiceItemQuantity([FromBody] UpdateInvoiceItemRequest req)
        {
            try
            {
                // Lấy dòng chi tiết hóa đơn
                var invoiceItem = _context.don_Chi_Tiets.FirstOrDefault(x => x.ID == req.chiTietId);
                if (invoiceItem == null)
                    return Json(new { success = false, message = "Không tìm thấy chi tiết hóa đơn!" });

                // Lấy giá hiện tại của sản phẩm từ DB
                var productDetail = _context.san_Pham_Chi_Tiets.FirstOrDefault(p => p.ID == invoiceItem.San_Pham_Chi_TietID);
                if (productDetail == null)
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm!" });

                // So sánh giá: nếu giá của hóa đơn khác với giá hiện tại, tức sản phẩm đã thay đổi giá
                if (invoiceItem.gia != productDetail.gia)
                {
                    string priceChangeMessage = $"{productDetail.ten_SPCT} đã có sự thay đổi về giá từ {invoiceItem.gia:N0} thành {productDetail.gia:N0}.";

                    return Json(new
                    {
                        success = false,
                        priceChangeMessage = priceChangeMessage
                    });
                }

                // Nếu đang tăng số lượng, kiểm tra kho
                if (req.delta > 0)
                {
                    if (productDetail.so_luong < req.delta)
                        return Json(new { success = false, message = "Kho không đủ hàng để tăng số lượng!" });

                    // Trừ tồn kho tương ứng
                    productDetail.so_luong -= req.delta;
                    _context.san_Pham_Chi_Tiets.Update(productDetail);
                }
                else if (req.delta < 0)
                {
                    // Nếu giảm số lượng, cộng lại kho
                    productDetail.so_luong += (-req.delta);
                    _context.san_Pham_Chi_Tiets.Update(productDetail);
                }

                // Cập nhật số lượng và thành tiền của dòng chi tiết
                int newQty = invoiceItem.so_luong + req.delta;
                if (newQty <= 0)
                {
                    // Nếu số lượng giảm về 0 hoặc âm, xóa dòng
                    _context.don_Chi_Tiets.Remove(invoiceItem);
                    newQty = 0;
                }
                else
                {
                    invoiceItem.so_luong = newQty;
                    invoiceItem.thanh_tien = invoiceItem.gia * newQty;
                    _context.don_Chi_Tiets.Update(invoiceItem);
                }

                _context.SaveChanges();
                var invoice = _context.hoa_Dons.FirstOrDefault(x => x.ID == invoiceItem.Hoa_DonID);
                var listHDCT = _context.don_Chi_Tiets.Where(x => x.Hoa_DonID == invoice.ID).AsQueryable();

                invoice.tong_tien = listHDCT.Select(x => (double)x.thanh_tien).Sum();
                _context.hoa_Dons.Update(invoice);
                _context.SaveChanges();
                return Json(new { success = true, newQty = newQty, newTotal = invoiceItem.thanh_tien });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        // Model cho request cập nhật số lượng
        public class UpdateInvoiceItemRequest
        {
            public Guid chiTietId { get; set; }
            public int delta { get; set; } // +1 để tăng, -1 để giảm
        }

        // Model cho request cập nhật số lượng
        public class GetInvoicesParam
        {
            public List<Guid> invoiceIds { get; set; }
        }

        [HttpPost]
        public IActionResult RemoveInvoiceItem([FromBody] Guid chiTietId)
        {
            try
            {
                var chiTiet = _context.don_Chi_Tiets.FirstOrDefault(x => x.ID == chiTietId);
                if (chiTiet == null)
                    return Json(new { success = false, message = "Không tìm thấy chi tiết hóa đơn!" });

                // Trả lại số lượng vào kho
                var spCT = _context.san_Pham_Chi_Tiets.FirstOrDefault(sp => sp.ID == chiTiet.San_Pham_Chi_TietID);
                if (spCT != null)
                {
                    spCT.so_luong += chiTiet.so_luong;
                    _context.san_Pham_Chi_Tiets.Update(spCT);
                }

                _context.don_Chi_Tiets.Remove(chiTiet);
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

      
        [HttpPost]
        public IActionResult AddProductToInvoice([FromBody] AddProductDto request)
        {

            try
            {
                // Lấy hóa đơn hiện tại
                var invoice = _context.hoa_Dons.FirstOrDefault(h => h.ID == request.InvoiceId);
                if (invoice == null)
                    return Json(new { success = false, message = "Không tìm thấy hóa đơn!" });

                // Lấy sản phẩm chi tiết
                var productDetail = _context.san_Pham_Chi_Tiets.FirstOrDefault(p => p.ID == request.ProductId);
                if (productDetail == null)
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm!" });

                // Kiểm tra số lượng tồn kho
                if (productDetail.so_luong < request.Quantity)
                    return Json(new { success = false, message = "Kho không đủ hàng!" });

                // Tính giá cuối cùng: nếu request.Price > 0 thì dùng, nếu không thì dùng giá gốc
                double finalPrice = request.Price > 0 ? request.Price : productDetail.gia;

                string priceChangeMessage = null;

                // Tìm dòng chi tiết có cùng sản phẩm và có giá bằng với finalPrice
                var detailWithSamePrice = _context.don_Chi_Tiets
                    .FirstOrDefault(ct => ct.Hoa_DonID == request.InvoiceId
                                       && ct.San_Pham_Chi_TietID == request.ProductId
                                       && ct.gia == finalPrice);

                if (detailWithSamePrice != null)
                {
                    // Giá trùng: cộng dồn số lượng và cập nhật thành tiền
                    detailWithSamePrice.so_luong += request.Quantity;
                    detailWithSamePrice.thanh_tien = detailWithSamePrice.gia * detailWithSamePrice.so_luong;
                    _context.don_Chi_Tiets.Update(detailWithSamePrice);
                }
                else
                {
                    // Kiểm tra xem có dòng nào của sản phẩm này (với giá cũ) hay không
                    var existingAnyDetail = _context.don_Chi_Tiets
                        .FirstOrDefault(ct => ct.Hoa_DonID == request.InvoiceId
                                           && ct.San_Pham_Chi_TietID == request.ProductId);
                    if (existingAnyDetail != null)
                    {
                        // Có dòng tồn tại nhưng giá không khớp, báo thông báo thay đổi giá
                        priceChangeMessage = $"Sản phẩm <b>{productDetail.ten_SPCT}</b> đã có sự thay đổi về giá từ <b>{existingAnyDetail.gia:N0}</b> thành <b>{finalPrice:N0}</b>.";


                    }

                    // Tạo dòng chi tiết mới với giá mới
                    var newDetail = new Hoa_Don_Chi_Tiet
                    {
                        ID = Guid.NewGuid(),
                        ma = "CT" + Guid.NewGuid().ToString().Substring(0, 8),
                        Hoa_DonID = request.InvoiceId,
                        San_Pham_Chi_TietID = request.ProductId,
                        so_luong = request.Quantity,
                        gia = finalPrice,
                        thanh_tien = finalPrice * request.Quantity,
                        ngay_tao = DateTime.Now,

                        nguoi_tao = "system",  // Bạn có thể lấy thông tin người tạo từ Claims nếu cần
                        trang_thai = 0
                    };
                    _context.don_Chi_Tiets.Add(newDetail);
                }

                _context.SaveChanges();
                // Cập nhật số lượng tồn kho: trừ số lượng được bán
                productDetail.so_luong -= request.Quantity;
                _context.san_Pham_Chi_Tiets.Update(productDetail);

                var listHDCT = _context.don_Chi_Tiets.Where(x => x.Hoa_DonID == request.InvoiceId).AsQueryable();

                // Cập nhật tổng tiền hóa đơn
                invoice.tong_tien = listHDCT.Select(x => (double)x.thanh_tien).Sum();

                //invoice.tong_tien = _context.don_Chi_Tiets.Where(x => x.Hoa_DonID == request.InvoiceId).Sum(x => x.so_luong * x.gia);
                //List<Hoa_Don_Chi_Tiet> lst= _context.don_Chi_Tiets.Where(x=>x.)

                _context.hoa_Dons.Update(invoice);

                _context.SaveChanges();

                return Json(new { success = true, message = "Thêm sản phẩm thành công!", priceChangeMessage = priceChangeMessage });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }




        //lưu khach hang vao taikhoanhoadon khi nhấn chọn khách hàng 
        [HttpPost]
        public IActionResult get(Guid invoiceId, Guid customerId)
        {
            // 1. Tìm hóa đơn
            var invoice = _context.hoa_Dons.FirstOrDefault(h => h.ID == invoiceId);
            if (invoice == null)
            {
                return Json(new { success = false, message = "Không tìm thấy hóa đơn!" });
            }

            // 2. Tìm khách hàng
            var customer = _context.tai_Khoans
                .Include(tk => tk.Vai_Tro)
                .FirstOrDefault(t => t.ID == customerId);
            if (customer == null)
            {
                return Json(new { success = false, message = "Không tìm thấy khách hàng!" });
            }

            // 3. Kiểm tra vai trò khách hàng
            if (customer.Vai_Tro == null || customer.Vai_Tro.ten_vai_tro != "Khách hàng")
            {
                return Json(new { success = false, message = "Tài khoản này không phải khách hàng!" });
            }

            // 4. Xóa các liên kết cũ (nếu có) cho invoiceId này
            var oldLinks = _context.tai_Khoan_Hoa_Dons
                .Where(x => x.Hoa_DonID == invoiceId)
                .ToList();
            if (oldLinks.Any())
            {
                _context.tai_Khoan_Hoa_Dons.RemoveRange(oldLinks);
            }

            // 5. Tạo bản ghi liên kết mới
            var link = new Tai_Khoan_Hoa_Don
            {
                ID = Guid.NewGuid(),
                Tai_KhoanID = customerId,
                Hoa_DonID = invoiceId,
                ngay_tao = DateTime.Now,
                Ten = customer.ho_ten,           // Gán tên khách hàng
                vai_tro = customer.Vai_Tro?.ten_vai_tro // Gán vai trò
            };
            _context.tai_Khoan_Hoa_Dons.Add(link);

            _context.SaveChanges();

            return Json(new { success = true, message = "Đã gắn khách hàng vào hóa đơn thành công!" });
        }


      
        // DTO nhận request
        public class PaymentRequest
        {
            public Guid Hoa_DonID { get; set; }
            public Guid Phuong_Thuc_Thanh_ToanID { get; set; }
            public double SoTien { get; set; }
            public string MoTa { get; set; }
        }

        [HttpGet]
        public IActionResult GetPaymentMethods()
        {
            // Không có điều kiện lọc nào – trả về tất cả
            var methods = _context.phuong_Thuc_Thanh_Toans
                .Select(m => new {
                    id = m.ID,
                    ma = m.ma,
                    ten_phuong_thuc = m.ten_phuong_thuc
                })
                .ToList();

            return Json(new { success = true, data = methods });
        }


        [HttpPost]
        public IActionResult AddPayment([FromBody] PaymentRequest model)
        {
            // 1. Tìm hóa đơn
            var invoice = _context.hoa_Dons.FirstOrDefault(h => h.ID == model.Hoa_DonID);
            if (invoice == null)
            {
                return Json(new { success = false, message = "Không tìm thấy hóa đơn!" });
            }

            // 2. Tìm phương thức thanh toán
            var method = _context.phuong_Thuc_Thanh_Toans.FirstOrDefault(m => m.ID == model.Phuong_Thuc_Thanh_ToanID);
            if (method == null)
            {
                return Json(new { success = false, message = "Không tìm thấy phương thức thanh toán!" });
            }

            // 3. Tạo record thanh toán
            var newPayment = new Thanh_Toan
            {
                ID = Guid.NewGuid(),
                trang_thai = 0, // 0: Mới, 1: Hoàn tất, ...
                so_tien_thanh_toan = model.SoTien,
                ngay_thanh_toan = DateTime.Now,
                mo_ta = model.MoTa,
                Hoa_DonID = model.Hoa_DonID,
                Phuong_Thuc_Thanh_ToanID = model.Phuong_Thuc_Thanh_ToanID
            };

            _context.thanh_Toans.Add(newPayment);
            _context.SaveChanges();
            invoice.tong_tien = _context.don_Chi_Tiets.Where(c => c.Hoa_DonID == model.Hoa_DonID).Select(x => (double)x.thanh_tien).Sum();

            _context.SaveChanges();
            // 4. Tính tổng tiền đã thanh toán
            var totalPaid = _context.thanh_Toans
                .Where(t => t.Hoa_DonID == model.Hoa_DonID)
                .Sum(x => (double)x.so_tien_thanh_toan) ;


            // 5. Tính tiền còn thiếu (nếu bảng hoa_Dons có cột tong_tien)
            double remaining = (invoice.tong_tien - totalPaid);
            if (remaining < 0) remaining = 0;


            // (Tuỳ logic) Nếu remaining = 0 => cập nhật trạng thái hóa đơn
            if (remaining == 0)
            {
                invoice.trang_thai = 1; // 1: Đã thanh toán
                _context.hoa_Dons.Update(invoice);
                _context.SaveChanges();
            }


            return Json(new
            {
                success = true,
                message = "Thanh toán thành công!",
                data = new
                {
                    PaymentID = newPayment.ID,
                    SoTien = newPayment.so_tien_thanh_toan,
                    PhuongThuc = method.ten_phuong_thuc,
                    TotalPaid = totalPaid,
                    Remaining = remaining
                }
            });
        }

        [HttpPost]
        //[Route("GetShippingFee1")]
        public async Task<IActionResult> GetShippingFee1([FromBody] GHNShipping request)
        {
            try
            {
                var ship = await _ghnService.CalculateFeeAsync(request);
                if (request.provinceId != 201)
                {
                    ship = 50000;
                }
                if (request.subtotal > 2000000)
                {
                    ship = 0;
                }
				HttpContext.Session.SetInt32("ShippingFee", ship);
				return Json(new { success = true, ship });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi vận chuyển: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult FinalizePayment([FromBody] FinalizePaymentRequest model)
        {
            // 1. Tìm hóa đơn
            var invoice = _context.hoa_Dons.FirstOrDefault(h => h.ID == model.InvoiceId);
            if (invoice == null)
                return Json(new { success = false, message = "Không tìm thấy hóa đơn" });

            // 2. Lưu các dòng thanh toán
            if (model.PaymentRows != null && model.PaymentRows.Count > 0)
            {
                foreach (var row in model.PaymentRows)
                {
                    var payment = new Thanh_Toan
                    {
                        ID = Guid.NewGuid(),
                        so_tien_thanh_toan = row.Amount,
                        Phuong_Thuc_Thanh_ToanID = GetPaymentMethodIdByCode(row.Method),
                        Hoa_DonID = model.InvoiceId,
                        ngay_thanh_toan = DateTime.Now,
                        trang_thai = 0,
                        mo_ta = "Thanh toán"
                    };
                    _context.thanh_Toans.Add(payment);
                }
                _context.SaveChanges();
            }

            // 3. Nếu có Voucher (dựa vào VoucherCodeString)
            if (!string.IsNullOrWhiteSpace(model.VoucherCodeString))
            {
                // Tìm voucher theo mã (trường ma trong bảng phieu_Giam_Gias)
                var voucher = _context.phieu_Giam_Gias.FirstOrDefault(v => v.ma == model.VoucherCodeString);
                if (voucher == null)
                    return Json(new { success = false, message = "Không tìm thấy phiếu giảm giá!" });
                if (voucher.so_luong <= 0)
                    return Json(new { success = false, message = "Phiếu giảm giá đã hết!" });
                invoice.tong_tien = model.FinalTotal;

                
                // Nếu loại phiếu giảm giá là 1 => Giảm %
                
                // Nếu loại phiếu giảm giá là 0 => Giảm VND
                

                // Giảm số lượng voucher
                voucher.so_luong -= 1;
                _context.phieu_Giam_Gias.Update(voucher);

                // Lưu voucher vào hóa đơn
                invoice.Giam_GiaID = voucher.ID;
                _context.hoa_Dons.Update(invoice);
                _context.SaveChanges();
                // Cập nhật tổng tiền hóa đơn (trừ số tiền giảm)
                
            }

            // 4. Đổi trạng thái hóa đơn thành 6 (Đã thanh toán)
            invoice.trang_thai = 5;
            _context.hoa_Dons.Update(invoice);
            _context.SaveChanges();

            return Json(new { success = true, message = "Thanh toán thành công!" });
        }




        // Ví dụ hàm map Method -> ID
        private Guid GetPaymentMethodIdByCode(string method)
        {
            // Sử dụng ToUpper() để so sánh không phân biệt chữ hoa thường
            if (method.ToUpper() == "TTM")
            {
                return _context.phuong_Thuc_Thanh_Toans
                    .Where(p => p.ma.ToUpper() == "TTM")
                    .Select(p => p.ID)
                    .FirstOrDefault();
            }
            else if (method.ToUpper() == "CK")
            {
                return _context.phuong_Thuc_Thanh_Toans
                    .Where(p => p.ma.ToUpper() == "CK")
                    .Select(p => p.ID)
                    .FirstOrDefault();
            }
            else if (method.ToUpper() == "all" || method.ToUpper() == "CA2")
            {
                return _context.phuong_Thuc_Thanh_Toans
                    .Where(p => p.ma.ToUpper() == "all" || p.ma.ToUpper() == "CA2")
                    .Select(p => p.ID)
                    .FirstOrDefault();
            }
            return Guid.Empty;
        }


    }
}
