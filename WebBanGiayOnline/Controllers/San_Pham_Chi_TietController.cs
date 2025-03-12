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
            var sanPham = _context.san_Pham_Chi_Tiets.AsQueryable();
            var result = sanPham.Select(x => new HangHoaVM
            {
                ID = x.ID,
                MaHh =(int) x.MaSP,
                TenHH = x.ten_SPCT,
                DonGia = x.gia,
                MoTa = x.moTa ?? ""

            });
            return View(result);
        }

        // GET: San_Pham_Chi_Tiet/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            var data1 = _context.san_Pham_Chi_Tiets.SingleOrDefault(x => x.ID == id);
            if(data1 == null)
            {
                TempData["Message"] = $"Không tìm thấy {id}";
                return Redirect("/404");
            }
            var result = new ChiTietHangHoaVM
            {
                ID = data1.ID,
                MaHh = (int)data1.MaSP,
                TenHH = data1.ten_SPCT,
                DonGia = data1.gia,
                MoTa = data1.moTa,
                SoLuongTon = data1.so_luong

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
