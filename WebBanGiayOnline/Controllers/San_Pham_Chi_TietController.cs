using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClssLib;
using WebBanGiay.Data;
using WebBanGiay.Models;
using System.ComponentModel;

namespace WebBanGiay.Controllers
{
    public class San_Pham_Chi_TietController : Controller
    {
        private readonly AppDbContext _context;

        public San_Pham_Chi_TietController(AppDbContext context)
        {
            _context = context;
        }

        // GET: San_Pham_Chi_Tiet
        public async Task<IActionResult> Index()
        {
            var sanPham = await _context.san_Phams
                .Include(x => x.Loai_Giay)
                .AsNoTracking().ToListAsync();
            var ViewMod = new List<HangHoaVM>();
            foreach (var item in sanPham)
            {
                // Lấy ảnh sản phẩm (nếu không có, dùng ảnh mặc định)
                var anhSanPham = _context.anh_San_Phams.FirstOrDefault(x => x.San_PhamID == item.ID);
                string imgUrl = anhSanPham?.anh_url ?? "img/default.png";



                // Lấy giá cao nhất (nếu không có, mặc định = 0)
                var chiTietSanPham = _context.san_Pham_Chi_Tiets
                    .Where(z => z.San_PhamID == item.ID)
                    .OrderByDescending(x => x.gia)
                    .FirstOrDefault();

                double gia = chiTietSanPham?.gia ?? 0;
                HangHoaVM hang = new HangHoaVM()
                {
                    ID = item.ID,
                    TenHH = item.ten_san_pham,
                    DonGia = gia,
                    MoTa = item.mo_ta,
                    Hinh = imgUrl ,
                    TenLoai = item.Loai_Giay.ten_loai_giay
                };
                ViewMod.Add(hang);
            }
            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus.ToList(), "ID", "ten_chat_lieu");
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays.ToList(), "ID", "ten_loai_co_giay");
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs.ToList(), "ID", "ten_danh_muc");
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays.ToList(), "ID", "ten_de_giay");
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays.ToList(), "ID", "ten_mui_giay");
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs.ToList(), "ID", "ten_kieu_dang");
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays.ToList(), "ID", "ten_loai_giay");
            return View(ViewMod);
        }
        [HttpGet]
        public IActionResult Filter(string chatLieu, string coGiay, string danhMuc, string deGiay, string muiGiay, string kieuDang, string loaiGiay)
        {
            Console.WriteLine($"📌 Nhận giá trị lọc: chatLieu={chatLieu}, coGiay={coGiay}, danhMuc={danhMuc}, deGiay={deGiay}, muiGiay={muiGiay}, kieuDang={kieuDang}, loaiGiay={loaiGiay}");

            var query = _context.san_Phams.AsQueryable();

            if (!string.IsNullOrEmpty(chatLieu))
                query = query.Where(sp => sp.Chat_LieuID.ToString() == chatLieu);

            if (!string.IsNullOrEmpty(coGiay))
                query = query.Where(sp => sp.Co_GiayID.ToString() == coGiay);

            if (!string.IsNullOrEmpty(danhMuc))
                query = query.Where(sp => sp.Danh_MucID.ToString() == danhMuc);

            if (!string.IsNullOrEmpty(deGiay))
                query = query.Where(sp => sp.De_GiayID.ToString() == deGiay);

            if (!string.IsNullOrEmpty(muiGiay))
                query = query.Where(sp => sp.Mui_GiayID.ToString() == muiGiay);

            if (!string.IsNullOrEmpty(kieuDang))
                query = query.Where(sp => sp.Kieu_DangID.ToString() == kieuDang);

            if (!string.IsNullOrEmpty(loaiGiay))
                query = query.Where(sp => sp.Loai_GiayID.ToString() == loaiGiay);

            var result = query
                .Include(x => x.Chat_Lieu)
                .Include(x => x.Co_Giay)
                .Include(x => x.Danh_Muc)
                .Include(x => x.De_Giay)
                .Include(x => x.Kieu_Dang)
                .Include(x => x.Loai_Giay)
                .Include(x => x.Mui_Giay)
                .AsNoTracking()
                .ToList();

            Console.WriteLine($"🔍 Tổng sản phẩm tìm thấy: {result.Count}");

            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus.ToList(), "ID", "ten_chat_lieu");
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays.ToList(), "ID", "ten_loai_co_giay");
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs.ToList(), "ID", "ten_danh_muc");
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays.ToList(), "ID", "ten_de_giay");
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays.ToList(), "ID", "ten_mui_giay");
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs.ToList(), "ID", "ten_kieu_dang");
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays.ToList(), "ID", "ten_loai_giay");

            return View("Index", result); // Trả về danh sách đã lọc
        }

        // GET: San_Pham_Chi_Tiet/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            var data1 = _context.san_Phams.Include(x => x.Loai_Giay).FirstOrDefault(x => x.ID == id);
            if(data1 == null)
            {
                TempData["Message"] = $"Không tìm thấy {id}";
                return Redirect("/404");
            }
            var result = new ChiTietHangHoaVM
            {
                ID = data1.ID,
                TenHH = data1.ten_san_pham,
                DonGia = _context.san_Pham_Chi_Tiets.Where(z => z.San_PhamID == data1.ID).Select(x => x.gia).Min(),
                MoTa = data1.mo_ta,
                SoLuongTon = _context.san_Pham_Chi_Tiets.Where(z => z.San_PhamID == data1.ID).Select(x => x.so_luong).Min(),
                TenLoai = data1.Loai_Giay.ten_loai_giay,
                ChiTiet = data1.mo_ta

            };
            return View(result);
        }

        // GET: San_Pham_Chi_Tiet/Create
        public IActionResult Create()
        {
            ViewData["Kich_ThuocID"] = new SelectList(_context.kich_Thuocs, "ID", "ten_kich_thuoc");
            ViewData["Mau_SacID"] = new SelectList(_context.mau_Sacs, "ID", "ma_mau");
            ViewData["San_PhamID"] = new SelectList(_context.san_Phams, "ID", "mo_ta");
            return View();
        }

        // POST: San_Pham_Chi_Tiet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MaSP,moTa,ten_SPCT,gia,so_luong,trang_thai,ngay_tao,ngay_sua,Kich_ThuocID,Mau_SacID,San_PhamID")] San_Pham_Chi_Tiet san_Pham_Chi_Tiet)
        {
            if (ModelState.IsValid)
            {
                san_Pham_Chi_Tiet.ID = Guid.NewGuid();
                _context.Add(san_Pham_Chi_Tiet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Kich_ThuocID"] = new SelectList(_context.kich_Thuocs, "ID", "ten_kich_thuoc", san_Pham_Chi_Tiet.Kich_ThuocID);
            ViewData["Mau_SacID"] = new SelectList(_context.mau_Sacs, "ID", "ma_mau", san_Pham_Chi_Tiet.Mau_SacID);
            ViewData["San_PhamID"] = new SelectList(_context.san_Phams, "ID", "mo_ta", san_Pham_Chi_Tiet.San_PhamID);
            return View(san_Pham_Chi_Tiet);
        }

        // GET: San_Pham_Chi_Tiet/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var san_Pham_Chi_Tiet = await _context.san_Pham_Chi_Tiets.FindAsync(id);
            if (san_Pham_Chi_Tiet == null)
            {
                return NotFound();
            }
            ViewData["Kich_ThuocID"] = new SelectList(_context.kich_Thuocs, "ID", "ten_kich_thuoc", san_Pham_Chi_Tiet.Kich_ThuocID);
            ViewData["Mau_SacID"] = new SelectList(_context.mau_Sacs, "ID", "ma_mau", san_Pham_Chi_Tiet.Mau_SacID);
            ViewData["San_PhamID"] = new SelectList(_context.san_Phams, "ID", "mo_ta", san_Pham_Chi_Tiet.San_PhamID);
            return View(san_Pham_Chi_Tiet);
        }

        // POST: San_Pham_Chi_Tiet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,MaSP,moTa,ten_SPCT,gia,so_luong,trang_thai,ngay_tao,ngay_sua,Kich_ThuocID,Mau_SacID,San_PhamID")] San_Pham_Chi_Tiet san_Pham_Chi_Tiet)
        {
            if (id != san_Pham_Chi_Tiet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(san_Pham_Chi_Tiet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!San_Pham_Chi_TietExists(san_Pham_Chi_Tiet.ID))
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
            ViewData["Kich_ThuocID"] = new SelectList(_context.kich_Thuocs, "ID", "ten_kich_thuoc", san_Pham_Chi_Tiet.Kich_ThuocID);
            ViewData["Mau_SacID"] = new SelectList(_context.mau_Sacs, "ID", "ma_mau", san_Pham_Chi_Tiet.Mau_SacID);
            ViewData["San_PhamID"] = new SelectList(_context.san_Phams, "ID", "mo_ta", san_Pham_Chi_Tiet.San_PhamID);
            return View(san_Pham_Chi_Tiet);
        }

        // GET: San_Pham_Chi_Tiet/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var san_Pham_Chi_Tiet = await _context.san_Pham_Chi_Tiets
                .Include(s => s.Kich_Thuoc)
                .Include(s => s.Mau_Sac)
                .Include(s => s.San_Pham)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (san_Pham_Chi_Tiet == null)
            {
                return NotFound();
            }

            return View(san_Pham_Chi_Tiet);
        }

        // POST: San_Pham_Chi_Tiet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var san_Pham_Chi_Tiet = await _context.san_Pham_Chi_Tiets.FindAsync(id);
            if (san_Pham_Chi_Tiet != null)
            {
                _context.san_Pham_Chi_Tiets.Remove(san_Pham_Chi_Tiet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool San_Pham_Chi_TietExists(Guid id)
        {
            return _context.san_Pham_Chi_Tiets.Any(e => e.ID == id);
        }
    }
}
