using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebBanGiay.Data;
using WebBanGiay.Areas.Admin.Models.ViewModel;
using WebBanGiay.Models.ViewModel;

namespace WebBanGiay.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        public AccountController(AppDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public IActionResult LoginAdmin()
        {
            return View("LoginAdmin"); 
        }

        [HttpPost]
        public async Task<IActionResult> LoginAdmin(LoginViewmodel model)
        {
            if (!ModelState.IsValid)
                return View("LoginAdmin", model);

            var user = _context.tai_Khoans
                .Include(u => u.Vai_Tro)
                .FirstOrDefault(u => u.user_name == model.Username);

            if (user == null || user.pass_word != model.Password)
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng.");
                return View("LoginAdmin", model);
            }

            bool isAdminOrEmployee = user.Vai_Tro.ten_vai_tro == "Admin" || user.Vai_Tro.ten_vai_tro == "Nhân Viên";
            if (!isAdminOrEmployee)
            {
                ModelState.AddModelError("", "Bạn không có quyền truy cập khu vực quản trị.");
                return View("LoginAdmin", model);
            }

            var diaChi = _context.dia_Chis.FirstOrDefault(dc => dc.Tai_KhoanID == user.ID);

            var claims = new List<Claim>
            {
                new Claim("userid", user.ID.ToString()),
                new Claim(ClaimTypes.Name, user.user_name),
                new Claim(ClaimTypes.Role, user.Vai_Tro.ten_vai_tro),
                new Claim(ClaimTypes.Email, user.email ?? ""),
                new Claim("ma", user.ma),
                new Claim("province", diaChi?.tinh ?? ""),
                new Claim("district", diaChi?.huyen ?? ""),
                new Claim("ward", diaChi?.xa ?? ""),
                new Claim("address", diaChi?.dia_chi_chi_tiet ?? "")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
    }
}
