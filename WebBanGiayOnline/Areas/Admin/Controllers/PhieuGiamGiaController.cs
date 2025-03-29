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
            // Bắt đầu với truy vấn của phiếu giảm giá
            var query = _context.phieu_Giam_Gias.AsQueryable();

            // Lọc theo ngày nếu có
            if (fromDate.HasValue)
            {
                query = query.Where(h => h.ngay_tao >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                query = query.Where(h => h.ngay_tao <= toDate.Value);
            }
            query = query.OrderBy(h => h.ngay_tao);
            if (fromDate >= toDate)
            {
                ViewBag.ThongBao = "❌ Ngày bắt đầu phải nhỏ hơn ngày kết thúc!";
            }

            // Lọc theo từ khóa (theo mã hoặc tên phiếu giảm giá)
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(p =>
                    p.ma.Contains(searchString) ||
                    p.ten_phieu_giam_gia.Contains(searchString));
            }

            if (Category.HasValue)
            {
                query = query.Where(h => h.trang_thai == Category);
            }

            // Đổ dữ liệu vào SelectList cho dropdown trạng thái
            ViewBag.TrangThaiList = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "Đã hết hạn" },
                new SelectListItem { Value = "1", Text = "Đang diễn ra" },

            }, "Value", "Text", Category?.ToString());





            // Phân trang:
            int pageNumber = page ?? 1;
            int pageSize = 5;

            // Cập nhật trạng thái cho từng phiếu
            var phieuList = query.ToList(); // Lấy danh sách thực tế từ database
            // Cập nhật trạng thái cho từng phiếu
            foreach (var phieu in phieuList)
            {
                phieu.UpdateTrangThai();
                _context.Entry(phieu).State = EntityState.Modified;
            }
            // Lưu thay đổi vào database
            _context.SaveChanges();

            // Chuyển đổi danh sách thành IPagedList để phân trang
            var pagedList = query.ToPagedList(pageNumber, pageSize);

            // Trả về View với Model là IPagedList<Phieu_Giam_Gia>
            return View("Index", pagedList);
        }


        // GET: KieuDangController/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieu_giam_gia = await _context.phieu_Giam_Gias
                .FirstOrDefaultAsync(m => m.ID == id);
            if (phieu_giam_gia == null)
            {
                return NotFound();
            }

            return View(phieu_giam_gia);
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
                _context.phieu_Giam_Gias.Add(phieu_giam_gia);
                await _context.SaveChangesAsync();

                if (customerIds != null && customerIds.Length > 0)
                {
                    var tai_khoan = _context.tai_Khoans.Where(c => customerIds.Contains(c.ID)).ToList();
                    Console.WriteLine($"Số lượng tài khoản được chọn: {tai_khoan.Count}");

                    if (phieu_giam_gia.kieu_giam_gia == 0)
                    {

                        foreach (var tai_khoans in tai_khoan)
                        {
                            Console.WriteLine($"Kiểm tra tài khoản: {tai_khoans?.ho_ten}, Email: {tai_khoans?.email}");

                            if (!string.IsNullOrEmpty(tai_khoans?.email))
                            {
                                await SendEmail(tai_khoans.email, phieu_giam_gia);
                            }
                            else
                            {
                                Console.WriteLine("Email trống hoặc null");
                            }
                            // Lưu thông tin liên kết voucher cá nhân cho khách hàng
                            var link = new Phieu_Giam_Gia_Tai_Khoan
                            {
                                ID = Guid.NewGuid(), // Nếu sử dụng composite key, bạn có thể cấu hình khác
                                Tai_KhoanID = tai_khoans.ID,
                                Phieu_Giam_GiaID = phieu_giam_gia.ID,
                                trang_thai = 0, // 0: active theo hệ thống của bạn
                                ngay_tao = DateTime.Now,
                                nguoi_tao = User.Identity?.Name ?? "admin"
                            };
                            _context.phieu_Giam_Gia_Tai_Khoans.Add(link);
                        }
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        // Nếu voucher công khai, bạn chỉ gửi email (hoặc xử lý theo yêu cầu)
                        foreach (var tk in tai_khoan)
                        {
                            Console.WriteLine($"Kiểm tra tài khoản: {tk?.ho_ten}, Email: {tk?.email}");
                            if (!string.IsNullOrEmpty(tk?.email))
                            {
                                await SendEmail(tk.email, phieu_giam_gia);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("customerIds trống hoặc null");
                }

                return RedirectToAction("Index");
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
