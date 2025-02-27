using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClssLib;
using WebBanGiay.Data;

namespace WebBanGiay.Areas.Admin
{
    [Area("Admin")]
    public class SanPhamController : Controller
    {
        private readonly AppDbContext _context;

        public SanPhamController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/SanPham
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.san_Phams.Include(s => s.Chat_Lieu).Include(s => s.Co_Giay).Include(s => s.Danh_Muc).Include(s => s.De_Giay).Include(s => s.Kieu_Dang).Include(s => s.Loai_Giay).Include(s => s.Mui_Giay);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/SanPham/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var san_Pham = await _context.san_Phams
                .Include(s => s.Chat_Lieu)
                .Include(s => s.Co_Giay)
                .Include(s => s.Danh_Muc)
                .Include(s => s.De_Giay)
                .Include(s => s.Kieu_Dang)
                .Include(s => s.Loai_Giay)
                .Include(s => s.Mui_Giay)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (san_Pham == null)
            {
                return NotFound();
            }

            return View(san_Pham);
        }

        // GET: Admin/SanPham/Create
        public IActionResult Create()
        {
            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus, "ID", "ma");
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays, "ID", "ma");
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs, "ID", "ma");
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays, "ID", "ma");
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs, "ID", "ma");
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays, "ID", "ma");
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays, "ID", "ma");
            return View();
        }

        // POST: Admin/SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ten_san_pham,mo_ta,trang_thai,ngay_tao,ngay_sua,nguoi_tao,nguoi_sua,Kieu_DangID,Danh_MucID,Loai_GiayID,Mui_GiayID,Co_GiayID,De_GiayID,Chat_LieuID")] San_Pham san_Pham)
        {
            if (ModelState.IsValid)
            {
                san_Pham.ID = Guid.NewGuid();
                _context.Add(san_Pham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus, "ID", "ma", san_Pham.Chat_LieuID);
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays, "ID", "ma", san_Pham.Co_GiayID);
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs, "ID", "ma", san_Pham.Danh_MucID);
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays, "ID", "ma", san_Pham.De_GiayID);
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs, "ID", "ma", san_Pham.Kieu_DangID);
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays, "ID", "ma", san_Pham.Loai_GiayID);
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays, "ID", "ma", san_Pham.Mui_GiayID);
            return View(san_Pham);
        }

        // GET: Admin/SanPham/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var san_Pham = await _context.san_Phams.FindAsync(id);
            if (san_Pham == null)
            {
                return NotFound();
            }
            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus, "ID", "ma", san_Pham.Chat_LieuID);
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays, "ID", "ma", san_Pham.Co_GiayID);
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs, "ID", "ma", san_Pham.Danh_MucID);
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays, "ID", "ma", san_Pham.De_GiayID);
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs, "ID", "ma", san_Pham.Kieu_DangID);
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays, "ID", "ma", san_Pham.Loai_GiayID);
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays, "ID", "ma", san_Pham.Mui_GiayID);
            return View(san_Pham);
        }

        // POST: Admin/SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,ten_san_pham,mo_ta,trang_thai,ngay_tao,ngay_sua,nguoi_tao,nguoi_sua,Kieu_DangID,Danh_MucID,Loai_GiayID,Mui_GiayID,Co_GiayID,De_GiayID,Chat_LieuID")] San_Pham san_Pham)
        {
            if (id != san_Pham.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(san_Pham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!San_PhamExists(san_Pham.ID))
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
            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus, "ID", "ma", san_Pham.Chat_LieuID);
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays, "ID", "ma", san_Pham.Co_GiayID);
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs, "ID", "ma", san_Pham.Danh_MucID);
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays, "ID", "ma", san_Pham.De_GiayID);
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs, "ID", "ma", san_Pham.Kieu_DangID);
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays, "ID", "ma", san_Pham.Loai_GiayID);
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays, "ID", "ma", san_Pham.Mui_GiayID);
            return View(san_Pham);
        }

        // GET: Admin/SanPham/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var san_Pham = await _context.san_Phams
                .Include(s => s.Chat_Lieu)
                .Include(s => s.Co_Giay)
                .Include(s => s.Danh_Muc)
                .Include(s => s.De_Giay)
                .Include(s => s.Kieu_Dang)
                .Include(s => s.Loai_Giay)
                .Include(s => s.Mui_Giay)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (san_Pham == null)
            {
                return NotFound();
            }

            return View(san_Pham);
        }

        // POST: Admin/SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var san_Pham = await _context.san_Phams.FindAsync(id);
            if (san_Pham != null)
            {
                _context.san_Phams.Remove(san_Pham);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool San_PhamExists(Guid id)
        {
            return _context.san_Phams.Any(e => e.ID == id);
        }
    }
}
