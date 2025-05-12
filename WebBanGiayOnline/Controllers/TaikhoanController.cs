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
using WebBanGiay.Models.ViewModel;


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


        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(Models.ViewModel.LoginViewmodel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View("Login", model);

            var user = _context.tai_Khoans
                .Include(u => u.Vai_Tro)
                .FirstOrDefault(u => u.user_name == model.Username);

            if (user == null || user.pass_word != model.Password)
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng.");
                return View("Login", model);
            }

            if (user.Vai_Tro.ten_vai_tro != "Khách hàng")
            {
                ModelState.AddModelError("", "Tài khoản này không thuộc vai trò khách hàng.");
                return View("Login", model);
            }

            var diaChi = _context.dia_Chis.FirstOrDefault(dc => dc.Tai_KhoanID == user.ID);

            var claims = new List<Claim>
            {
                new Claim("userid", user.ID.ToString()),
                new Claim("name", user.ho_ten),
                new Claim("email", user.email),
                new Claim("SDT", user.sdt),
                new Claim(ClaimTypes.Name, user.user_name),
                new Claim(ClaimTypes.Role, user.Vai_Tro.ten_vai_tro),
                new Claim(ClaimTypes.Email, user.email ?? ""),
                new Claim("province", CleanLocationName(diaChi?.tinh)),
                new Claim("district", diaChi?.huyen ?? ""),
                new Claim("ward", diaChi?.xa ?? ""),
                new Claim("address", diaChi?.dia_chi_chi_tiet ?? "")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }





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


        //---------Quên mật khẩu--------------//
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(QuenMatkhau model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.tai_Khoans.FirstOrDefault(u => u.email == model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email không tồn tại trong hệ thống.");
                return View(model);
            }

            // Tạo mã xác nhận hoặc token (ở đây dùng Guid đơn giản)
            var resetToken = Guid.NewGuid().ToString();

            // Lưu token vào bảng hoặc gửi trực tiếp qua email (ở đây giả sử bạn gửi link reset)
            // Gửi email (giả lập link)
            var resetLink = Url.Action("ResetPassword", "Account", new { email = model.Email, token = resetToken }, Request.Scheme);

            // TODO: Gửi email resetLink cho người dùng
            Console.WriteLine($"Gửi email đặt lại mật khẩu: {resetLink}");

            TempData["SuccessMessage"] = "Hướng dẫn đặt lại mật khẩu đã được gửi tới email của bạn.";
            return RedirectToAction("Login");
        }


        //---------Reset Password-------------//

        public IActionResult ResetPassword(string email, string token)
        {
            var model = new ResetPasswordViewModel
            {
                Email = email,
                Token = token
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.tai_Khoans.FirstOrDefault(u => u.email == model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Tài khoản không tồn tại.");
                return View(model);
            }

            // Bạn có thể kiểm tra token ở đây nếu bạn lưu trong DB
            // Trong ví dụ đơn giản này, ta giả lập là token luôn đúng

            user.pass_word = model.NewPassword; // Bạn nên mã hóa mật khẩu nếu có

            _context.tai_Khoans.Update(user);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Mật khẩu đã được đặt lại thành công.";
            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Models.ViewModel.RegisterViewModel model)
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

        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    TempData["Message"] = "Đăng xuất thành công!";
        //    return RedirectToAction("Login", "TaiKhoan");
        //}
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (role == "Admin" || role == "Nhân Viên")
                return RedirectToAction("LoginAdmin", "Account", new { area = "Admin" });

            return RedirectToAction("Login", "TaiKhoan");
        }


    }
}
