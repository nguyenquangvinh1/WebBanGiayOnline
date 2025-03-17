using ClssLib;
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

        public IActionResult Index()
        {
            var tai_Khoans = _context.tai_Khoans
                .Where(t => t.Vai_Tro.ten_vai_tro == "Khách Hàng")
                .Include(tk => tk.Dia_Chi)
                .OrderByDescending(tk => tk.ngay_tao) // Sắp xếp theo ngày tạo mới nhất
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

            return View(tai_Khoans);
        }


        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Khachhang = _context.vai_Tros.FirstOrDefault(x => x.ten_vai_tro == "Khách Hàng");
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
            if (_context.tai_Khoans.Any(n => n.sdt == model.PhoneNumber))
            {
                ModelState.AddModelError("sdt", "Số điện thoại đã tồn tại!");
            }
            if (ModelState.IsValid)
            {

                // Truy vấn vai trò "Khách Hàng" trong cơ sở dữ liệu
                var vaiTro = await _context.vai_Tros.FirstOrDefaultAsync(v => v.ten_vai_tro == "Khách hàng");

                if (vaiTro == null)
                {
                    ModelState.AddModelError("", "Vai trò Khách Hàng không tồn tại.");
                    return View(model);
                }

                // Tạo mới tài khoản
                var taiKhoan = new Tai_Khoan
                {
                    ID = Guid.NewGuid(),
                    user_name = model.UserName,
                    ho_ten = model.Ho_ten,
                    pass_word = model.PassWord,
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

        [HttpPost]
        public JsonResult UpdateAddress(Dia_Chi model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
            }

            if (model.Tai_KhoanID == null || !_context.tai_Khoans.Any(x => x.ID == model.Tai_KhoanID))
            {
                return Json(new { success = false, message = "Tài khoản không hợp lệ!" });
            }

            try
            {

                var existingAddress = _context.dia_Chis.Find(model.ID);
                if (existingAddress == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy địa chỉ để cập nhật!" });
                }

                existingAddress.dia_chi_chi_tiet = model.dia_chi_chi_tiet;
                existingAddress.tinh = model.tinh;
                existingAddress.huyen = model.huyen;
                existingAddress.xa = model.xa;



                _context.dia_Chis.Update(existingAddress);


                _context.SaveChanges();
                return Json(new { success = true, message = "Cập nhật địa chỉ thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi lưu dữ liệu: " + ex.Message });
            }
        }



        //-----------------Thêm địa chỉ----------------------//
        [HttpPost]
        public JsonResult AddAddress(Dia_Chi model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
            }

            if (model.Tai_KhoanID == null || !_context.tai_Khoans.Any(x => x.ID == model.Tai_KhoanID))
            {
                return Json(new { success = false, message = "Tài khoản không hợp lệ!" });
            }

            try
            {
                // Kiểm tra xem tài khoản đã có địa chỉ mặc định chưa
                bool hasDefaultAddress = _context.dia_Chis.Any(x => x.Tai_KhoanID == model.Tai_KhoanID && x.loai_dia_chi == 1);

                var newAddress = new Dia_Chi
                {
                    ID = Guid.NewGuid(),
                    Tai_KhoanID = model.Tai_KhoanID,
                    dia_chi_chi_tiet = model.dia_chi_chi_tiet,
                    tinh = model.tinh,
                    huyen = model.huyen,
                    xa = model.xa,
                    loai_dia_chi = hasDefaultAddress ? 2 : 1 // Nếu chưa có mặc định thì đặt là 1, nếu có rồi thì đặt là 2
                };

                _context.dia_Chis.Add(newAddress);
                _context.SaveChanges();

                return Json(new { success = true, message = "Thêm địa chỉ thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi lưu dữ liệu: " + ex.Message });
            }
        }

        //-----------------Xóa----------------------//

        [HttpPost]
        public IActionResult DeleteAddress(Guid id)
        {
            var address = _context.dia_Chis.FirstOrDefault(d => d.ID == id);

            if (address == null)
            {
                return Json(new { success = false, message = "Địa chỉ không tồn tại!" });
            }

            // Kiểm tra nếu địa chỉ đang là mặc định (LoaiDiaChi = 1)
            if (address.loai_dia_chi == 1)
            {
                return Json(new { success = false, message = "Không thể xóa địa chỉ mặc định!" });
            }

            _context.dia_Chis.Remove(address);
            _context.SaveChanges();

            return Json(new { success = true, message = "Xóa địa chỉ thành công!" });
        }


        //-----------------Chọn mặc đinh----------------------//

        [HttpPost]
        public IActionResult SetDefaultAddress(Guid addressId, Guid customerId)
        {
            var customer = _context.tai_Khoans
                .Include(c => c.Dia_Chi)

                .FirstOrDefault(c => c.ID == customerId);


            if (customer == null)
                return Json(new { success = false, message = "Khách hàng không tồn tại." });

            // Kiểm tra địa chỉ có thuộc khách hàng không
            var selectedAddress = customer.Dia_Chi.FirstOrDefault(a => a.ID == addressId);
            if (selectedAddress == null)
                return Json(new { success = false, message = "Địa chỉ không tồn tại." });

            // Cập nhật tất cả địa chỉ thành "Phụ"
            foreach (var address in customer.Dia_Chi)
            {
                address.loai_dia_chi = 2;
                _context.Entry(address).State = EntityState.Modified; // Đánh dấu là đã sửa
            }

            // Cập nhật địa chỉ được chọn thành "Mặc định"
            selectedAddress.loai_dia_chi = 1;
            _context.Entry(selectedAddress).State = EntityState.Modified;

            _context.SaveChanges(); // Lưu thay đổi vào CSDL

            // Lấy lại danh sách địa chỉ đã cập nhật từ CSDL
            var sortedAddresses = _context.dia_Chis
                .Where(a => a.Tai_KhoanID == customerId)
                .OrderBy(a => a.loai_dia_chi) // Mặc định lên đầu
                .Select(a => new
                {
                    id = a.ID,
                    dia_chi = a.dia_chi_chi_tiet,
                    xa = a.xa,
                    huyen = a.huyen,
                    tinh = a.tinh,
                    loai_dia_chi = a.loai_dia_chi
                })
                .ToList();

            return Json(new { success = true, message = "Cập nhật thành công!", sortedAddresses });
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
                    ten_vai_tro = "Khách Hàng"
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
                //Kiểm tra vai trò "Khách Hàng"
                var vaiTro = await _context.vai_Tros.FirstOrDefaultAsync(v => v.ten_vai_tro == "Khách Hàng");
                if (vaiTro == null)
                {
                    return BadRequest(new { success = false, message = "Vai trò Khách Hàng không tồn tại." });
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
                    ma = GenerateUniqueMaKhachHang(), // Hàm tạo mã khách hàng duy nhất
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