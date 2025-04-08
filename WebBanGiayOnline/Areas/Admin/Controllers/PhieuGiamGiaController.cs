using ClssLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using WebBanGiay.Data;
using X.PagedList.Extensions;

using X.PagedList.Mvc.Core;
// Sử dụng MailKit
using MimeKit;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocumentFormat.OpenXml.InkML;



namespace WebBanGiay.Areas.Admin.Controllers
{

    [Area("Admin")] /*sfsdfsd*/
    public class PhieuGiamGiaController : Controller
    {
        // GET: CoGiayController
        private readonly AppDbContext _context;

        public PhieuGiamGiaController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(
     int? page,
     string searchString,
     int? Category,
     DateTime? fromDate,
     DateTime? toDate,
     int? trangThai)
        {
            // Truy vấn danh sách phiếu giảm giá
            var query = _context.phieu_Giam_Gias.AsQueryable();

            // Kiểm tra ngày hợp lệ
            if (fromDate.HasValue && toDate.HasValue && fromDate >= toDate)
            {
                ViewBag.ThongBao = "❌ Ngày bắt đầu phải nhỏ hơn ngày kết thúc!";
            }

            // Lọc theo ngày nếu có
            if (fromDate.HasValue)
            {
                query = query.Where(h => h.ngay_tao >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                query = query.Where(h => h.ngay_tao <= toDate.Value);
            }

            // Lọc theo từ khóa (tìm theo mã hoặc tên)
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.ma.Contains(searchString) || p.ten_phieu_giam_gia.Contains(searchString));
            }

            // Lọc theo trạng thái nếu có
            if (Category.HasValue)
            {
                query = query.Where(h => h.trang_thai == Category);
            }

            // Chuyển về danh sách trước khi cập nhật trạng thái
            var danhSachPhieu = query.ToList();

            // Cập nhật trạng thái
            foreach (var phieu in danhSachPhieu)
            {
                phieu.UpdateTrangThai();
                _context.Entry(phieu).State = EntityState.Modified;
            }
            danhSachPhieu = danhSachPhieu
            .Where(h => h.ngay_tao != null)
            .OrderByDescending(h => h.ngay_tao)
            .ThenByDescending(h => h.ID)
            .ToList();
            _context.SaveChanges();

            // Sau khi cập nhật, thực hiện lại sắp xếp
            danhSachPhieu = danhSachPhieu.OrderByDescending(h => h.ngay_tao).ToList();

            // Đổ dữ liệu vào dropdown trạng thái
            ViewBag.TrangThaiList = new SelectList(new List<SelectListItem>
    {
        new SelectListItem { Value = "0", Text = "Đã hết hạn" },
        new SelectListItem { Value = "1", Text = "Đang diễn ra" }
    }, "Value", "Text", Category?.ToString());

            // Phân trang
            int pageNumber = page ?? 1;
            int pageSize = 5;
            var pagedList = danhSachPhieu.ToPagedList(pageNumber, pageSize);

            return View("Index", pagedList);
        }

        [HttpGet]
        public IActionResult Create()
        {

            var tai_khoan = _context.tai_Khoans.ToList();
            Console.WriteLine($"Số lượng tài khoản: {tai_khoan.Count}");
            ViewBag.tai_khoans = tai_khoan ?? new List<Tai_Khoan>();
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Phieu_Giam_Gia phieu_giam_gia, Guid[] customerIds)
        {
            Console.WriteLine($"Số lượng customerIds: {customerIds?.Length}");

            if (ModelState.IsValid)
            {
                // Cập nhật ngày tạo mới nhất để đảm bảo sắp xếp đúng
                phieu_giam_gia.ngay_tao = DateTime.Now;

                _context.phieu_Giam_Gias.Add(phieu_giam_gia);
                await _context.SaveChangesAsync();

                // Xử lý nếu có danh sách khách hàng
                if (customerIds != null && customerIds.Length > 0)
                {
                    var tai_khoan = _context.tai_Khoans.Where(c => customerIds.Contains(c.ID)).ToList();

                    if (phieu_giam_gia.kieu_giam_gia == 0) // Nếu là cá nhân
                    {
                        foreach (var tk in tai_khoan)
                        {
                            if (!string.IsNullOrEmpty(tk.email))
                            {
                                await SendEmail(tk.email, phieu_giam_gia);
                            }

                            var link = new Phieu_Giam_Gia_Tai_Khoan
                            {
                                ID = Guid.NewGuid(),
                                Tai_KhoanID = tk.ID,
                                Phieu_Giam_GiaID = phieu_giam_gia.ID,
                                trang_thai = 0,
                                ngay_tao = DateTime.Now,
                                nguoi_tao = User.Identity?.Name ?? "admin"
                            };
                            _context.phieu_Giam_Gia_Tai_Khoans.Add(link);
                        }
                        await _context.SaveChangesAsync();
                    }
                    else // Nếu là công khai, chỉ gửi email
                    {
                        foreach (var tk in tai_khoan)
                        {
                            if (!string.IsNullOrEmpty(tk.email))
                            {
                                await SendEmail(tk.email, phieu_giam_gia);
                            }
                        }
                    }
                }
                // Truy vấn danh sách phiếu giảm giá
                var query = _context.tai_Khoans.AsQueryable();
                // Lọc theo từ khóa (tìm theo mã hoặc tên)

                // 🔥 Đặt thông báo thành công
                TempData["SuccessMessage"] = "✅ Thêm thành công!";

                // 🔥 Điều hướng về trang đầu tiên sau khi thêm
                return RedirectToAction("Index", new { page = 1 });
            }

            ViewBag.tai_khoans = _context.tai_Khoans.ToList();
            return View(phieu_giam_gia);
        }

