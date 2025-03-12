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
namespace WebBanGiay.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HoaDonController : Controller
    {
        private readonly AppDbContext _context;

        public HoaDonController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/HoaDon
        public async Task<IActionResult> Index(int? page, string searchString, int? Category, DateTime? fromDate, DateTime? toDate)
        {

            int pageNumber = page ?? 1;
            int pageSize = 5; // Số hóa đơn trên mỗi trang

            var hoaDons = _context.hoa_Dons.AsQueryable();
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
        new SelectListItem { Value = "4", Text = "Đã hủy" }
    }, "Value", "Text", Category.ToString());


            var pagedList = hoaDons.ToPagedList(pageNumber, pageSize);

            return View("Index", pagedList); // Trả về View Index để dùng lại giao diện
        }



        // GET: Admin/HoaDon/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            var order = _context.hoa_Dons
            .Include(o => o.Tai_Khoan_Hoa_Dons)
            .ThenInclude(tkh => tkh.Tai_Khoan)
            .FirstOrDefault(o => o.ID == id);
            ViewBag.TrangThaiList = new List<SelectListItem>
{
    new SelectListItem { Value = "0", Text = "Chờ xử lý" },
    new SelectListItem { Value = "1", Text = "Đã xác nhận" },
    new SelectListItem { Value = "2", Text = "Đang giao hàng" },
    new SelectListItem { Value = "3", Text = "Hoàn thành" },
    new SelectListItem { Value = "4", Text = "Đã hủy" }
};

            if (order == null)
            {
                return NotFound();
            }





            return View(order);
        }

        [HttpPost]
        public JsonResult UpdateStatus(Guid? id, int newStatus)
        {
            var hoaDon = _context.hoa_Dons.FirstOrDefault(h => h.ID == id);
            if (hoaDon != null) // Đảm bảo trạng thái hợp lệ
            {
                hoaDon.trang_thai = newStatus;
                _context.Update(hoaDon);
                _context.SaveChanges();



                return Json(new
                {
                    success = true,
                    status = newStatus,
                    message = "Cập nhật trạng thái thành công!"
                });
            }


            return Json(new { success = false, message = "Cập nhật thất bại!" });
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
                worksheet.Cell(currentRow, 3).Value = "Số điện thoại";
                worksheet.Cell(currentRow, 4).Value = "Ngày tạo";
                worksheet.Cell(currentRow, 5).Value = "Loại Hóa Đơn";
                worksheet.Cell(currentRow, 6).Value = "Trang thại";
                worksheet.Cell(currentRow, 7).Value = "Tổng Tiền";

                // Định dạng tiêu đề
                worksheet.Row(currentRow).Style.Font.Bold = true;
                worksheet.Row(currentRow).Style.Fill.BackgroundColor = XLColor.LightGray;


                foreach (var hoaDon in hoaDonList)
                {
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = hoaDon.MaHoaDon;
                    worksheet.Cell(currentRow, 2).Value = hoaDon.ten_nguoi_nhan;
                    worksheet.Cell(currentRow, 3).Value = hoaDon.sdt_nguoi_nhan;
                    worksheet.Cell(currentRow, 4).Value = hoaDon.ngay_tao.ToString("dd/MM/yyyy");
                    string loaiHoaDonText = hoaDon.loai_hoa_don == 1 ? "Tại Quầy" : "Giao Hàng";
                    worksheet.Cell(currentRow, 5).Value = loaiHoaDonText;
                    string trangThaiText = hoaDon.trang_thai switch
                    {
                        0 => "Chờ Xử Lý",
                        1 => "Đã Xác Nhận",
                        2 => "Đang Giao Hàng",
                        3 => "Hoàn Thành",
                        4 => "Đã Hủy",
                        _ => "Không Xác Định"
                    };
                    worksheet.Cell(currentRow, 6).Value = trangThaiText;
                    worksheet.Cell(currentRow, 7).Value = hoaDon.tong_tien;
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
        //[HttpPost]
        //public JsonResult HuyDonHang(Guid? id)
        //{
        //    var hoaDon = _context.hoa_Dons.FirstOrDefault(h => h.ID == id);
        //    if (hoaDon == null)
        //    {
        //        return Json(new { success = false, message = "Không tìm thấy đơn hàng!" });
        //    }


        //    if (hoaDon.trang_thai == 3 || hoaDon.trang_thai == 2) // Hoàn thành hoặc Đang giao hàng
        //    {
        //        return Json(new { success = false, message = "Không thể hủy đơn hàng đã hoàn thành hoặc đang giao hàng!" });
        //    }
        //    if (hoaDon.trang_thai == 4)
        //    {

        //        return Json(new { success = false, message = "Đơn hàng đã bị hủy, không thể thay đổi trạng thái!" });
        //    }
        //    hoaDon.tong_tien = 0;
        //    hoaDon.trang_thai = 4;



        //    _context.hoa_Dons.Update(hoaDon);
        //    _context.SaveChanges();
        //    Console.WriteLine($"Hủy đơn hàng: {hoaDon.ID} - Sau khi cập nhật: Tổng tiền = {hoaDon.tong_tien}");


        //    return Json(new { success = true });
        //}


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


       


    }


}

