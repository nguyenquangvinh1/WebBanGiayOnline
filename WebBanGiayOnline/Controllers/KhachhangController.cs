using ClssLib;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;
using WebBanGiay.Data;

namespace WebBanGiay.Controllers
{
    public class KhachhangController : Controller
    {
        private readonly AppDbContext _context;
        public KhachhangController(AppDbContext context)
        {
            _context = context;
        }
        // GET: Thông tin khách hàng + danh sách địa chỉ
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(User.FindFirstValue("userid"));

            var customer = await _context.tai_Khoans
                .Include(c => c.Dia_Chi)
                .FirstOrDefaultAsync(c => c.ID == userId);

            if (customer == null) return NotFound();

            customer.Dia_Chi = customer.Dia_Chi
                .OrderBy(a => a.loai_dia_chi)
                .ToList();

            return View(customer);
        }

        // POST: Cập nhật thông tin khách hàng
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer([FromBody] Tai_Khoan model)
        {
            var userId = Guid.Parse(User.FindFirstValue("userid"));

            var customer = await _context.tai_Khoans.FindAsync(userId);
            if (customer == null) return Json(new { success = false });

            customer.ho_ten = model.ho_ten;
            customer.email = model.email;
            customer.sdt = model.sdt;

            _context.tai_Khoans.Update(customer);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // POST: Lưu địa chỉ (thêm hoặc sửa)
        [HttpPost]
        public IActionResult SaveAddress(Dia_Chi address)
        {
            var userId = Guid.Parse(User.FindFirstValue("userid"));

            if (userId == Guid.Empty) return Json(new { success = false, message = "Tài khoản không hợp lệ." });

            var hasDefault = _context.dia_Chis.Any(dc => dc.Tai_KhoanID == userId && dc.loai_dia_chi == 1);

            if (address.ID == Guid.Empty)
            {
                address.ID = Guid.NewGuid();
                address.Tai_KhoanID = userId;
                address.ngay_tao = DateTime.Now;
                address.loai_dia_chi = hasDefault ? 2 : 1;

                _context.Add(address);
            }
            else
            {
                var existing = _context.dia_Chis.FirstOrDefault(dc => dc.ID == address.ID && dc.Tai_KhoanID == userId);
                if (existing == null) return Json(new { success = false, message = "Không tìm thấy địa chỉ." });

                existing.dia_chi_chi_tiet = address.dia_chi_chi_tiet;
                existing.tinh = address.tinh;
                existing.huyen = address.huyen;
                existing.xa = address.xa;
                existing.loai_dia_chi = address.loai_dia_chi;
                existing.ngay_sua = DateTime.Now;

                _context.Update(existing);
            }

            _context.SaveChanges();

            return Json(new { success = true, id = address.ID, loai_dia_chi = address.loai_dia_chi });
        }

        [HttpGet]
        public IActionResult GetAddressById(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue("userid"));

            var address = _context.dia_Chis.FirstOrDefault(a => a.ID == id && a.Tai_KhoanID == userId);
            if (address == null)
                return Json(new { success = false, message = "Không tìm thấy địa chỉ." });

            return Json(new
            {
                success = true,
                address = new
                {
                    address.ID,
                    address.dia_chi_chi_tiet,
                    address.tinh,
                    address.huyen,
                    address.xa,
                    address.loai_dia_chi
                }
            });
        }

        [HttpPost]
        public IActionResult DeleteAddress(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue("userid"));

            var address = _context.dia_Chis.FirstOrDefault(a => a.ID == id && a.Tai_KhoanID == userId);
            if (address != null)
            {
                _context.dia_Chis.Remove(address);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult SetDefaultAddress(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue("userid"));

            var address = _context.dia_Chis.FirstOrDefault(a => a.ID == id && a.Tai_KhoanID == userId);
            if (address == null)
                return Json(new { success = false, message = "Không tìm thấy địa chỉ." });

            var allAddresses = _context.dia_Chis.Where(a => a.Tai_KhoanID == userId).ToList();
            foreach (var a in allAddresses)
            {
                a.loai_dia_chi = a.ID == id ? 1 : 2;
                _context.Attach(a).State = EntityState.Modified;
            }

            _context.SaveChanges();

            return Json(new { success = true, message = "Đã chọn địa chỉ mặc định." });
        }
        [HttpGet]
        public IActionResult GetThongTinKhachHang(Guid id)
        {
            var khach = _context.tai_Khoans
                .Include(x => x.Vai_Tro)
                .FirstOrDefault(x => x.ID == id);
            if (khach == null)
            {
                return Json(new { success = false, message = "không tìm thấy khách hàng!" });
            }

            // Lấy địa chỉ của người dùng (nếu có)
            var diaChi = _context.dia_Chis.FirstOrDefault(dc => dc.Tai_KhoanID == id);

            string CleanLocationName(string input) =>
            string.IsNullOrWhiteSpace(input) ? "" :
            Regex.Replace(input, @"^(Tỉnh|Thành phố)\s+", "", RegexOptions.IgnoreCase).Trim();

            if (khach == null)
                return NotFound();

            return Json(new
            {
                name = khach.ho_ten,
                email = khach.email,
                phone = khach.sdt,
                address = diaChi?.dia_chi_chi_tiet ?? "",
                province = CleanLocationName(diaChi?.tinh),
                district = diaChi?.huyen ?? "",
                ward = diaChi?.xa ?? ""
            });
        }
        [HttpGet]
        public IActionResult GetAddressCustomer(Guid? customerId)
        {
            if (customerId == null)
            {
                return Json(new { success = false, message = "chưa có ID khách hàng! " });
            }
            try
            {
                var query = _context.dia_Chis.Where(x => x.Tai_KhoanID == customerId)
                    .Select(x => new
                    {
                        tinh = x.tinh,
                        huyen = x.huyen,
                        xa = x.xa,
                        chi_tiet = x.dia_chi_chi_tiet
                    }).ToList();
                return Json(query);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = "Lỗi địa chỉ: " + ex.Message });
            }

        }
    }
}

