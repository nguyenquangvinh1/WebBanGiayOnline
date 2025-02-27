using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClssLib;
using WebBanGiay.Data;
using Humanizer;

namespace WebBanGiay.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongKesController : Controller
    {
        private readonly AppDbContext _context;

        public ThongKesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ThongKes
        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (today.DayOfWeek == DayOfWeek.Sunday ? -6 : 1));
            var endOfWeek = startOfWeek.AddDays(6); 

            var homNay = _context.hoa_Dons
                .Where(hd => hd.ngay_tao >= today)
                .Select(hd => new ThongKe
                {
                    TongTien = _context.hoa_Dons.Where(hd => hd.trang_thai == 3).Sum(hd => hd.tong_tien),

                    DonThanhCong = hd.trang_thai == 3 ? 1 : 0,
                    DonHuy = hd.trang_thai == 4 ? 1 : 0
                }).ToList();

            var tuanNay = _context.hoa_Dons
                .Where(hd => hd.ngay_tao >= startOfWeek && hd.ngay_tao <= endOfWeek)
                .Select(hd => new ThongKe
                {
                    TongTien = _context.hoa_Dons.Where(hd => hd.trang_thai == 3).Sum(hd => hd.tong_tien),

                    DonThanhCong = hd.trang_thai == 3 ? 1 : 0,
                    DonHuy = hd.trang_thai == 4 ? 1 : 0
                }).ToList();

            var thangNay = _context.hoa_Dons
                .Where(hd => hd.ngay_tao.Month == today.Month && hd.ngay_tao.Year == today.Year)
                .Select(hd => new ThongKe
                {
                    TongTien = _context.hoa_Dons.Where(hd => hd.trang_thai == 3).Sum(hd => hd.tong_tien),


                    DonThanhCong = hd.trang_thai == 3 ? 1 : 0,
                    DonHuy = hd.trang_thai == 4 ? 1 : 0
                }).ToList();

            var namNay = _context.hoa_Dons
                .Where(hd => hd.ngay_tao.Year == today.Year)
                .Select(hd => new ThongKe
                {
                    TongTien = _context.hoa_Dons.Where(hd => hd.trang_thai == 3).Sum(hd => hd.tong_tien),

                    DonThanhCong = hd.trang_thai == 3 ? 1 : 0,
                    DonHuy = hd.trang_thai == 4 ? 1 : 0
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


    }
    }
