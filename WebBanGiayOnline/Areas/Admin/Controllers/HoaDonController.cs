using System;
using X.PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Mvc.Core;
using ClssLib;
using WebBanGiay.Data;
using System.Globalization;
using X.PagedList.Extensions;
using System.Security.Cryptography;
using NuGet.Common;
using System.Drawing;
using ClosedXML.Excel;
using WebBanGiay.ViewModel;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Net.Mail;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WebBanGiay.Helpers;
using System.Security.Claims;
using WebBanGiay.Models;
using Microsoft.AspNetCore.Authorization;


namespace WebBanGiay.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(AuthenticationSchemes = "AdminScheme", Policy = "AdminOrEmployeePolicy")]
	public class HoaDonController : Controller
    {
        private readonly AppDbContext _context;
        private readonly List<TTHD> tthd = new List<TTHD> { 
            new TTHD{ ID = 0, Name = "Chờ xử lý"},
            new TTHD{ ID = 1, Name = "Đã xác nhận"},
            new TTHD{ ID = 2, Name = "Đang giao hàng"},
            new TTHD{ ID = 3, Name = "Hoàn thành"},
            new TTHD{ ID = 4, Name = "Đã hủy"},
            new TTHD{ ID = 5, Name = "Đã Thanh Toán"},
            new TTHD{ ID = 6, Name = "Chưa Thanh Toán"}
        };

        public HoaDonController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/HoaDon
        public async Task<IActionResult> Index(int? page, string searchString, int? Category, DateTime? fromDate, DateTime? toDate)
        {

            int pageNumber = page ?? 1;
            int pageSize = 10; // Số hóa đơn trên mỗi trang

            var hoaDons = _context.hoa_Dons.AsQueryable();
            if (!fromDate.HasValue && !toDate.HasValue && string.IsNullOrEmpty(searchString) && !Category.HasValue)
            {
                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);
                hoaDons = hoaDons.Where(h => h.ngay_tao >= today && h.ngay_tao < tomorrow);
            }
            if (fromDate.HasValue)
            {
                hoaDons = hoaDons.Where(h => h.ngay_tao >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                hoaDons = hoaDons.Where(h => h.ngay_tao <= toDate.Value);
            }
            hoaDons = hoaDons.OrderBy(h => h.ngay_tao);

            if (fromDate >= toDate)
            {
                ViewBag.ThongBao = "❌ Ngày bắt đầu phải nhỏ hơn ngày kết thúc!";
            }



            if (!string.IsNullOrEmpty(searchString))
            {
                hoaDons = hoaDons.Where(h => h.MaHoaDon.Contains(searchString));
            }


            if (Category.HasValue)
            {
                hoaDons = hoaDons.Where(h => h.trang_thai == Category);
            }


            foreach (var hoaDon in hoaDons)
            {
                if (string.IsNullOrEmpty(hoaDon.MaHoaDon)) // Nếu chưa có mã
                {
                    hoaDon.MaHoaDon = GenerateNewHoaDonID();
                }

            }



            // Đổ dữ liệu vào SelectList để hiển thị dropdown
            ViewBag.TrangThaiList = new SelectList(new List<SelectListItem>
    {
        new SelectListItem { Value = "0", Text = "Chờ xử lý" },
        new SelectListItem { Value = "1", Text = "Đã xác nhận" },
        new SelectListItem { Value = "2", Text = "Đang giao hàng" },
        new SelectListItem { Value = "3", Text = "Hoàn thành" },
        new SelectListItem { Value = "4", Text = "Đã hủy" },
        new SelectListItem { Value = "5", Text = "Chưa thanh toán" },
        new SelectListItem { Value = "6", Text = "Đã thanh toán" },
    }, "Value", "Text", Category.ToString());


            var pagedList = hoaDons.ToPagedList(pageNumber, pageSize);

            return View("Index", pagedList); // Trả về View Index để dùng lại giao diện
        }

        public IActionResult RemoveBill(Guid id)
        {
            var hoadonChiTiet = _context.don_Chi_Tiets.FirstOrDefault(x => x.ID == id);
            if (hoadonChiTiet == null)
                return RedirectToAction("Index");

            var hoaDon = _context.hoa_Dons.FirstOrDefault(h => h.ID == hoadonChiTiet.Hoa_DonID);

        
            if (hoaDon != null && (hoaDon.trang_thai == 2 ||hoaDon.trang_thai==1 || hoaDon.trang_thai == 3 || hoaDon.trang_thai == 4 || hoaDon.trang_thai == 5))
            {
                TempData["Error"] = "Không thể xóa sản phẩm vì hóa đơn đã xử lý.";
                return RedirectToAction("Details", new { id = hoaDon.ID });
            }

       
            var sanPhamId = hoadonChiTiet.San_Pham_Chi_TietID;
            var soLuong = hoadonChiTiet.so_luong;
            var soTien = hoadonChiTiet.thanh_tien;

            _context.don_Chi_Tiets.Remove(hoadonChiTiet);

            _context.SaveChanges();


            if (hoaDon != null && soTien.HasValue)
            {
                hoaDon.tong_tien -= soTien.Value;
                _context.hoa_Dons.Update(hoaDon);

                _context.SaveChanges();
            }

        
            var sanPhamChiTiet = _context.san_Pham_Chi_Tiets.FirstOrDefault(sp => sp.ID == sanPhamId);
            if (sanPhamChiTiet != null)
            {
                sanPhamChiTiet.so_luong += soLuong;
                _context.san_Pham_Chi_Tiets.Update(sanPhamChiTiet);

                _context.SaveChanges();
            }
            var soLuongConLai = _context.don_Chi_Tiets.Count(x => x.Hoa_DonID == hoaDon.ID);
            if (soLuongConLai == 0)
            {
                hoaDon.trang_thai = 4; 
                _context.hoa_Dons.Update(hoaDon);

                _context.SaveChanges();


            }



            if (hoaDon != null)
                return RedirectToAction("Details", new { id = hoaDon.ID });

            return RedirectToAction("Index");
        }



        // GET: Admin/HoaDon/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            var hoaDon = _context.hoa_Dons
       .Include(h => h.Hoa_Don_Chi_Tiets)
       .ThenInclude(H =>H.San_Pham_Chi_Tiet)
      .Include(h => h.Thanh_Toans)
      .Include(h =>h.Giam_Gia)
      .Include( h => h.Tai_Khoan_Hoa_Dons)
       .FirstOrDefault(h => h.ID == id) ?? new Hoa_Don();

          

            hoaDon.Hoa_Don_Chi_Tiets = hoaDon.Hoa_Don_Chi_Tiets ?? new List<Hoa_Don_Chi_Tiet>();
            ViewBag.TrangThaiList = new List<SelectListItem>
{
    new SelectListItem { Value = "0", Text = "Chờ xử lý" },
    new SelectListItem { Value = "1", Text = "Đã xác nhận" },
    new SelectListItem { Value = "2", Text = "Đang giao hàng" },
    new SelectListItem { Value = "3", Text = "Hoàn thành" },
    new SelectListItem { Value = "4", Text = "Đã hủy" },  

    
};

            if (hoaDon == null)
            {
                return NotFound();
            }





            return View(hoaDon);
        }


        [HttpPost]
        public IActionResult UpdateQuantity(Guid id, int quantity, double newPrice)
        {
            var cart = _context.don_Chi_Tiets.FirstOrDefault(z => z.ID == id);
            
            if (cart != null)
            {
                var hoaDon = _context.hoa_Dons.FirstOrDefault(h => h.ID == cart.Hoa_DonID);

                if (hoaDon != null && (hoaDon.trang_thai == 2 || hoaDon.trang_thai == 3 || hoaDon.trang_thai == 4 || hoaDon.trang_thai == 5))
                {
                    return Json(new { success = false, message = "Trạng đơn hàng không cho phép bạn thay đổi số lượng !" });
                }

                var sanPham = _context.san_Pham_Chi_Tiets.FirstOrDefault(sp => sp.ID == cart.San_Pham_Chi_TietID);
                if (sanPham == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm trong kho!" });
                }

                // Nếu giá mới khác giá cũ => tạo sản phẩm mới thay vì cập nhật dòng hiện tại
                if (sanPham.gia != newPrice)
                {
                    // Kiểm tra số lượng đủ
                    newPrice = sanPham.gia;
                    int soLuongCu = cart.so_luong;
                    int chenhlech = quantity - soLuongCu;

                    // Trừ số lượng sản phẩm trong kho
                    sanPham.so_luong -= quantity;
                    _context.Update(sanPham);

              
                    // Tạo sản phẩm mới với giá mới
                    var newCart = new Hoa_Don_Chi_Tiet()
                    {
                        ID = Guid.NewGuid(),
                        Hoa_DonID = cart.Hoa_DonID,
                        San_Pham_Chi_TietID = cart.San_Pham_Chi_TietID,
                        so_luong = chenhlech,
                        gia = newPrice,
                        thanh_tien = chenhlech * newPrice,
                        ma = cart.ma
                    };

                    _context.Add(newCart);
                    _context.SaveChanges();

                    // Cập nhật tổng tiền hóa đơn
                    var tongTienMoi = _context.don_Chi_Tiets
                                        .Where(z => z.ma == hoaDon.MaHoaDon)
                                        .Sum(z => z.gia * z.so_luong);

                    hoaDon.tong_tien = tongTienMoi;
                    _context.Update(hoaDon);
                    _context.SaveChanges();

                    return Json(new
                    {
                        success = true,
                        newTotal = newCart.thanh_tien,
                        totalCart = hoaDon.tong_tien
                    });
                }
                else
                {
                    int soLuongCu = cart.so_luong;
                    int chenhlech = quantity - soLuongCu;

                    if (chenhlech > 0)
                    {
                        // Tăng số lượng => cần kiểm tra kho
                        if (chenhlech > sanPham.so_luong)
                        {
                            return Json(new { success = false, message = "Số lượng sản phẩm không đủ trong kho!" });
                        }
                        sanPham.so_luong -= chenhlech;
                    }
                    else if (chenhlech < 0)
                    {
                        // Giảm số lượng => hoàn lại vào kho
                        sanPham.so_luong += Math.Abs(chenhlech);
                    }


                    _context.Update(sanPham);

                    cart.so_luong = quantity;
                    cart.thanh_tien = cart.gia * quantity;

                    _context.Update(cart);
                    _context.SaveChanges();

                    if (hoaDon != null)
                    {
                        var tongTienMoi = _context.don_Chi_Tiets
                                            .Where(z => z.ma == hoaDon.MaHoaDon)
                                            .Sum(z => z.gia * z.so_luong);

                        hoaDon.tong_tien = tongTienMoi;
                        _context.Update(hoaDon);
                        _context.SaveChanges();
                    }

                    return Json(new
                    {
                        success = true,
                        newTotal = cart.thanh_tien,
                        totalCart = hoaDon?.tong_tien ?? cart.thanh_tien
                    });
                }
            }

            return Json(new { success = false, message = "Không tìm thấy sản phẩm trong giỏ hàng!" });
        }



        [HttpPost]
        public IActionResult CapNhatThongTin(Hoa_Don model)
        {
            var hoaDon = _context.hoa_Dons.FirstOrDefault(h => h.ID == model.ID);
            if (hoaDon == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin
            hoaDon.ten_nguoi_nhan = model.ten_nguoi_nhan;
            hoaDon.sdt_nguoi_nhan = model.sdt_nguoi_nhan;
            hoaDon.email_nguoi_nhan = model.email_nguoi_nhan;
            hoaDon.dia_chi = model.dia_chi;
            hoaDon.ngay_sua = DateTime.Now;
            hoaDon.nguoi_sua = User.Identity?.Name ?? "Không xác định";
            _context.Update(hoaDon);
            _context.SaveChanges();

            TempData["ThongBao"] = "Cập nhật thông tin thành công!";
            return RedirectToAction("Details", new { id = model.ID });
        }




        [HttpPost]
        public JsonResult UpdateStatus(Guid? id, int newStatus, string reason)
        {
            var hoaDon = _context.hoa_Dons.FirstOrDefault(h => h.ID == id);

            var cart = _context.don_Chi_Tiets.FirstOrDefault(z => z.Hoa_DonID == hoaDon.ID);
         
            var sanPham = _context.san_Pham_Chi_Tiets.FirstOrDefault(sp => sp.ID == cart.San_Pham_Chi_TietID);
            if (hoaDon != null) 
            {
                hoaDon.trang_thai = newStatus;
                _context.Update(hoaDon);

                string Id = User.FindFirst("userid")?.Value ?? "Unknown";
                string name = User.Identity?.Name ?? "Unknown";
                string role = User.FindFirstValue(ClaimTypes.Role) ?? "Unknown";
                TTHD tTHD = tthd.FirstOrDefault(x => x.ID == newStatus);
                var link = new Tai_Khoan_Hoa_Don
                {
                    ID = Guid.NewGuid(),
                    Tai_KhoanID = Guid.Parse(Id),
                    Hoa_DonID = hoaDon.ID,
                    ngay_tao = DateTime.Now,
                    Ten = name,           // Gán tên NV
                    vai_tro = role, // Gán vai trò
                    thao_tac = "Chuyển trạng thái Hóa Đơn thành: " + tTHD.Name,
                    ghi_chu = reason
                };
                if (newStatus == 1)
                {
                    if (sanPham != null && cart != null)
                    {
                        if (sanPham.so_luong >= cart.so_luong)
                        {
                            sanPham.so_luong -= cart.so_luong;
                            _context.Update(sanPham);
                        }
                        else
                        {
                            return Json(new { success = false, message = "Số lượng sản phẩm không đủ trong kho!" });
                        }
                    }
                }

           
                _context.tai_Khoan_Hoa_Dons.Add(link);

                _context.SaveChanges();



                try
                {
                    SendEmail(hoaDon.email_nguoi_nhan,hoaDon.trang_thai , hoaDon.ten_nguoi_nhan,hoaDon.MaHoaDon);
                }
                catch (Exception ex)
                {
                   
                }



                return Json(new
                {
                    success = true,
                    status = newStatus,
                    message = "Cập nhật trạng thái thành công!"
                });
            }


            return Json(new { success = false, message = "Cập nhật thất bại!" });
        }
        private void SendEmail(string toEmail, int status, string username ,string ma)
        {
            var hoadon = _context.hoa_Dons.FirstOrDefault(x => x.trang_thai == status);
            string GetOrderStatusName(int status)
            {
                switch (status)
                {
                    case 0: return "Chờ xử lý";
                    case 1: return "Đã xác nhận";
                    case 2: return "Đang giao hàng";
                    case 3: return "Hoàn thành";
                    case 4: return "Đã hủy";
                    default: return "Không xác định";
                }
            }
            string orderStatus = GetOrderStatusName(hoadon.trang_thai);
            string fromEmail = "trangthph44337@fpt.edu.vn";  // Thay bằng Gmail của bạn
            string fromPassword = "fdqe bzsy cscd kerv"; // Thay bằng App Password (16 ký tự)
            var time = DateTime.Now;
            string subject = $@"Cập nhập trạng thái đơn hàng {ma} của bạn ";
            string body = $@"
            <p>Chào {username},</p>
            <p>Vào lúc {time} </p>
            <p>Trạng thái đơn hàng của bạn {orderStatus}. </p>
            <p>Đơn hàng đang trong quá trính gửi đi hãy để ý đơn hàng nhé .</p>
             <p> Adidas Shop rất cảm ơn vì sự ủng hộ của bạn .</p>
 <p>Nếu có thắc mắc hay muốn thay đổi đơn hàng hãy goi Hotline : <strong> 0865884051 </strong> </p>";


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


        // GET: Admin/HoaDon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/HoaDon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,tong_tien,ghi_chu,trang_thai,dia_chi,sdt_nguoi_nhan,email_nguoi_nhan,loai_hoa_don,ten_nguoi_nhan,thoi_gian_nhan_hang,ngay_tao,ngay_sua,nguoi_tao,nguoi_sua")] Hoa_Don hoa_Don)
        {
            if (string.IsNullOrEmpty(hoa_Don.MaHoaDon))
            {
                hoa_Don.MaHoaDon = GenerateNewHoaDonID();
            }


            _context.Add(hoa_Don);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));



        }

        // GET: Admin/HoaDon/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoa_Don = await _context.hoa_Dons.FindAsync(id);
            if (hoa_Don == null)
            {
                return NotFound();
            }
            return View(hoa_Don);
        }

        
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,tong_tien,ghi_chu,trang_thai,dia_chi,sdt_nguoi_nhan,email_nguoi_nhan,loai_hoa_don,ten_nguoi_nhan,thoi_gian_nhan_hang,ngay_tao,ngay_sua,nguoi_tao,nguoi_sua")] Hoa_Don hoa_Don)
        {
            if (id != hoa_Don.ID)
            {
                return NotFound();
            }


            try
            {


                _context.Update(hoa_Don);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Hoa_DonExists(hoa_Don.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }





        // GET: Admin/HoaDon/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoa_Don = await _context.hoa_Dons
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hoa_Don == null)
            {
                return NotFound();
            }

            return View(hoa_Don);
        }

        // POST: Admin/HoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var hoa_Don = await _context.hoa_Dons.FindAsync(id);
            if (hoa_Don != null)
            {
                _context.hoa_Dons.Remove(hoa_Don);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Hoa_DonExists(Guid id)
        {
            return _context.hoa_Dons.Any(e => e.ID == id);
        }


        private string GenerateNewHoaDonID()
        {
            // Tìm hóa đơn có ID lớn nhất
            var lastHoaDon = _context.hoa_Dons
                .OrderByDescending(h => h.MaHoaDon)
                .FirstOrDefault();


            int newID = 1;

            if (lastHoaDon != null)
            {

                string lastID = lastHoaDon.MaHoaDon.Replace("HD", "");
                if (int.TryParse(lastID, out int lastNumber))
                {
                    newID = lastNumber + 1;
                }
            }

            // Format lại thành dạng "HD001"
            return $"HD{newID:D5}"; // 5 chữ số, VD: HD00001
        }

        public ActionResult ExportExcel()
        {
            var hoaDonList = _context.hoa_Dons.ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Hóa Đơn");
                var currentRow = 1;



                worksheet.Cell(currentRow, 1).Value = "Mã Hóa Đơn";
                worksheet.Cell(currentRow, 2).Value = "Người Đặt";
                worksheet.Cell(currentRow, 3).Value = "Số điện thoại"; worksheet.Cell(currentRow, 4).Value = "Nhân viên";
                worksheet.Cell(currentRow, 5).Value = "Ngày tạo";
                worksheet.Cell(currentRow, 6).Value = "Loại Hóa Đơn";
                worksheet.Cell(currentRow, 7).Value = "Trang thại";
                worksheet.Cell(currentRow, 8).Value = "Tổng Tiền";

                // Định dạng tiêu đề
                worksheet.Row(currentRow).Style.Font.Bold = true;
                worksheet.Row(currentRow).Style.Fill.BackgroundColor = XLColor.LightGray;


                foreach (var hoaDon in hoaDonList)
                {
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = hoaDon.MaHoaDon;
                    worksheet.Cell(currentRow, 2).Value = hoaDon.ten_nguoi_nhan;
                    worksheet.Cell(currentRow, 3).Value = hoaDon.sdt_nguoi_nhan;
                    worksheet.Cell(currentRow, 4).Value = hoaDon.nguoi_tao;
                    worksheet.Cell(currentRow, 5).Value = hoaDon.ngay_tao.ToString("dd/MM/yyyy");
                    string loaiHoaDonText = hoaDon.loai_hoa_don == 1 ? "Tại Quầy" : "Online";
                    worksheet.Cell(currentRow, 6).Value = loaiHoaDonText;
                    string trangThaiText = hoaDon.trang_thai switch
                    {
                        0 => "Chờ Xử Lý",
                        1 => "Đã Xác Nhận",
                        2 => "Đang Giao Hàng",
                        3 => "Hoàn Thành",
                        4 => "Đã Hủy",
                        _ => "Không Xác Định"
                    };
                    worksheet.Cell(currentRow, 7).Value = trangThaiText;
                    worksheet.Cell(currentRow, 8).Value = hoaDon.tong_tien;
                }


                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var fileName = $"HoaDon_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }






        }
        [HttpPost]
        public JsonResult HuyDonHang(Guid? id)
        {
            var hoaDon = _context.hoa_Dons
                .Include(h => h.Hoa_Don_Chi_Tiets) 
                .FirstOrDefault(h => h.ID == id);
        

            if (hoaDon == null)
            {
                return Json(new { success = false, message = "Không tìm thấy đơn hàng!" });
            }

            if (hoaDon.trang_thai == 3 || hoaDon.trang_thai == 2 || hoaDon.trang_thai == 5)
            {
                return Json(new { success = false, message = "Không thể hủy đơn hàng đã hoàn thành hoặc đang giao hàng!" });
            }

            if (hoaDon.trang_thai == 4)
            {
                return Json(new { success = false, message = "Đơn hàng đã bị hủy, không thể thay đổi trạng thái!" });
            }

            foreach (var ct in hoaDon.Hoa_Don_Chi_Tiets)
            {
                var sanPhamChiTiet = _context.san_Pham_Chi_Tiets.FirstOrDefault(sp => sp.ID == ct.San_Pham_Chi_TietID);
                if (sanPhamChiTiet != null)
                {
                    sanPhamChiTiet.so_luong += ct.so_luong;
                    _context.san_Pham_Chi_Tiets.Update(sanPhamChiTiet);
                }
            }
            var giamgia = _context.phieu_Giam_Gias.FirstOrDefault(x => x.ID == hoaDon.Giam_GiaID);
            foreach (var ct in hoaDon.Hoa_Don_Chi_Tiets)
            {
                if (giamgia != null)
                {
                    giamgia.so_luong += ct.so_luong;
                    giamgia.gia_tri_giam = 0;
                }
            }
            if (hoaDon.Ship != null)
            {
                hoaDon.Ship = 0;
            }

            // Cập nhật trạng thái đơn hàng
            hoaDon.tong_tien = 0;
            hoaDon.trang_thai = 4;
        
            

            _context.hoa_Dons.Update(hoaDon);
            _context.SaveChanges();

            Console.WriteLine($"Hủy đơn hàng: {hoaDon.ID} - Tổng tiền = {hoaDon.tong_tien}");

            return Json(new { success = true });
        }



        //[HttpPost]
        //public IActionResult CreateInvoice()
        //{
        //    try
        //    {
        //        var newInvoice = new Hoa_Don
        //        {
        //            ID = Guid.NewGuid(),            
        //            MaHoaDon = GenerateNewHoaDonID(), 
        //            trang_thai = 0,                 
        //            ngay_tao = DateTime.Now
        //        };

        //        _context.SaveChanges(); // Lưu vào DB

        //        return Json(new {
        //            success = true,
        //            invoiceId = newInvoice.ID, // Trả về ID hóa đơn mới tạo
        //            message = "Tạo hóa đơn mới thành công!"
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new {
        //            success = false,
        //            message = "Lỗi khi tạo hóa đơn: " + ex.Message
        //        });
        //    }
        //}
        //public IActionResult GetInvoiceById(Guid id)
        //{
        //    var invoice = _context.hoa_Dons
        //                          .Include(h => h.Hoa_Don_Chi_Tiets) // Lấy luôn chi tiết sản phẩm
        //                          .FirstOrDefault(h => h.ID == id);

        //    if (invoice == null)
        //        return Json(new { success = false, message = "Không tìm thấy hóa đơn" });

        //    return Json(new
        //    {
        //        success = true,
        //        invoice = invoice
        //    });
        //}

        [HttpPost]
        public IActionResult Detial(Guid id, bool sua = false)
        {
            var hoaDon = _context.hoa_Dons.Find(id);
            ViewBag.ShowForm = sua && hoaDon.trang_thai < 2 || hoaDon.trang_thai == -1;
            return View(hoaDon);
        }



    }


}

