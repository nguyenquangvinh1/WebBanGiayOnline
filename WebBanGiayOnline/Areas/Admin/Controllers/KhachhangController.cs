using ClssLib;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WebBanGiay.Areas.Admin.Data;
using WebBanGiay.Areas.Admin.Models.ViewModel;
using WebBanGiay.Data;
using WebBanGiay.ViewModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebBanGiay.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KhachhangController : Controller
    {
        //HttpClient _httpClient;
        private readonly AppDbContext _context;

        public KhachhangController(AppDbContext context)
        {
            _context = context;
            //this._httpClient = httpClient;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 3;

            var query = _context.tai_Khoans
                .Where(t => t.Vai_Tro.ten_vai_tro == "Khách hàng")
                .Include(tk => tk.Dia_Chi)
                .OrderByDescending(tk => tk.ngay_tao);

            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var tai_Khoans = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(customer => new CreateCustomerViewModel
                {
                    Id = customer.ID,
                    Ho_ten = customer.ho_ten,
                    Makh = customer.ma,
                    PhoneNumber = customer.sdt,
                    Email = customer.email,
                    DateOfBirth = customer.ngay_sinh,
                    Gender = customer.gioi_tinh,
                    Createdate = customer.ngay_tao,
                    tinh = customer.Dia_Chi.FirstOrDefault(dc => dc.loai_dia_chi == 1)?.tinh,
                    huyen = customer.Dia_Chi.FirstOrDefault(dc => dc.loai_dia_chi == 1)?.huyen,
                    xa = customer.Dia_Chi.FirstOrDefault(dc => dc.loai_dia_chi == 1)?.xa,
                    dia_chi = customer.Dia_Chi.FirstOrDefault(dc => dc.loai_dia_chi == 1)?.dia_chi_chi_tiet
                })
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;
            ViewBag.TotalPages = totalPages;

            return View(tai_Khoans);
        }


        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Khachhang = _context.vai_Tros.FirstOrDefault(x => x.ten_vai_tro == "Khách hàng");
            var kh = await _context.tai_Khoans.Where(x => x.Vai_TroID == Khachhang.ID)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kh == null)
            {
                return NotFound();
            }

            return View(kh);
        }

        public IActionResult Create()
        {

            var viewModel = new CreateCustomerViewModel
            {

            }; // Khởi tạo model mới
            return View(viewModel);
        }



        // POST: Admin/Customer/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerViewModel model)
        {
            if (_context.tai_Khoans.Any(n => n.email == model.Email))
            {
                ModelState.AddModelError("email", "Email đã tồn tại!");
            }
            if (_context.tai_Khoans.Any(n => n.cccd == model.CCCD))
            {
                ModelState.AddModelError("cccd", "Số CCCD đã tồn tại!");
            }
            if (ModelState.IsValid)
            {

                // Truy vấn vai trò "Khách hàng" trong cơ sở dữ liệu
                var vaiTro = _context.vai_Tros.FirstOrDefault(v => v.ten_vai_tro == "Khách hàng");

                if (vaiTro == null)
                {
                    ModelState.AddModelError("", "Vai trò Khách hàng không tồn tại.");
                    return View(model);
                }

                // Tạo mới tài khoản
                var taiKhoan = new Tai_Khoan
                {
                    ID = Guid.NewGuid(),
                    user_name = model.UserName,
                    ho_ten = model.Ho_ten,
                  
                    ngay_sinh = model.DateOfBirth,
                    sdt = model.PhoneNumber,
                    email = model.Email,
                    cccd = model.CCCD,
                    gioi_tinh = model.Gender,
                    trang_thai = 1,
                    ngay_tao = DateTime.Now,
                    Vai_TroID = vaiTro.ID,
                    ma = GenerateUniqueMaKhachHang(),

                };


                //taiKhoan.GenerateCustomerCode();


                _context.tai_Khoans.Add(taiKhoan);
                await _context.SaveChangesAsync();

                model.Makh = taiKhoan.ma;
                // Tạo địa chỉ
                var diaChi = new Dia_Chi
                {
                    Tai_KhoanID = taiKhoan.ID,
                    tinh = model.tinh,
                    huyen = model.huyen,
                    xa = model.xa,
                    dia_chi_chi_tiet = model.dia_chi,
                    loai_dia_chi = 1,
                    ngay_tao = DateTime.Now,

                };


                //model.Ma = taiKhoan.ma;

                _context.dia_Chis.Add(diaChi);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Thêm khách hàng thành công!";

                return RedirectToAction("Index", "Khachhang");
            }
            return View(model);
        }

        private string GenerateUniqueMaKhachHang()
        {
            return "KH" + new Random().Next(100000, 999999);

        }

        //----------------------------------------Edit----------------------------------//

        // GET: Edit Customer
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
        //-----------------Cập nhật địa chỉ----------------------//
        public IActionResult SaveAddress(Dia_Chi address)
        {
            try
            {
                // Kiểm tra nếu Tai_KhoanID không hợp lệ
                if (address.Tai_KhoanID == null || address.Tai_KhoanID == Guid.Empty)
                {
                    return Json(new { success = false, message = "Taikhoan_id không hợp lệ." });
                }

                var hasDefaultAddress = _context.dia_Chis
                    .Any(dc => dc.Tai_KhoanID == address.Tai_KhoanID && dc.loai_dia_chi == 1);

                // Kiểm tra nếu ID là Guid.Empty (Thêm mới) hoặc không có ID
                if (address.ID == Guid.Empty)
                {
                    address.ID = Guid.NewGuid(); // Tạo GUID mới cho địa chỉ
                    address.ngay_tao = DateTime.Now; // Gán ngày tạo
                    address.loai_dia_chi = hasDefaultAddress ? 2 : 1; // Nếu đã có địa chỉ mặc định thì loại địa chỉ mới là phụ

                    _context.Add(address); // Thêm mới địa chỉ vào cơ sở dữ liệu
                }
                else // Nếu có ID thì thực hiện cập nhật
                {
                    var existingAddress = _context.dia_Chis.FirstOrDefault(dc => dc.ID == address.ID);
                    if (existingAddress != null)
                    {
                        // Cập nhật thông tin địa chỉ
                        existingAddress.dia_chi_chi_tiet = address.dia_chi_chi_tiet;
                        existingAddress.tinh = address.tinh; // Cập nhật tên tỉnh
                        existingAddress.huyen = address.huyen; // Cập nhật tên huyện
                        existingAddress.xa = address.xa; // Cập nhật tên xã
                        existingAddress.loai_dia_chi = address.loai_dia_chi;
                        existingAddress.ngay_sua = DateTime.Now; // Gán ngày sửa
                        _context.Update(existingAddress); // Cập nhật địa chỉ trong cơ sở dữ liệu
                    }
                    else
                    {
                        return Json(new { success = false, message = "Không tìm thấy địa chỉ để cập nhật." });
                    }
                }

                _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                return Json(new
                {
                    success = true,
                    id = address.ID,
                    loai_dia_chi = address.loai_dia_chi
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



        //---------------------------Sửa-----------------------//
        // Controller action để lấy thông tin địa chỉ
        [HttpGet]
        public IActionResult GetAddressById(Guid id)
        {
            var address = _context.dia_Chis.FirstOrDefault(a => a.ID == id);
            if (address == null)
            {
                return Json(new { success = false, message = "Không tìm thấy địa chỉ." });
            }

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
            var address = _context.dia_Chis.FirstOrDefault(a => a.ID == id);
            if (address != null)
            {
                _context.dia_Chis.Remove(address);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


        //---------------Chọn mặc định--------------------//

        [HttpPost]
        public IActionResult SetDefaultAddress(Guid id)
        {
            try
            {
                // Tìm địa chỉ cần thay đổi thành mặc định
                var address = _context.dia_Chis.FirstOrDefault(a => a.ID == id);
                if (address == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy địa chỉ." });
                }

                // Đặt tất cả địa chỉ của khách hàng thành "Phụ" (Chỉ cập nhật địa chỉ này)
                var customerAddresses = _context.dia_Chis.Where(a => a.Tai_KhoanID == address.Tai_KhoanID).ToList();
                foreach (var customerAddress in customerAddresses)
                {
                    if (customerAddress.ID != id) // Không thay đổi địa chỉ hiện tại
                    {
                        customerAddress.loai_dia_chi = 2; // Đặt loại địa chỉ thành "Phụ"
                        _context.Attach(customerAddress).State = EntityState.Modified;
                    }
                }

                // Đặt địa chỉ được chọn làm mặc định
                address.loai_dia_chi = 1; // Đặt loại địa chỉ thành "Mặc định"
                _context.Attach(address).State = EntityState.Modified;

                // Lưu các thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();

                return Json(new { success = true, message = "Địa chỉ đã được chọn làm mặc định." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Đã có lỗi xảy ra: " + ex.Message });
            }
        }



        [HttpGet]
        public IActionResult GetAllKhachHang()
        {
            var tai_Khoans = _context.tai_Khoans
                 .Where(t => t.Vai_Tro.ten_vai_tro == "Khách hàng")
                 .Include(tk => tk.Dia_Chi)
                 .OrderByDescending(tk => tk.ngay_tao)
                 .ToList()
                 .Select(customer => new {
                     id = customer.ID,                 // Sử dụng "id" (chữ thường)
                     ho_ten = customer.ho_ten,
                     phoneNumber = customer.sdt,
                     email = customer.email,
                     dateOfBirth = customer.ngay_sinh,
                     gender = customer.gioi_tinh,
                     createdate = customer.ngay_tao,
                     tinh = customer.Dia_Chi.FirstOrDefault(dc => dc.loai_dia_chi == 1)?.tinh,
                     huyen = customer.Dia_Chi.FirstOrDefault(dc => dc.loai_dia_chi == 1)?.huyen,
                     xa = customer.Dia_Chi.FirstOrDefault(dc => dc.loai_dia_chi == 1)?.xa,
                     dia_chi = customer.Dia_Chi.FirstOrDefault(dc => dc.loai_dia_chi == 1)?.dia_chi_chi_tiet
                 })
                 .ToList();

            return Json(new { success = true, data = tai_Khoans });
        }



        public static void Seed(AppDbContext context)
        {
            if (!context.vai_Tros.Any())
            {
                context.vai_Tros.Add(new Vai_Tro
                {
                    ID = Guid.NewGuid(),
                    ten_vai_tro = "Khách hàng"
                    // thêm các thuộc tính khác nếu cần
                });
                context.SaveChanges();
            }
        }



        [HttpPost]
        public async Task<IActionResult> AddQuickCustomer([FromBody] QuickCustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();
                return BadRequest(new { success = false, message = "Invalid model", errors });
            }

            try
            {
                //Kiểm tra vai trò "Khách hàng"
                var vaiTro = await _context.vai_Tros.FirstOrDefaultAsync(v => v.ten_vai_tro == "Khách hàng");
                if (vaiTro == null)
                {
                    return BadRequest(new { success = false, message = "Vai trò Khách hàng không tồn tại." });
                }

                if (await _context.tai_Khoans.AnyAsync(n => n.email == model.Email))
                {
                    return BadRequest(new { success = false, message = "Email đã tồn tại!" });
                }
                if (await _context.tai_Khoans.AnyAsync(n => n.sdt == model.PhoneNumber))
                {
                    return BadRequest(new { success = false, message = "SĐT đã tồn tại!" });
                }

                var newCustomer = new Tai_Khoan
                {
                    ID = Guid.NewGuid(),
                    user_name = "",
                    ho_ten = model.HoTen,
                    sdt = model.PhoneNumber,
                    email = model.Email,
                    ngay_tao = DateTime.Now,
                    Vai_TroID = vaiTro.ID,
                    ma = GenerateUniqueMaKhachHang(), // Hàm tạo mã Khách hàng duy nhất
                    gioi_tinh = 0,
                    ngay_sinh = new DateTime(2000, 1, 1),
                    cccd = "",
                    trang_thai = 1
                };

                _context.tai_Khoans.Add(newCustomer);
                await _context.SaveChangesAsync();

                var diaChi = new Dia_Chi
                {
                    ID = Guid.NewGuid(),
                    loai_dia_chi = 1,
                    tinh = "",
                    huyen = "",
                    xa = "",
                    dia_chi_chi_tiet = "",
                    Tai_KhoanID = newCustomer.ID,
                    ngay_tao = DateTime.Now,
                    ngay_sua = DateTime.Now
                };

                _context.dia_Chis.Add(diaChi);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    data = new
                    {
                        id = newCustomer.ID,
                        ho_ten = newCustomer.ho_ten,
                        phoneNumber = newCustomer.sdt,
                        email = newCustomer.email,
                        dia_chi = new
                        {
                            diaChiID = diaChi.ID,
                            dia_chi_chi_tiet = diaChi.dia_chi_chi_tiet,
                            tinh = diaChi.tinh,
                            huyen = diaChi.huyen,
                            xa = diaChi.xa
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }








        //------------------------------------------------------------------------------//
        private bool Exists(Guid id)
        {
            return _context.tai_Khoans.Any(e => e.ID == id);
        }
    }
}