using ClssLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using WebBanGiay.Data;
// Sử dụng MailKit
using MimeKit;

namespace WebBanGiay.Areas.Admin.Controllers
{

    [Area("Admin")] /*sfsdfsd*/
    public class PhieuGiamGiaController : Controller
    {
        // GET: CoGiayController
        private readonly AppDbContext _context;
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _port = 587;
        private readonly string _user = "datnguyen24102002@gmail.com"; // Thay bằng địa chỉ email của bạn
        private readonly string _password = "ruub cfwn grrs ukkz"; // Thay bằng mật khẩu ứng dụng của bạn
        private string connectionString = "YourConnectionStringHere";
        public PhieuGiamGiaController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString, DateTime? startDate, DateTime? endDate, string searchStrings, int? trangThai)
        {
            var phieuGiamGias = await _context.phieu_Giam_Gias.ToListAsync();



            // Cập nhật trạng thái dựa trên ngày hết hạn
            foreach (var phieu in phieuGiamGias)
            {
                phieu.UpdateTrangThai();
            }

            _context.SaveChanges(); // Lưu thay đổi nếu cần
            var phieuGiamGiass = from pg in _context.phieu_Giam_Gias
                                 select pg;

            if (!String.IsNullOrEmpty(searchString))
            {
                phieuGiamGiass = phieuGiamGiass.Where(pg => pg.ma.Contains(searchString) ||
                                                           pg.ten_phieu_giam_gia.Contains(searchString));
            }
            ViewData["SearchString"] = HttpContext.Request.Query["searchString"];

            return View(phieuGiamGias.ToList());
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


        //GET: KieuDangController/Create
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
                // Thêm phiếu giảm giá mới
                _context.phieu_Giam_Gias.Add(phieu_giam_gia);
                await _context.SaveChangesAsync();

                if (customerIds != null && customerIds.Length > 0)
                {
                    // Lấy danh sách tài khoản được chọn từ customerIds
                    var taiKhoanList = _context.tai_Khoans.Where(c => customerIds.Contains(c.ID)).ToList();
                    Console.WriteLine($"Số lượng tài khoản được chọn: {taiKhoanList.Count}");

                    // Nếu voucher là cá nhân (kieu_giam_gia == 0)
                    if (phieu_giam_gia.kieu_giam_gia == 0)
                    {
                        foreach (var tk in taiKhoanList)
                        {
                            Console.WriteLine($"Kiểm tra tài khoản: {tk?.ho_ten}, Email: {tk?.email}");

                            // Gửi email cho khách hàng (nếu có email)
                            if (!string.IsNullOrEmpty(tk?.email))
                            {
                                await SendEmail(tk.email, phieu_giam_gia);
                            }
                            else
                            {
                                Console.WriteLine("Email trống hoặc null");
                            }

                            // Lưu thông tin liên kết voucher cá nhân cho khách hàng
                            var link = new Phieu_Giam_Gia_Tai_Khoan
                            {
                                ID = Guid.NewGuid(), // Nếu sử dụng composite key, bạn có thể cấu hình khác
                                Tai_KhoanID = tk.ID,
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
                        foreach (var tk in taiKhoanList)
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

        [HttpGet]
        public IActionResult GetAvailableDiscounts(Guid? customerId, decimal orderTotal)
        {
            var now = DateTime.Now;
            var query = _context.phieu_Giam_Gias.Where(pg => pg.trang_thai == 0  // Thay 1 -> 0 nếu active là 0
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
            Console.WriteLine("Số phiếu giảm giá phù hợp: " + list.Count);

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



        // Thêm chức năng tìm kiếm theo mã hoặc tên

    }

}