        private async Task SendEmail(string toEmail, Phieu_Giam_Gia phieu_giam_gia)
        {
            try
            {
                var fromEmail = new MailAddress("datnguyen24102002@gmail.com", "user");
                var toAddress = new MailAddress(toEmail);
                const string fromPassword = "btoz oley calg yjyc";
                string subject = "Ưu đãi đặc biệt dành cho bạn!";
                string body = $"Xin chào,\n\nBạn nhận được phiếu giảm giá: {phieu_giam_gia.ma} với giá trị: {phieu_giam_gia.gia_tri_giam}%!";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail.Address, fromPassword)
                };

                using (var message = new MailMessage(fromEmail, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                })
                {
                    await smtp.SendMailAsync(message);
                    Console.WriteLine($"Email đã gửi thành công tới: {toEmail}");
                }
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"Lỗi SMTP: {smtpEx.StatusCode} - {smtpEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi không xác định khi gửi email: {ex.Message}\n{ex.StackTrace}");
            }
        }
        private string GenerateDiscountCode()
        {
            // Logic để sinh mã ngẫu nhiên (VD: "DISCOUNT2023")
            return "DISCOUNT" + DateTime.Now.Year;
        }
        // GET: KieuDangController/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieu_giam_gia = await _context.phieu_Giam_Gias.FindAsync(id);
            if (phieu_giam_gia == null)
            {
                return NotFound();
            }
            return View(phieu_giam_gia);
        }

        // POST: KieuDangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Phieu_Giam_Gia phieu_giam_gia)
        {
            var co = await _context.phieu_Giam_Gias.FindAsync(phieu_giam_gia.ID);
            if (co == null)
                return NotFound();
            _context.Entry(phieu_giam_gia).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            var phieuGiamGia = _context.phieu_Giam_Gias.Find(id);
            if (phieuGiamGia != null)
            {
                _context.phieu_Giam_Gias.Remove(phieuGiamGia);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction(nameof(Index));
        }

        // Thêm chức năng lọc theo trạng thái
        public static IEnumerable<Phieu_Giam_Gia> FilterByStatus(IEnumerable<Phieu_Giam_Gia> vouchers, int status)
        {
            return vouchers.Where(v => v.trang_thai == status);
        }

        // Thêm chức năng tìm kiếm theo mã hoặc tên
        public static IEnumerable<Phieu_Giam_Gia> SearchByCodeOrName(IEnumerable<Phieu_Giam_Gia> vouchers, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return vouchers;

            searchTerm = searchTerm.ToLower();
            return vouchers.Where(v => v.ma.ToLower().Contains(searchTerm) || v.ten_phieu_giam_gia.ToLower().Contains(searchTerm));
        }

        [HttpGet]
        public IActionResult GetAvailableDiscounts(Guid? customerId, double orderTotal)
        {
            var now = DateTime.Now;
            var query = _context.phieu_Giam_Gias.Where(pg => pg.trang_thai == 1  // Thay 1 -> 0 nếu active là 0
                                                             && (pg.ngay_ket_thuc == null || pg.ngay_ket_thuc > now)
                                                             && pg.so_luong > 0
                                                             && (pg.gia_tri_toi_thieu == null || pg.gia_tri_toi_thieu <= orderTotal));

            if (customerId == null)
            {
                // Khách lẻ: chỉ lấy voucher công khai
                query = query.Where(pg => pg.kieu_giam_gia == 1);
            }
            else
            {
                // Nếu có khách hàng, lấy voucher công khai hoặc voucher cá nhân của họ
                var personalVoucherIds = _context.phieu_Giam_Gia_Tai_Khoans
                    .Where(x => x.Tai_KhoanID == customerId)
                    .Select(x => x.Phieu_Giam_GiaID)
                    .ToList();
                query = query.Where(pg => pg.kieu_giam_gia == 1 || (pg.kieu_giam_gia == 0 && personalVoucherIds.Contains(pg.ID)));
            }

            var list = query.ToList();
            //Console.WriteLine("Số phiếu giảm giá phù hợp: " + list.Count);

            foreach (var pg in list)
            {
                pg.UpdateTrangThai();
            }
            _context.SaveChanges();

            var result = list.Select(pg => new {
                id = pg.ID,
                ma = pg.ma,
                ten = pg.ten_phieu_giam_gia,
                loai = pg.loai_phieu_giam_gia,  // 0 => %, 1 => VND
                kieu = pg.kieu_giam_gia,         // 0 => cá nhân, 1 => công khai
                gia_tri = pg.gia_tri_giam,
                so_tien_giam_toi_da = pg.so_tien_giam_toi_da,
            });
            return Json(result);
        }
    }
}
