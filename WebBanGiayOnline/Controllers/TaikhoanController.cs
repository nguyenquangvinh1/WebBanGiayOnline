using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using WebBanGiay.Data;
using ClssLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using WebBanGiay.Areas.Admin.Models.ViewModel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Net.Mail;
using System.Net;
using WebBanGiay.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace WebBanGiay.Controllers
{
    [AllowAnonymous]
    public class TaiKhoanController : Controller
    {
        private readonly AppDbContext _context;

        public TaiKhoanController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        public async Task<IActionResult> LoginAdmin(LoginViewmodel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View("Login", model);

            // Lấy thông tin người dùng từ database
            var user = _context.tai_Khoans
                .Include(u => u.Vai_Tro)
                .FirstOrDefault(u => u.user_name == model.Username);

            if (user == null || user.pass_word != model.Password)
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng.");
                return View("Login", model);
            }

            // Kiểm tra nếu URL chứa "/Admin"
            bool isAdminArea = !string.IsNullOrEmpty(returnUrl) && returnUrl.ToLower().Contains("/admin");

            // Kiểm tra vai trò người dùng
            bool isAdminOrEmployee = user.Vai_Tro.ten_vai_tro == "Admin" || user.Vai_Tro.ten_vai_tro == "Nhân Viên";
            bool isCustomer = user.Vai_Tro.ten_vai_tro == "Khách hàng";

            // Trường hợp vào trang Admin mà không có quyền Admin hoặc Nhân viên
            if (isAdminArea && !isAdminOrEmployee)
            {
                ModelState.AddModelError("", "Bạn không có quyền truy cập vào khu vực quản trị.");
                return View("Login", model);
            }

            // Đăng nhập thành công - Tạo claims
            var claims = new List<Claim>
{
    new Claim("userid", user.ID.ToString()),
        new Claim("ma", user.ma.ToString()),

    new Claim(ClaimTypes.Name, user.user_name),
    new Claim(ClaimTypes.Role, user.Vai_Tro.ten_vai_tro),
    new Claim(ClaimTypes.Email, user.email ?? "")
};

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            // Điều hướng dựa trên vai trò
            if (isAdminOrEmployee)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" }); // Chuyển hướng đến Admin
            }
            else if (isCustomer)
            {
                return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chủ khách hàng
            }

            return returnUrl != null ? Redirect(returnUrl) : RedirectToAction("Index", "Home");
        }







        //// Kiểm tra vai trò và điều hướng
        //if (user.Vai_Tro.ten_vai_tro == "Admin" || user.Vai_Tro.ten_vai_tro == "Nhân viên")
        //{
        //	var referrer = Request.Headers["Referer"].ToString();
        //	if (!referrer.Contains("/Admin"))
        //	{
        //		ModelState.AddModelError("", "Tài khoản này không thể đăng nhập ở đây. Vui lòng đăng nhập vào trang quản trị.");
        //		return View("Login", model);
        //	}

        //	return RedirectToAction("Index", "Home", new { area = "Admin" });
        //}

        //return RedirectToAction("Index", "Home");




        //    public IActionResult Login(bool isAdminLogin = false)
        //    {
        //        ViewBag.IsAdminLogin = isAdminLogin;
        //        return View();
        //    }

        //    [HttpPost]
        //    public async Task<IActionResult> LoginAdmin(LoginViewmodel model, bool isAdminLogin = false)
        //    {
        //        if (!ModelState.IsValid)
        //            return View(model);

        //        var user = _context.tai_Khoans
        //            .Include(u => u.Vai_Tro)
        //            .FirstOrDefault(u => u.user_name == model.Username);

        //        if (user == null || user.pass_word != model.Password)
        //        {
        //            ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng.");
        //            return View(model);
        //        }

        //        // Nếu đăng nhập ở trang bán hàng online nhưng tài khoản là Admin hoặc Nhân viên
        //        if (!isAdminLogin && (user.Vai_Tro.ten_vai_tro == "Admin" || user.Vai_Tro.ten_vai_tro == "Nhân viên"))
        //        {
        //            ModelState.AddModelError("", "Tài khoản này không thể đăng nhập ở đây. Vui lòng đăng nhập vào trang quản trị.");
        //            return View(model);
        //        }

        //        // Kiểm tra tài khoản khách hàng chưa có mật khẩu -> gửi link reset mật khẩu
        //        if (user.Vai_Tro != null && user.Vai_Tro.ten_vai_tro == "Khách hàng" && string.IsNullOrEmpty(user.pass_word))
        //        {
        //            if (!string.IsNullOrEmpty(user.email) && user.email.Contains("@"))
        //            {
        //                await SendResetPasswordLink(user);
        //                TempData["Message"] = "Tài khoản chưa có mật khẩu. Vui lòng kiểm tra email để đặt lại mật khẩu.";
        //                return RedirectToAction("Login");
        //            }

        //            ModelState.AddModelError("", "Email không hợp lệ. Không thể gửi link đặt lại mật khẩu.");
        //            return View(model);
        //        }

        //        // Lấy địa chỉ nếu có
        //        var diaChi = _context.dia_Chis.FirstOrDefault(dc => dc.Tai_KhoanID == user.ID);

        //        // Đăng nhập thành công
        //        var claims = new List<Claim>
        //{
        //    new Claim("userid", user.ID.ToString()),
        //    new Claim(ClaimTypes.Name, user.user_name),
        //    new Claim(ClaimTypes.Role, user.Vai_Tro.ten_vai_tro),
        //    new Claim(ClaimTypes.Email, user.email ?? ""),
        //    new Claim("Phone", user.sdt ?? "")
        //};

        //        // Kiểm tra địa chỉ không null trước khi thêm claim
        //        if (diaChi != null)
        //        {
        //            claims.Add(new Claim("Address", diaChi.dia_chi_chi_tiet ?? ""));
        //            claims.Add(new Claim("tinh", diaChi.tinh ?? ""));
        //            claims.Add(new Claim("huyen", diaChi.huyen ?? ""));
        //            claims.Add(new Claim("xa", diaChi.xa ?? ""));
        //        }

        //        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        //        // Điều hướng dựa trên vai trò và loại đăng nhập
        //        if (isAdminLogin && (user.Vai_Tro.ten_vai_tro == "Admin" || user.Vai_Tro.ten_vai_tro == "Nhân viên"))
        //        {
        //            return RedirectToAction("Index", "Home", new { area = "Admin" });
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }


        public async Task<IActionResult> Edit(Guid id)
        {
            var customer = await _context.tai_Khoans
                .Include(c => c.Dia_Chi)
                .FirstOrDefaultAsync(c => c.ID == id);

            if (customer == null) return NotFound();

            // Sắp xếp danh sách địa chỉ: địa chỉ mặc định lên đầu
            customer.Dia_Chi = customer.Dia_Chi
                .OrderBy(a => a.loai_dia_chi) // Mặc định (1) lên đầu, Phụ (2) xuống sau
                .ToList();

            return View(customer);
        }


        // POST: Update Customer Info
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer([FromBody] Tai_Khoan model)
        {
            if (model == null) return Json(new { success = false });

            var customer = await _context.tai_Khoans.FindAsync(model.ID);
            if (customer == null) return Json(new { success = false });

            customer.ho_ten = model.ho_ten;
            customer.email = model.email;
            customer.sdt = model.sdt;

            _context.tai_Khoans.Update(customer);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        private async Task<bool> SendResetPasswordLink(Tai_Khoan user)
        {
            try
            {
                // Tạo link đặt lại mật khẩu chứa email đã mã hóa (hoặc ID)
                string resetToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray()); // Tạo token ngẫu nhiên
                string resetLink = Url.Action("DatLaiMatKhau", "TaiKhoan", new { email = user.email, token = resetToken }, Request.Scheme);

                var mailMessage = new MailMessage("your-email@gmail.com", user.email);
                mailMessage.Subject = "Đặt lại mật khẩu tài khoản của bạn";
                mailMessage.Body = $"Xin chào {user.user_name},<br/>Vui lòng nhấp vào liên kết sau để đặt lại mật khẩu: <a href='{resetLink}'>Đặt lại mật khẩu</a>";
                mailMessage.IsBodyHtml = true;

                using (var smtp = new SmtpClient("smtp.gmail.com"))
                {
                    smtp.Port = 587;
                    smtp.Credentials = new NetworkCredential("your-email@gmail.com", "your-email-password");
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mailMessage);
                }

                return true; // Gửi thành công
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi gửi email: " + ex.Message);
                return false; // Gửi thất bại
            }
        }

        [HttpGet]
        public IActionResult DatLaiMatKhau(string email, string token)
        {
            ViewBag.Email = email;
            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DatLaiMatKhau(string email, string token, string newPassword)
        {
            var user = await _context.tai_Khoans.FirstOrDefaultAsync(tk => tk.email == email);

            if (user == null || string.IsNullOrEmpty(token))
            {
                ViewBag.Message = "Liên kết không hợp lệ hoặc đã hết hạn.";
                return View();
            }

            user.pass_word = newPassword;  // Nên mã hóa mật khẩu trước khi lưu
            _context.Update(user);
            await _context.SaveChangesAsync();

            ViewBag.Message = "Đặt lại mật khẩu thành công!";
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Kiểm tra trùng tên đăng nhập và email
            bool isExist = _context.tai_Khoans.Any(t => t.user_name == model.Username || t.email == model.Email);
            if (isExist)
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc email đã tồn tại");
                return View(model);
            }

            // ID vai trò khách hàng mặc định (cần kiểm tra lại trong bảng Vai_Tro)
            var vaiTroKhachHang = _context.vai_Tros.FirstOrDefault(v => v.ten_vai_tro == "Khách hàng");

            if (vaiTroKhachHang == null)
            {
                ModelState.AddModelError("", "Không tìm thấy vai trò 'Khách hàng'");
                return View(model);
            }

            // Tạo tài khoản mới với vai trò khách hàng mặc định
            var taiKhoanMoi = new Tai_Khoan
            {
                ID = Guid.NewGuid(),
                user_name = model.Username,
                pass_word = model.Password,
                email = model.Email,
                ho_ten = "Khách hàng mới",
                ma = "KH" + DateTime.Now.Ticks,
                ngay_sinh = DateTime.Now,
                gioi_tinh = 0,
                sdt = "Chưa cập nhật",
                trang_thai = 1,
                cccd = "Chưa cập nhật",
                hinh_anh = null,
                ngay_tao = DateTime.Now,
                Vai_TroID = vaiTroKhachHang.ID,
                ResetToken = null,
                TokenExpiry = null
            };

            _context.tai_Khoans.Add(taiKhoanMoi);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");


            // Gửi link đặt lại mật khẩu

            TempData["Message"] = "Đăng ký thành công.";
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Message"] = "Đăng xuất thành công!";
            return RedirectToAction("Login", "TaiKhoan");
        }
    }
}
