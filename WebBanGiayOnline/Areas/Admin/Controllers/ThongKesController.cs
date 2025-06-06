﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClssLib;
using WebBanGiay.Data;
using Humanizer;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
    
namespace WebBanGiay.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(AuthenticationSchemes = "AdminScheme", Policy = "AdminOrEmployeePolicy")]
	public class ThongKesController : Controller
    {
        private readonly AppDbContext _context;

        public ThongKesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ThongKes

        public async Task<IActionResult> Index(Guid? id, DateTime? fromDate, DateTime? toDate)
        {
            var thongke = _context.thongKes
                .Include(x => x.San_Pham_Chi_Tiet)
                .OrderByDescending(x => x.SoLuongSP)
                .ToList();

            // Lấy thông tin sản phẩm bán chạy
            var sanPhamBanChay = _context.san_Pham_Chi_Tiets
                .Where(spct => spct.so_luong > 0)
                .Select(spct => new
                {
                    TenSanPham = spct.ten_SPCT,  // Tên sản phẩm
                    SoLuongBan = _context.don_Chi_Tiets
                                         .Where(hdct => hdct.San_Pham_Chi_TietID == spct.ID &&
                                                        (hdct.Hoa_Don.trang_thai == 4 || hdct.Hoa_Don.trang_thai == 6)) // Trạng thái bán online (4) hoặc bán tại quầy (6)
                                         .Sum(hdct => hdct.so_luong)  // Tính tổng số lượng bán cho sản phẩm
                })
                .OrderByDescending(sp => sp.SoLuongBan)  // Sắp xếp theo số lượng bán
                .Take(10)  // Lấy 10 sản phẩm bán chạy nhất
                .ToList();

            // Lấy thông tin số lượng tồn kho của sản phẩm và sắp xếp theo số lượng tồn giảm dần
            var sanPhamTonKho = _context.san_Pham_Chi_Tiets
                .Select(spct => new
                {
                    TenSanPham = spct.ten_SPCT,  // Tên sản phẩm
                    SoLuongTon = spct.so_luong -
                                 _context.don_Chi_Tiets
                                         .Where(hdct => hdct.San_Pham_Chi_TietID == spct.ID &&
                                                        (hdct.Hoa_Don.trang_thai == 4 || hdct.Hoa_Don.trang_thai == 6)) // Trừ đi số lượng bán online (4) hoặc tại quầy (6)
                                         .Sum(hdct => hdct.so_luong)  // Trừ đi số lượng bán
                })
                .OrderByDescending(sp => sp.SoLuongTon)  // Sắp xếp theo số lượng tồn kho giảm dần
                  .Take(10)
                .ToList();

            // Kiểm tra dữ liệu và gán kết quả vào ViewBag
            ViewBag.SanPhamBanChay = sanPhamBanChay;
            ViewBag.SanPhamTonKho = sanPhamTonKho;

            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (today.DayOfWeek == DayOfWeek.Sunday ? -6 : 1));
            var endOfWeek = startOfWeek.AddDays(6);
            var homNay = _context.hoa_Dons
                .Where(hd => hd.ngay_tao >= today && (hd.trang_thai == 3 || hd.trang_thai == 4|| hd.trang_thai == 5))
                .GroupBy(_ => 1)
                .Select(x => new ThongKe()
                {
                    TongTien = x.Sum(x => x.tong_tien),
                    DonThanhCong = x.Count(x => x.trang_thai == 3 || x.trang_thai == 5),
                    DonHuy = x.Count(x => x.trang_thai == 4),
                }).ToList();



            var tuanNay = _context.hoa_Dons
                .Where(hd => hd.ngay_tao >= startOfWeek && hd.ngay_tao <= endOfWeek && (hd.trang_thai == 3 || hd.trang_thai == 4 || hd.trang_thai == 5))
                .GroupBy(_ => 1)
                .Select(x =>
                       new ThongKe()
                       {
                           TongTien = x.Where(x => x.trang_thai == 3).Sum(x => x.tong_tien),
                           DonThanhCong = x.Count(x => x.trang_thai == 3 || x.trang_thai ==5),
                           DonHuy = x.Count(x => x.trang_thai == 4),
                       }).ToList();

            var thangNay = _context.hoa_Dons
                .Where(hd => hd.ngay_tao.Month == today.Month && hd.ngay_tao.Year == today.Year && (hd.trang_thai == 3 || hd.trang_thai == 4 || hd.trang_thai == 5))
                .GroupBy(_ => 1)
                .Select(x =>
                       new ThongKe()
                       {
                           TongTien = x.Where(x => x.trang_thai == 3).Sum(x => x.tong_tien),
                           DonThanhCong = x.Count(x => x.trang_thai == 3 || x.trang_thai == 5),
                           DonHuy = x.Count(x => x.trang_thai == 4),
                       }).ToList();

            var namNay = _context.hoa_Dons
                .Where(hd => hd.ngay_tao.Year == today.Year && (hd.trang_thai == 3 || hd.trang_thai == 4 || hd.trang_thai == 5))
                .GroupBy(_ => 1)
                .Select(x =>
new ThongKe()
{
    TongTien = x.Where(x => x.trang_thai == 3).Sum(x => x.tong_tien),
    DonThanhCong = x.Count(x => x.trang_thai == 3 || x.trang_thai == 5),
    DonHuy = x.Count(x => x.trang_thai == 4),
}).ToList();

            var thongKeList = new List<ThongKe>

    {
        new ThongKe
        {
            DoanhThu = today,
            TongTien = homNay.Sum(hd => hd.TongTien),
            SoLuongSP = homNay.Sum(hd => hd.SoLuongSP),
            DonThanhCong = homNay.Sum(hd => hd.DonThanhCong),
            DonHuy = homNay.Sum(hd => hd.DonHuy),
        },
        new ThongKe
        {
            DoanhThu = startOfWeek,
            TongTien = tuanNay.Sum(hd => hd.TongTien),
            SoLuongSP = tuanNay.Sum(hd => hd.SoLuongSP),
            DonThanhCong = tuanNay.Sum(hd => hd.DonThanhCong),
            DonHuy = tuanNay.Sum(hd => hd.DonHuy),
        },
        new ThongKe
        {
            DoanhThu = new DateTime(today.Year, today.Month, 1),
            TongTien = thangNay.Sum(hd => hd.TongTien),
            SoLuongSP = thangNay.Sum(hd => hd.SoLuongSP),
            DonThanhCong = thangNay.Sum(hd => hd.DonThanhCong),
            DonHuy = thangNay.Sum(hd => hd.DonHuy),
        },
        new ThongKe
        {
            DoanhThu = new DateTime(today.Year, 1, 1),
            TongTien = namNay.Sum(hd => hd.TongTien),
            SoLuongSP = namNay.Sum(hd => hd.SoLuongSP),
            DonThanhCong = namNay.Sum(hd => hd.DonThanhCong),
            DonHuy = namNay.Sum(hd => hd.DonHuy),
        }


    };
            return View(thongKeList);
        }


        [HttpPost]
        [Route("Getdata")]
        public async Task<IActionResult> Getdata(Guid? id)
        {

            var thongKe = _context.hoa_Dons
                .Where(x => x.trang_thai == 3 || x.trang_thai == 5)
               .GroupBy(x => x.ngay_tao)
               
                .Select(x => new
                {
                    DoanhThu = x.Key,
                    TongTien =  x.Sum(x => x.tong_tien),

                    DonThanhCong = x.Sum(x => x.trang_thai == 3 || x.trang_thai ==5 ? 1 : 0),
                    DonHuy = x.Sum(x => x.trang_thai == 4 ? 1 : 0),
                }).ToList();


            return Json(thongKe);
        }

        [HttpPost]
        [Route("filter")]
        public JsonResult filter(DateTime? fromDate, DateTime? toDate)
        {
            var data = _context.hoa_Dons
                .Where(hd => hd.ngay_tao >= fromDate && hd.ngay_tao <= toDate && (hd.trang_thai == 3 || hd.trang_thai == 4))
                .GroupBy(hd => hd.ngay_tao.Date)
                .Select(g => new
                {
                    doanhThu = g.Key.ToString("yyyy-MM-dd"), // Chuyển DateTime thành string
                    TongTien = g.Where(x => x.trang_thai == 3 || x.trang_thai == 5).Sum(x => x.tong_tien),

                    donThanhCong = g.Count(hd => hd.trang_thai == 3 || hd.trang_thai ==5),
                    donHuy = g.Count(hd => hd.trang_thai == 4)
                }).ToList();

            return Json(data);
        }




    }

}