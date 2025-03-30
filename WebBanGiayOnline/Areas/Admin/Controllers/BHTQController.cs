using ClssLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebBanGiay.Data;
using WebBanGiay.ViewModel;

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
                    ghi_chu = "",

                    trang_thai = 0,
                    dia_chi = "",
                    sdt_nguoi_nhan = "",
                    email_nguoi_nhan = "",
                    loai_hoa_don = 1,
                    ten_nguoi_nhan = "",
                    thoi_gian_nhan_hang = DateTime.Now,
                    ngay_tao = DateTime.Now,
                    nguoi_tao = "system"
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

        [HttpPost]
        public IActionResult UpdateInvoiceItemQuantity([FromBody] UpdateInvoiceItemRequest req)
        {
            try
            {
                // Tìm chi tiết hóa đơn
                var chiTiet = _context.don_Chi_Tiets.FirstOrDefault(x => x.ID == req.chiTietId);
                if (chiTiet == null)
                    return Json(new { success = false, message = "Không tìm thấy chi tiết hóa đơn!" });

                // Tìm sản phẩm trong kho tương ứng
                var spCT = _context.san_Pham_Chi_Tiets.FirstOrDefault(sp => sp.ID == chiTiet.San_Pham_Chi_TietID);
                if (spCT == null)
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm trong kho!" });

                int oldQty = chiTiet.so_luong;
                int newQty = oldQty + req.delta; // req.delta có thể âm (giảm) hoặc dương (tăng)

                // Lấy hóa đơn chứa chi tiết này
                var invoice = _context.hoa_Dons.FirstOrDefault(h => h.ID == chiTiet.Hoa_DonID);
                if (invoice == null)
                    return Json(new { success = false, message = "Không tìm thấy hóa đơn chứa chi tiết này!" });

                if (newQty <= 0)
                {
                    // Nếu số lượng giảm xuống 0 hoặc âm, trả lại toàn bộ số lượng của chi tiết vào kho,
                    // sau đó xóa chi tiết khỏi hóa đơn.
                    spCT.so_luong += oldQty;
                    _context.san_Pham_Chi_Tiets.Update(spCT);
                    _context.don_Chi_Tiets.Remove(chiTiet);
                }
                else
                {
                    int diff = newQty - oldQty; // diff > 0: tăng; diff < 0: giảm
                    if (diff > 0)
                    {
                        // Nếu tăng, kiểm tra kho có đủ không
                        if (spCT.so_luong < diff)
                            return Json(new { success = false, message = "Kho không đủ hàng!" });
                        spCT.so_luong -= diff;
                    }
                    else if (diff < 0)
                    {
                        // Nếu giảm, cộng số lượng đã giảm (-diff) vào kho
                        spCT.so_luong += (-diff);
                    }

                    // Cập nhật chi tiết hóa đơn
                    chiTiet.so_luong = newQty;
                    chiTiet.thanh_tien = chiTiet.gia * newQty;
                    chiTiet.ngay_sua = DateTime.Now;
                    chiTiet.nguoi_sua = "system";
                    _context.don_Chi_Tiets.Update(chiTiet);
                    _context.san_Pham_Chi_Tiets.Update(spCT);
                }

                // Sau khi cập nhật/xóa chi tiết, tính lại tổng tiền của hóa đơn
                // Tính lại tổng tiền
                invoice.tong_tien = _context.don_Chi_Tiets
                    .Where(x => x.Hoa_DonID == invoice.ID)
                    .Sum(x => (double?)x.thanh_tien) ?? 0;

                _context.hoa_Dons.Update(invoice);

                _context.SaveChanges();

                // Nếu chi tiết đã bị xóa thì trả về newQty = 0 và newTotal = 0
                return Json(new { success = true, newQty = newQty > 0 ? newQty : 0, newTotal = newQty > 0 ? chiTiet.thanh_tien : 0 });
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

       


        //   [HttpPost]
        //public IActionResult RemoveInvoiceItem([FromBody] Guid chiTietId)
        //{
        //    try
        //    {
        //        var chiTiet = _context.don_Chi_Tiets.FirstOrDefault(x => x.ID == chiTietId);
        //        if (chiTiet == null)
        //            return Json(new { success = false, message = "Không tìm thấy chi tiết hóa đơn!" });

        //        // Trả lại số lượng vào kho
        //        var spCT = _context.san_Pham_Chi_Tiets.FirstOrDefault(sp => sp.ID == chiTiet.San_Pham_Chi_TietID);
        //        if (spCT != null)
        //        {
        //            spCT.so_luong += chiTiet.so_luong;
        //            _context.san_Pham_Chi_Tiets.Update(spCT);
        //        }

        //        _context.don_Chi_Tiets.Remove(chiTiet);
        //        _context.SaveChanges();

        //        return Json(new { success = true });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public IActionResult AddProductToInvoice([FromBody] AddProductDto request)
        //{
        //    // Lấy hóa đơn
        //    var invoice = _context.hoa_Dons.FirstOrDefault(h => h.ID == request.InvoiceId);
        //    if (invoice == null)
        //        return Json(new { success = false, message = "Không tìm thấy hóa đơn!" });

        //    // Lấy sản phẩm
        //    var productDetail = _context.san_Pham_Chi_Tiets.FirstOrDefault(p => p.ID == request.ProductId);
        //    if (productDetail == null)
        //        return Json(new { success = false, message = "Không tìm thấy sản phẩm!" });

        //    // Kiểm tra kho đủ không
        //    if (productDetail.so_luong < request.Quantity)
        //        return Json(new { success = false, message = "Kho không đủ hàng!" });

        //    // Tìm chi tiết hoá đơn hiện có
        //    var existing = _context.don_Chi_Tiets
        //        .FirstOrDefault(ct => ct.Hoa_DonID == request.InvoiceId
        //                           && ct.San_Pham_Chi_TietID == request.ProductId);

        //    if (existing != null)
        //    {
        //        // Cộng dồn
        //        existing.so_luong += request.Quantity;
        //        existing.thanh_tien = existing.gia * existing.so_luong;
        //    }
        //    else
        //    {
        //        // Tạo mới
        //        var newMa = "CT" + Guid.NewGuid().ToString().Substring(0, 8);
        //        var chiTiet = new Hoa_Don_Chi_Tiet
        //        {
        //            ID = Guid.NewGuid(),
        //            ma = newMa,
        //            Hoa_DonID = request.InvoiceId,
        //            San_Pham_Chi_TietID = request.ProductId,
        //            so_luong = request.Quantity,
        //            gia = productDetail.gia,
        //            thanh_tien = productDetail.gia * request.Quantity,
        //            ngay_tao = DateTime.Now,
        //            nguoi_tao = "system",
        //            trang_thai = 0
        //        };
        //        _context.don_Chi_Tiets.Add(chiTiet);
        //    }

        //    // Trừ số lượng trong kho
        //    productDetail.so_luong -= request.Quantity;

        //    // Lưu
        //    _context.SaveChanges();

        //    return Json(new { success = true, message = "Thêm sản phẩm thành công!" });
        //}


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
            // Lấy hóa đơn
            var invoice = _context.hoa_Dons.FirstOrDefault(h => h.ID == request.InvoiceId);
            if (invoice == null)
                return Json(new { success = false, message = "Không tìm thấy hóa đơn!" });

            // Lấy sản phẩm chi tiết
            var productDetail = _context.san_Pham_Chi_Tiets.FirstOrDefault(p => p.ID == request.ProductId);
            if (productDetail == null)
                return Json(new { success = false, message = "Không tìm thấy sản phẩm!" });

            // Kiểm tra số lượng kho
            if (productDetail.so_luong < request.Quantity)
                return Json(new { success = false, message = "Kho không đủ hàng!" });

            // Tìm chi tiết hóa đơn đã tồn tại với sản phẩm này
            var existing = _context.don_Chi_Tiets
                .FirstOrDefault(ct => ct.Hoa_DonID == request.InvoiceId
                                   && ct.San_Pham_Chi_TietID == request.ProductId);

            // Tính giá cuối cùng: nếu request.Price > 0 thì dùng, còn không dùng giá gốc của sản phẩm
            double finalPrice = request.Price > 0 ? request.Price : productDetail.gia;

            if (existing != null)
            {
                // Log giá trị hiện có để debug
                Console.WriteLine("Existing invoice detail found: Quantity = " + existing.so_luong);

                // Cộng dồn số lượng và cập nhật thành tiền
                existing.so_luong += request.Quantity;
                existing.thanh_tien = existing.gia * existing.so_luong;
                // Đánh dấu entity này là Modified
                _context.don_Chi_Tiets.Update(existing);
            }
            else
            {
                // Tạo chi tiết hóa đơn mới
                string newMa = "CT" + Guid.NewGuid().ToString().Substring(0, 8);
                var chiTiet = new Hoa_Don_Chi_Tiet
                {
                    ID = Guid.NewGuid(),
                    ma = newMa,
                    Hoa_DonID = request.InvoiceId,
                    San_Pham_Chi_TietID = request.ProductId,
                    so_luong = request.Quantity,
                    gia = finalPrice,
                    thanh_tien = finalPrice * request.Quantity,
                    ngay_tao = DateTime.Now,
                    nguoi_tao = "system",
                    trang_thai = 0
                };
                _context.don_Chi_Tiets.Add(chiTiet);
            }

            // Cập nhật số lượng trong kho
            productDetail.so_luong -= request.Quantity;
            _context.san_Pham_Chi_Tiets.Update(productDetail);

            // Cập nhật tổng tiền của hóa đơn
            invoice.tong_tien += finalPrice * request.Quantity;
            _context.hoa_Dons.Update(invoice);

            _context.SaveChanges();

            return Json(new { success = true, message = "Thêm sản phẩm thành công!" });
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

            // 4. Tính tổng tiền đã thanh toán
            var totalPaid = _context.thanh_Toans
                .Where(t => t.Hoa_DonID == model.Hoa_DonID)
                .Sum(x => (double?)x.so_tien_thanh_toan) ?? 0;

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

                double discountAmount = 0;
                // Nếu loại phiếu giảm giá là 1 => Giảm %
                if (voucher.loai_phieu_giam_gia == 1)
                {
                    discountAmount = invoice.tong_tien * (voucher.gia_tri_giam / 100.0);
                    if (voucher.so_tien_giam_toi_da.HasValue && discountAmount > voucher.so_tien_giam_toi_da.Value)
                        discountAmount = voucher.so_tien_giam_toi_da.Value;
                }
                // Nếu loại phiếu giảm giá là 0 => Giảm VND
                else if (voucher.loai_phieu_giam_gia == 0)
                {
                    discountAmount = voucher.gia_tri_giam;
                }

                // Giảm số lượng voucher
                voucher.so_luong -= 1;
                _context.phieu_Giam_Gias.Update(voucher);

                // Lưu voucher vào hóa đơn
                invoice.Giam_GiaID = voucher.ID;
                _context.hoa_Dons.Update(invoice);
                _context.SaveChanges();
                // Cập nhật tổng tiền hóa đơn (trừ số tiền giảm)
                double newTotal = invoice.tong_tien - discountAmount;
                if (newTotal < 0)
                    newTotal = 0;
                invoice.tong_tien = newTotal;
            }

            // 4. Đổi trạng thái hóa đơn thành 3 (Hoàn thành)
            invoice.trang_thai = 3;
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
