using ClssLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Data;
using Newtonsoft.Json;
using WebBanGiay.view_mode;
using System.Globalization;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using X.PagedList;
using X.PagedList.Mvc.Core;
using X.PagedList.Extensions;
using Microsoft.IdentityModel.Tokens;
using ClosedXML.Excel;
using System.IO;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Authorization;



namespace WebBanGiay.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Policy = "EmployeePolicy")]
    [Authorize(Policy = "EmployeePolicy")]

    //[Route("Admin/NhanVien")]

    public class NhanVienController : Controller
    {
        private readonly AppDbContext _context;

        public NhanVienController(AppDbContext context)
        {
            _context = context;
        }






        public async Task<IActionResult> Index(int? pageNumber, int pageSize = 3)
        {
            ViewBag.PageSizes = new List<SelectListItem>
    {
        new SelectListItem { Value = "5", Text = "5" },
        new SelectListItem { Value = "10", Text = "10" },
        new SelectListItem { Value = "20", Text = "20" },
        new SelectListItem { Value = "50", Text = "50" },
        new SelectListItem { Value = "100", Text = "100" }
    };

            ViewBag.SelectedPageSize = pageSize;
            int currentPage = pageNumber ?? 1;

            // Thiết lập header để tránh cache (nếu cần)
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";


            //xoas thong bao k tieu thu them sau khi them nv ok 
            var successMessage= TempData["SuccessMessage"] as string;
            TempData.Remove("SuccessMessage");

            var nhanVienRole = await _context.vai_Tros.FirstOrDefaultAsync(x => x.ten_vai_tro == "Nhân Viên");
            if (nhanVienRole == null)
            {
                return NotFound();
            }

            var list = await _context.tai_Khoans
                .Where(x => x.Vai_TroID == nhanVienRole.ID)
                .OrderByDescending(x => x.ngay_tao)
                .ToListAsync();

            var lst = new List<NhanVien_Model>();
            foreach (var item in list)
            {
                var diachi = await _context.dia_Chis.FirstOrDefaultAsync(x => x.Tai_KhoanID == item.ID && x.loai_dia_chi == 1);
                lst.Add(new NhanVien_Model
                {
                    Id = item.ID,
                    user_name = item.user_name,
                    pass_word = item.pass_word,
                    ho_ten = item.ho_ten,
                    ngay_sinh = item.ngay_sinh,
                    gioi_tinh = item.gioi_tinh,
                    sdt = item.sdt,
                    email = item.email,
                    cccd = item.cccd,
                    trang_thai=item.trang_thai,
                    tinh = diachi?.tinh,
                    huyen = diachi?.huyen,
                    xa = diachi?.xa,
                    dia_chi_chi_tiet = diachi?.dia_chi_chi_tiet
                });
            }

            // Áp dụng phân trang
            var paginatedList = lst.ToPagedList(currentPage, pageSize);

            // Chuyển đổi sang EmployeeListViewModel
            var viewModel = new EmployeeListViewModel
            {
                Employees = paginatedList.ToList(),  // Chuyển sang List để phù hợp với kiểu dữ liệu
                CurrentPage = paginatedList.PageNumber,
                TotalPages = paginatedList.PageCount,
                PageSize = paginatedList.PageSize,
                TotalCount = paginatedList.TotalItemCount,
                SuccessMessage = successMessage
            };

            return View(viewModel);
        }





        //phan trang 



        //xuat excel 


        public IActionResult ExportExcel()
        {
            // Lấy vai trò "Nhân Viên"
            var nhanVienRole = _context.vai_Tros.FirstOrDefault(x => x.ten_vai_tro == "Nhân Viên");
            if (nhanVienRole == null)
            {
                return BadRequest("Vai trò Nhân Viên không tồn tại!");
            }

            // Lọc chỉ những tài khoản thuộc nhân viên
            var employees = _context.tai_Khoans
                .Where(x => x.Vai_TroID == nhanVienRole.ID)
                .ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("NhanVien");

                // Tạo dòng tiêu đề cho file Excel: hợp nhất ô từ A1 đến G1
                worksheet.Range("A1:G1").Merge();
                worksheet.Cell("A1").Value = "DANH SÁCH NHÂN VIÊN";
                worksheet.Cell("A1").Style.Font.Bold = true;
                worksheet.Cell("A1").Style.Font.FontSize = 16;
                worksheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell("A1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Row(1).Height = 30; // điều chỉnh chiều cao hàng nếu cần

                // Tạo header cho bảng ở hàng thứ 2
                worksheet.Cell(2, 1).Value = "Họ và tên";
                worksheet.Cell(2, 2).Value = "Username";
                worksheet.Cell(2, 3).Value = "Email";
                worksheet.Cell(2, 4).Value = "CCCD";
                worksheet.Cell(2, 5).Value = "SĐT";
                worksheet.Cell(2, 6).Value = "Giới tính";
                worksheet.Cell(2, 7).Value = "Ngày sinh";

                // Định dạng header (hàng 2)
                var headerRange = worksheet.Range(2, 1, 2, 7);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

                // Điền dữ liệu nhân viên bắt đầu từ hàng thứ 3
                int row = 3;
                foreach (var emp in employees)
                {
                    worksheet.Cell(row, 1).Value = emp.ho_ten;
                    worksheet.Cell(row, 2).Value = emp.user_name;
                    worksheet.Cell(row, 3).Value = emp.email;
                    worksheet.Cell(row, 4).Value = emp.cccd;
                    worksheet.Cell(row, 5).Value = emp.sdt;
                    worksheet.Cell(row, 6).Value = emp.gioi_tinh == 1 ? "Nam" : "Nữ";
                    worksheet.Cell(row, 7).Value = emp.ngay_sinh.ToString("yyyy-MM-dd");
                    row++;
                }

                // Điều chỉnh kích thước cột cho vừa nội dung
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "NhanVien.xlsx");
                }
            }
        }


        //public async Task<IActionResult> Index(int? pageNumber, int pageSize = 5)
        //{
        //    ViewBag.PageSizes = new List<SelectListItem>
        //{
        //    new SelectListItem { Value = "5", Text = "5" },
        //    new SelectListItem { Value = "10", Text = "10" },
        //    new SelectListItem { Value = "20", Text = "20" },
        //    new SelectListItem { Value = "50", Text = "50" },
        //    new SelectListItem { Value = "100", Text = "100" }
        //};

        //    ViewBag.SelectedPageSize = pageSize; // Giá trị mặc định

        //    int currentPage = pageNumber ?? 1; // Nếu không có tham số, mặc định là trang 1

        //    var nhanVien = _context.vai_Tros.FirstOrDefault(x => x.ten_vai_tro == "Nhân Viên");
        //    var list = await _context.tai_Khoans
        //          .Where(x => x.Vai_TroID == nhanVien.ID)
        //          .ToListAsync();

        //    var lst = new List<NhanVien_Model>();
        //    foreach (var item in list)
        //    {
        //        var diachi = _context.dia_Chis.FirstOrDefault(x => x.Tai_KhoanID == item.ID && x.loai_dia_chi == 1);
        //        lst.Add(new NhanVien_Model()
        //        {
        //            Id = item.ID,
        //            user_name = item.user_name,
        //            pass_word = item.pass_word,
        //            ho_ten = item.ho_ten,
        //            ngay_sinh = item.ngay_sinh,
        //            gioi_tinh = item.gioi_tinh,
        //            sdt = item.sdt,
        //            email = item.email,
        //            cccd = item.cccd,
        //            tinh = (diachi?.tinh ?? "") + (diachi?.huyen ?? "") + (diachi?.xa ?? "")
        //        });
        //    }

        //    // Phân trang
        //    var paginatedList = lst.ToPagedList(currentPage, pageSize);
        //    return View(paginatedList);
        //}


        public async Task<IActionResult> Search(string keyword)
        {
            // Lấy vai trò "Nhân Viên"
            var nhanVienRole = _context.vai_Tros.FirstOrDefault(x => x.ten_vai_tro == "Nhân Viên");
            if (nhanVienRole == null)
            {
                return Json(new List<NhanVien_Model>());
            }

            // Lọc các tài khoản theo vai trò "Nhân Viên"
            var query = _context.tai_Khoans
                .Include(nv => nv.Dia_Chi)  // Nạp luôn thông tin địa chỉ
                .Where(nv => nv.Vai_TroID == nhanVienRole.ID)
                .AsQueryable();

            // Áp dụng điều kiện tìm kiếm nếu keyword không rỗng
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(nv => nv.ho_ten.Contains(keyword)
                                       || nv.email.Contains(keyword)
                                       || nv.sdt.Contains(keyword));
            }

            var accounts = await query.ToListAsync();

            var result = accounts.Select(nv =>
            {
                // Lấy địa chỉ theo điều kiện loai_dia_chi == 1
                var dc = nv.Dia_Chi.FirstOrDefault(d => d.loai_dia_chi == 1);
                // Ghép chuỗi địa chỉ đầy đủ: nếu dc khác null, ghép lại theo định dạng "Địa chỉ chi tiết, Tỉnh, Huyện, Xã"
                var diaChiFull = dc != null
                    ? $"{dc.dia_chi_chi_tiet}, {dc.tinh}, {dc.huyen}, {dc.xa}"
                    : "";
                return new NhanVien_Model
                {
                    Id = nv.ID,
                    ho_ten = nv.ho_ten,
                    email = nv.email,
                    sdt = nv.sdt,
                    cccd = nv.cccd,
                    gioi_tinh = nv.gioi_tinh,
                    ngay_sinh = nv.ngay_sinh,
                    trang_thai = nv.trang_thai,
                    // Gán chuỗi địa chỉ đầy đủ vào thuộc tính "tinh" (bạn có thể đổi tên thuộc tính nếu cần)
                    tinh = diaChiFull
                };
            }).ToList();

            return Json(result);
        }



        public JsonResult FilterByStatus(int? status)
        {
            // Lấy vai trò "Nhân Viên"
            var nhanVienRole = _context.vai_Tros.FirstOrDefault(x => x.ten_vai_tro == "Nhân Viên");
            if (nhanVienRole == null)
            {
                return Json(new List<object>());
            }

            // Lọc tài khoản: chỉ lấy những tài khoản của nhân viên theo vai trò "Nhân Viên"
            var accounts = _context.tai_Khoans
                .Include(tk => tk.Dia_Chi)
                .Where(tk => tk.Vai_TroID == nhanVienRole.ID && (!status.HasValue || tk.trang_thai == status))
                .ToList();

            var result = accounts.Select(tk =>
            {
                // Lấy địa chỉ theo điều kiện loai_dia_chi == 1
                var dc = tk.Dia_Chi.FirstOrDefault(d => d.loai_dia_chi == 1);
                // Tạo chuỗi địa chỉ đầy đủ: nếu có dữ liệu thì ghép lại, nếu không thì trả về chuỗi rỗng
                var diaChiFull = dc != null
                                 ? $"{dc.dia_chi_chi_tiet}, {dc.tinh}, {dc.huyen}, {dc.xa}"
                                 : "";
                return new
                {
                    Id = tk.ID,
                    ho_ten = tk.ho_ten,
                    email = tk.email,
                    sdt = tk.sdt,
                    cccd = tk.cccd,
                    gioi_tinh = tk.gioi_tinh,
                    ngay_sinh = tk.ngay_sinh,
                    trang_thai = tk.trang_thai,
                    fulldiachi = diaChiFull
                };
            }).ToList();

            return Json(result);
        }

        [HttpPost]
        public IActionResult ChangeStatus(Guid id, int trang_thai)
        {
            var nhanVien = _context.tai_Khoans.FirstOrDefault(x => x.ID == id);
            if (nhanVien == null)
            {
                return Json(new { success = false, message = "Nhân viên không tồn tại" });
            }

            nhanVien.trang_thai = trang_thai;
            _context.tai_Khoans.Update(nhanVien);
            _context.SaveChanges();

            // Trả về JSON kèm trạng thái mới để client cập nhật DOM
            return Json(new { success = true, newStatus = nhanVien.trang_thai });
        }



        //Update trang thai nhan vien[HttpPost]
        //[HttpPost]
        //public IActionResult ChangeStatus(Guid id, int trang_thai)
        //{
        //    var employee = _context.tai_Khoans.FirstOrDefault(e => e.ID == id);
        //    if (employee == null)
        //    {
        //        return Json(new { success = false, message = "Nhân viên không tồn tại." });
        //    }

        //    // Nếu trạng thái mới bằng với trạng thái hiện tại, không cần cập nhật
        //    if (employee.trang_thai == trang_thai)
        //    {
        //        return Json(new { success = true, message = "Không có thay đổi." });
        //    }

        //    employee.trang_thai = trang_thai;
        //    // Ép EF nhận biết thay đổi của property
        //    _context.Entry(employee).Property(x => x.trang_thai).IsModified = true;

        //    int updated = _context.SaveChanges();
        //    System.Diagnostics.Debug.WriteLine($"Employee ID {id}: New trang_thai = {employee.trang_thai}, updated = {updated}");

        //    return Json(new { success = updated > 0, message = updated > 0 ? "Trạng thái đã được cập nhật." : "Cập nhật không thành công." });
        //}






        // Nested class để cập nhật trạng thái

        //phan trang 

        //[HttpGet("GetNhanViens")]
        public IActionResult GetNhanViens(int page = 1, int pageSize = 10)
        {
            var vaiTroNhanVien = _context.vai_Tros
                .Where(vt => vt.ten_vai_tro == "Nhân viên")
                .Select(vt => vt.ID)
                .FirstOrDefault();

            var query = _context.tai_Khoans
                .Where(tk => tk.Vai_TroID == vaiTroNhanVien)
                .Select(tk => new NhanVien_Model
                {
                    Id = tk.ID,
                    ho_ten = tk.ho_ten,
                    email = tk.email,
                    sdt = tk.sdt,
                    cccd = tk.cccd,
                    user_name = tk.user_name,
                    pass_word = tk.pass_word,
                    ngay_sinh = tk.ngay_sinh,
                    gioi_tinh = tk.gioi_tinh,
                    trang_thai = tk.trang_thai,
                    tinh = tk.Dia_Chi.Where(dc => (bool)dc.is_default).Select(dc => dc.tinh).FirstOrDefault(),
                    huyen = tk.Dia_Chi.Where(dc => (bool)dc.is_default).Select(dc => dc.huyen).FirstOrDefault(),
                    xa = tk.Dia_Chi.Where(dc => (bool)dc.is_default).Select(dc => dc.xa).FirstOrDefault(),
                    hinh_anh = tk.hinh_anh
                });

            // Phân trang
            var totalItems = query.Count();
            var nhanViens = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var response = new
            {
                totalItems,
                totalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                currentPage = page,
                pageSize,
                data = nhanViens
            };

            return Ok(response);
        }











        // GET: Admin/NhanVien/Details/5
        //[HttpGet("Details")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = _context.vai_Tros.FirstOrDefault(x => x.ten_vai_tro == "Nhân Viên");
            var nv = await _context.tai_Khoans
                .Where(x => x.Vai_TroID == nhanVien.ID)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (nv == null)
            {
                return NotFound();
            }

            return View(nv);
        }

        // GET: Admin/NhanVien/Create

        public IActionResult Create()
        {
            TempData.Remove("SuccessMessage");
            var model = new NhanVien_Model(); // Đảm bảo model không null
            
            ViewData["Vai_TroID"] = new SelectList(_context.vai_Tros.ToList(), "ID", "ten_vai_tro");
            return View(model);
        }
        [HttpPost]

        public IActionResult Create(NhanVien_Model nv, IFormFile HinhAnh)
        {
            var nhanVienRole = _context.vai_Tros.FirstOrDefault(x => x.ten_vai_tro == "Nhân Viên");

            if (_context.tai_Khoans.Any(n => n.email == nv.email && n.Vai_TroID == nhanVienRole.ID))
            {
                ModelState.AddModelError("email", "Email đã tồn tại!");
            }
            if (_context.tai_Khoans.Any(n => n.cccd == nv.cccd))
            {
                ModelState.AddModelError("cccd", "Số CCCD đã tồn tại!");
            }
            if (_context.tai_Khoans.Any(n => n.sdt == nv.sdt))
            {
                ModelState.AddModelError("sdt", "Số điện thoại đã tồn tại!");
            }

            if (string.IsNullOrEmpty(nv.pass_word))
            {
                nv.pass_word = GenerateRandomPassword();
            }
            if (!ModelState.IsValid)
            {
                foreach (var kvp in ModelState)
                {
                    foreach (var error in kvp.Value.Errors)
                    {
                        Console.WriteLine($"Key: {kvp.Key}, Error: {error.ErrorMessage}");
                    }
                }
                ViewData["Vai_TroID"] = new SelectList(_context.vai_Tros.ToList(), "ID", "ten_vai_tro");
                return View(nv);
            }

            // Nếu không có file được upload, uniqueFileName sẽ là null
            string uniqueFileName = "default-avatar.jpg";
            if (HinhAnh != null && HinhAnh.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/nhanvien");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(HinhAnh.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    HinhAnh.CopyTo(fileStream);
                }
            }

            int newNumber = 1;
            try
            {
                var maList = _context.tai_Khoans
                    .Where(x => x.ma != null && x.ma.StartsWith("nv"))
                    .Select(x => x.ma)
                    .ToList();

                int lastNumber = 0;
                foreach (var m in maList)
                {
                    if (m.Length > 2 && int.TryParse(m.Substring(2), out int num))
                    {
                        if (num > lastNumber)
                            lastNumber = num;
                    }
                }
                newNumber = lastNumber + 1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error generating newMa: " + ex.Message);
                newNumber = 1;
            }
            string newMa = "nv" + newNumber.ToString("D2");

            Guid newID = Guid.NewGuid();
            if (nhanVienRole == null)
            {
                return BadRequest("Vai trò Nhân Viên không tồn tại!");
            }

            if (string.IsNullOrWhiteSpace(nv.pass_word))
            {
                nv.pass_word = GenerateRandomPassword();
            }
            string generatedPassword = nv.pass_word;

            var taikhoan = new Tai_Khoan()
            {
                ID = newID,
                ma = newMa,
                user_name = nv.user_name,
                pass_word = generatedPassword,
                cccd = nv.cccd,
                ho_ten = nv.ho_ten,
                ngay_sinh = nv.ngay_sinh,
                email = nv.email,
                sdt = nv.sdt,
                gioi_tinh = nv.gioi_tinh,
                ngay_tao = DateTime.Now,
                Vai_TroID = nhanVienRole.ID,
                hinh_anh = uniqueFileName // Nếu không có file upload thì sẽ là null
            };

            _context.tai_Khoans.Add(taikhoan);

            Dia_Chi dc = new Dia_Chi()
            {
                ID = Guid.NewGuid(),
                loai_dia_chi = 1,
                tinh = nv.tinh,
                huyen = nv.huyen,
                xa = nv.xa,
                dia_chi_chi_tiet = nv.dia_chi_chi_tiet,
                ngay_tao = DateTime.Now,
                Tai_KhoanID = newID
            };

            _context.dia_Chis.Add(dc);
            _context.SaveChanges();

            try
            {
                SendEmail(nv.email, nv.user_name, nv.pass_word);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần, nhưng record đã được lưu.
            }
            TempData["SuccessMessage"] = "Thêm nhân viên thành công!";
            Console.WriteLine(TempData["SuccessMessage"]); // Kiểm tra trong console

            return RedirectToAction("Index", "NhanVien", new { pageNumber = 1 });
        }

        private void SendEmail(string toEmail, string username, string password)
        {
            string fromEmail = "trangthph44337@fpt.edu.vn";  // Thay bằng Gmail của bạn
            string fromPassword = "fdqe bzsy cscd kerv"; // Thay bằng App Password (16 ký tự)

            string subject = "Tài khoản nhân viên mới";
            string body = $@"
            <p>Chào {username},</p>
            <p>Tài khoản của bạn đã được tạo thành công.</p>
            <p><b>Tên đăng nhập:</b> {username}</p>
            <p><b>Mật khẩu:</b> {password}</p>";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(fromEmail);
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false; // Bắt buộc phải đặt false
                    smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }





        //[HttpPost]
        //public IActionResult Create(NhanVien_Model nv, IFormFile HinhAnh)
        //{

        //    var nhanVienRole = _context.vai_Tros.FirstOrDefault(x => x.ten_vai_tro == "Nhân Viên");

        //    //bool isDuplicate = _context.tai_Khoans.Any(n =>
        //    //   n.email == nv.email || n.cccd == nv.cccd || n.sdt == nv.sdt);

        //    if (_context.tai_Khoans.Any(n => n.email == nv.email && n.Vai_TroID == nhanVienRole.ID))
        //    {
        //        ModelState.AddModelError("email", "Email đã tồn tại!");
        //    }
        //    if (_context.tai_Khoans.Any(n => n.cccd == nv.cccd))
        //    {
        //        ModelState.AddModelError("cccd", "Số CCCD đã tồn tại!");
        //    }
        //    if (_context.tai_Khoans.Any(n => n.sdt == nv.sdt))
        //    {
        //        ModelState.AddModelError("sdt", "Số điện thoại đã tồn tại!");
        //    }

        //    if (string.IsNullOrEmpty(nv.pass_word))
        //    {
        //        nv.pass_word = GenerateRandomPassword();
        //    }
        //    if (!ModelState.IsValid)
        //    {
        //        ViewData["Vai_TroID"] = new SelectList(_context.vai_Tros.ToList(), "ID", "ten_vai_tro");
        //        return View(nv);
        //    }

        //    string uniqueFileName = "default-avatar.png";

        //    if (HinhAnh != null && HinhAnh.Length > 0)
        //    {
        //        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/nhanvien");
        //        if (!Directory.Exists(uploadsFolder))
        //        {
        //            Directory.CreateDirectory(uploadsFolder);
        //        }
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(HinhAnh.FileName);
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            HinhAnh.CopyTo(fileStream);
        //        }
        //    }

        //    int newNumber = 1;
        //    try
        //    {
        //        // Lấy danh sách các mã nhân viên hiện có (đảm bảo mã không null và bắt đầu bằng "nv")
        //        var maList = _context.tai_Khoans
        //            .Where(x => x.ma != null && x.ma.StartsWith("nv"))
        //            .Select(x => x.ma)
        //            .ToList();

        //        int lastNumber = 0;
        //        foreach (var m in maList)
        //        {
        //            if (m.Length > 2 && int.TryParse(m.Substring(2), out int num))
        //            {
        //                if (num > lastNumber)
        //                    lastNumber = num;
        //            }
        //        }
        //        newNumber = lastNumber + 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine("Error generating newMa: " + ex.Message);
        //        newNumber = 1;
        //    }
        //    string newMa = "nv" + newNumber.ToString("D2");
        //    //endmanv 


        //    Guid newID = Guid.NewGuid();
        //    if (nhanVienRole == null)
        //    {
        //        return BadRequest("Vai trò Nhân Viên không tồn tại!");
        //    }

        //    //kiểm tra xem mật khẩu đã tồn tại hay chưa
        //    if (string.IsNullOrWhiteSpace(nv.pass_word))
        //    {
        //        nv.pass_word = GenerateRandomPassword();
        //    }
        //    string generatedPassword = nv.pass_word;

        //    var taikhoan = new Tai_Khoan()
        //    {
        //        ID = newID,
        //        ma = newMa,
        //        user_name = nv.user_name,
        //        pass_word = generatedPassword,
        //        cccd = nv.cccd,
        //        ho_ten = nv.ho_ten,
        //        ngay_sinh = nv.ngay_sinh,
        //        email = nv.email,
        //        sdt = nv.sdt,
        //        gioi_tinh = nv.gioi_tinh,
        //        ngay_tao=DateTime.Now,
        //        Vai_TroID = nhanVienRole.ID,
        //        hinh_anh = uniqueFileName
        //    };

        //    _context.tai_Khoans.Add(taikhoan);

        //    Dia_Chi dc = new Dia_Chi()
        //    {
        //        ID = Guid.NewGuid(),
        //        loai_dia_chi = 1,
        //        tinh = nv.tinh,
        //        huyen = nv.huyen,
        //        xa = nv.xa,

        //        dia_chi_chi_tiet = nv.dia_chi_chi_tiet,

        //        ngay_tao = DateTime.Now, // Sửa ở đây
        //        //ngay_sua = null,
        //        Tai_KhoanID = newID
        //    };

        //    _context.dia_Chis.Add(dc);
        //    _context.SaveChanges();

        //    try
        //    {
        //        SendEmail(nv.email, nv.user_name, nv.pass_word);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Bạn có thể log lỗi gửi email nếu cần, nhưng record đã được lưu.
        //    }

        //    return RedirectToAction("Index", "NhanVien", new { pageNumber = 1 });

        //}

      


        public string GenerateRandomPassword(int length = 8)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@#$!";
            Random random = new Random();
            return new string(Enumerable.Repeat(validChars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //[HttpGet("Edit")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVienRole = await _context.vai_Tros.FirstOrDefaultAsync(x => x.ten_vai_tro == "Nhân Viên");
            if (nhanVienRole == null)
            {
                return NotFound();
            }

            var nv = await _context.tai_Khoans
                .Where(x => x.Vai_TroID == nhanVienRole.ID && x.ID == id)
                .FirstOrDefaultAsync();

            if (nv == null)
            {
                return NotFound();
            }
            var idd = nv.ID;
            var diachi = _context.dia_Chis
                .FirstOrDefault(x => x.Tai_KhoanID == nv.ID && x.loai_dia_chi == 1);

            var nhanVienModel = new NhanVien_Model
            {
                Id = nv.ID,
                ma = nv.ma,
                user_name = nv.user_name,
                pass_word = nv.pass_word,
                ho_ten = nv.ho_ten,
                ngay_sinh = nv.ngay_sinh,
                gioi_tinh = nv.gioi_tinh,
                sdt = nv.sdt,
                email = nv.email,
                cccd = nv.cccd,
                hinh_anh = nv.hinh_anh,
                ngay_tao = nv.ngay_tao,
               
                // Gán riêng các thuộc tính địa chỉ
                tinh = diachi?.tinh,
                huyen = diachi?.huyen,
                xa = diachi?.xa,
                dia_chi_chi_tiet = diachi?.dia_chi_chi_tiet
            };

            return View(nhanVienModel);
        }

        [HttpPost]
        public IActionResult Edit(NhanVien_Model nv, IFormFile file)
        {



            var nhanVien = _context.tai_Khoans.FirstOrDefault(x => x.ID == nv.Id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            // Nếu có file mới, xử lý cập nhật ảnh
            if (file != null && file.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/nhanvien");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Xóa ảnh cũ nếu tồn tại
                if (!string.IsNullOrEmpty(nhanVien.hinh_anh))
                {
                    string oldFilePath = Path.Combine(uploadsFolder, nhanVien.hinh_anh);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Lưu file ảnh mới
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                nhanVien.hinh_anh = uniqueFileName;
            }

            // Cập nhật các thông tin nhân viên
            nhanVien.user_name = nv.user_name;
            nhanVien.pass_word = nv.pass_word;
            nhanVien.cccd = nv.cccd;
            nhanVien.ho_ten = nv.ho_ten;
            nhanVien.ngay_sinh = nv.ngay_sinh;
            nhanVien.email = nv.email;
            nhanVien.sdt = nv.sdt;
            nhanVien.gioi_tinh = nv.gioi_tinh;

            // Cập nhật thông tin địa chỉ (từ bảng dia_Chis)
            var diaChi = _context.dia_Chis.FirstOrDefault(x => x.Tai_KhoanID == nhanVien.ID && x.loai_dia_chi == 1);
            if (diaChi != null)
            {
                // Nếu đã có địa chỉ, cập nhật các trường
                diaChi.tinh = string.IsNullOrEmpty(nv.tinh) ? diaChi.tinh : nv.tinh;
                diaChi.huyen = string.IsNullOrEmpty(nv.huyen) ? diaChi.huyen : nv.huyen;
                diaChi.xa = string.IsNullOrEmpty(nv.xa) ? diaChi.xa : nv.xa;
                diaChi.dia_chi_chi_tiet = string.IsNullOrEmpty(nv.dia_chi_chi_tiet) ? diaChi.dia_chi_chi_tiet : nv.dia_chi_chi_tiet;

                diaChi.ngay_sua = DateTime.Now;
                _context.dia_Chis.Update(diaChi);
            }
            else
            {
                // Nếu địa chỉ chưa có, tạo mới và thêm vào context
                Dia_Chi newDiaChi = new Dia_Chi
                {
                    ID = Guid.NewGuid(),
                    loai_dia_chi = 1,
                    tinh = nv.tinh,
                    huyen = nv.huyen,
                    xa = nv.xa,
                    ngay_sua = DateTime.Now,
                    Tai_KhoanID = nhanVien.ID
                };
                _context.dia_Chis.Add(newDiaChi);
            }
            _context.tai_Khoans.Update(nhanVien);


            _context.SaveChanges();
            TempData["SuccessMessage"] = "Cập nhật nhân viên thành công!";
            // Sau khi update, chuyển hướng đến trang Details (hoặc Index nếu bạn muốn)
            return RedirectToAction("Index", new { id = nhanVien.ID });
        }





        // POST: Admin/NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var nv = await _context.tai_Khoans.FindAsync(id);
            if (nv != null)
            {
                _context.tai_Khoans.Remove(nv);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Exists(Guid id)
        {
            return _context.tai_Khoans.Any(e => e.ID == id);
        }
    }
}