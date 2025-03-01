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

        public PhieuGiamGiaController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString)
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


        // GET: KieuDangController/Create
        [HttpGet]
        public IActionResult Create()
        {
          
            return View();
        }



        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("datnguyen24102002@gmail.com", _user));
            message.To.Add(new MailboxAddress("dat0358043034@gmail.com", toEmail));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            using (var client = new MailKit.Net.Smtp.SmtpClient()) // Sử dụng MailKit
            {
                try
                {
                    await client.ConnectAsync(_smtpServer, _port, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_user, _password);
                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    // Ghi log hoặc xử lý lỗi
                    Console.WriteLine($"Lỗi gửi email: {ex.Message}");
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
        }



        // POST: KieuDangController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Phieu_Giam_Gia phieu_giam_gia)
        {
            if (ModelState.IsValid)
            {
                phieu_giam_gia.ID = Guid.NewGuid();
                phieu_giam_gia.ngay_tao = DateTime.Now;
               
               
                _context.phieu_Giam_Gias.Add(phieu_giam_gia);
                await _context.SaveChangesAsync();
                // Lấy danh sách phiếu giảm giá và sắp xếp cho mới nhất lên đầu
                var phieuGiamGias = _context.phieu_Giam_Gias.OrderByDescending(pg => pg.ID).ToList();

                // Cập nhật lại danh sách trong ViewBag hoặc ViewModel
                ViewBag.phieu_Giam_Gias = phieuGiamGias;
                TempData["SuccessMessage"] = "Thêm phiếu giảm giá thành công!";
                // Gửi email cho khách hàng
                string toEmail = "dat0358043034@gmail.com"; // Địa chỉ email nhận
                string subject = "Xác Nhận Phiếu Giảm Giá";
                string base64Image = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUA...";
                string body = $@"
    <html>
        <body>
            <p>Chào bạn,</p>
            <p>Bạn đã nhận được phiếu giảm giá mới! Mã: {phieu_giam_gia.ma}</p>
            <img src='{base64Image}' alt='Phiếu Giảm Giá' style='max-width:100%; height:auto;' />
            <p>Chúc bạn tiết kiệm!</p>
        </body>
    </html>";

                phieu_giam_gia.ma = GenerateDiscountCode(); // Hàm tự sinh mã
                await SendEmailAsync(toEmail, subject, body);
                return RedirectToAction(nameof(Index));
            }
            return View(phieu_giam_gia);


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


    }
}
