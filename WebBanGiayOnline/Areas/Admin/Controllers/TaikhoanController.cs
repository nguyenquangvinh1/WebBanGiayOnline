//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using WebBanGiay.Data;
//using ClssLib;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
//using WebBanGiay.Areas.Admin.Models.ViewModel;

//namespace WebBanGiay.Controllers
//{
//    [Area("Admin")]
//    public class TaiKhoanController : Controller
//    {
//        private readonly AppDbContext _context;

//        public TaiKhoanController(AppDbContext context)
//        {
//            _context = context;
//        }


//        public async Task<IActionResult> DangNhap()
//        {
//            return View();
//        }


//        [HttpPost]
//        public async Task<IActionResult> DangNhap(LoginViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }


//            var taiKhoan = _context.tai_Khoans
//                .Include(t => t.Vai_Tro) // Đảm bảo lấy Vai_Tro cho việc phân quyền
//                .FirstOrDefault(t => t.user_name == model.Username && t.pass_word == model.Password);

//            if (taiKhoan == null)
//            {

//                model.ErrorMessage = "Tên hoặc mật khẩu không đúng!";
//                return View(model);
//            }

//            // Tạo các claims cho đăng nhập
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.Name, taiKhoan.ho_ten),
//                new Claim(ClaimTypes.Email, taiKhoan.email),
//                new Claim(ClaimTypes.Role, taiKhoan.Vai_Tro.ten_vai_tro) // Phân quyền theo Vai_Tro
//            };

//            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
//            var authProperties = new AuthenticationProperties();

//            // Đăng nhập và lưu session
//            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

//            // Chuyển hướng đến trang chủ hoặc trang tương ứng dựa trên vai trò
//            if (taiKhoan.Vai_Tro.ten_vai_tro == "Admin")
//            {
//                return RedirectToAction("Index", "Admin");
//            }
//            else if (taiKhoan.Vai_Tro.ten_vai_tro == "Nhân Viên")
//            {
//                return RedirectToAction("Index", "NhanVien");
//            }
//            else
//            {
//                return RedirectToAction("Index", "Home");
//            }
//        }


//        public IActionResult DangKy()
//        {
//            return View();
//        }


//        [HttpPost]
//        public async Task<IActionResult> DangKy(LoginViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }

//            // Kiểm tra nếu tài khoản đã tồn tại
//            var existingAccount = _context.tai_Khoans.FirstOrDefault(t => t.user_name == model.Username);
//            if (existingAccount != null)
//            {
//                model.ErrorMessage = "Tài khoản đã tồn tại!";
//                return View(model); // Nếu tài khoản đã tồn tại, thông báo lỗi
//            }

//            var khachhang = _context.vai_Tros.FirstOrDefault(v => v.ten_vai_tro == "Khách Hàng");
//            // Tạo tài khoản mới
//            var taiKhoan = new Tai_Khoan
//            {
//                ID = Guid.NewGuid(),
//                email = model.Username,
//                pass_word = model.Password,
//                Vai_TroID = khachhang.ID,
//                ngay_tao = DateTime.Now
//            };

//            _context.tai_Khoans.Add(taiKhoan);
//            await _context.SaveChangesAsync();

//            return RedirectToAction("DangNhap"); // Chuyển hướng về trang đăng nhập
//        }

//        // ✅ 5. Đăng xuất
//        public async Task<IActionResult> DangXuat()
//        {
//            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//            return RedirectToAction("DangNhap");
//        }
//    }
//}
