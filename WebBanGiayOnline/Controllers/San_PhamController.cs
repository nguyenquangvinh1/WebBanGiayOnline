using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClssLib;
using WebBanGiay.Data;

namespace WebBanGiay.Controllers
{
    public class San_PhamController : Controller
    {
        private readonly AppDbContext _context;

        public San_PhamController(AppDbContext context)
        {
            _context = context;
        }

        // GET: San_Pham
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.san_Phams.Include(s => s.Chat_Lieu).Include(s => s.Co_Giay).Include(s => s.Danh_Muc).Include(s => s.De_Giay).Include(s => s.Kieu_Dang).Include(s => s.Loai_Giay).Include(s => s.Mui_Giay);
            return View(await appDbContext.ToListAsync());
        }

        // GET: San_Pham/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
          
          

            var san_Pham = await _context.san_Phams
                .Include(s => s.Chat_Lieu)
                .Include(s => s.Co_Giay)
                .Include(s => s.Danh_Muc)
                .Include(s => s.De_Giay)
                .Include(s => s.Kieu_Dang)
                .Include(s => s.Loai_Giay)
                .Include(s => s.Mui_Giay)
                .FirstOrDefaultAsync(m => m.ID == id);
     

            return View(san_Pham);
        }

        // GET: San_Pham/Create
        public IActionResult Create()
        {
            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus, "ID", "ten_chat_lieu");
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays, "ID", "ten_loai_co_giay");
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs, "ID", "ten_danh_muc");
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays, "ID", "ten_de_giay");
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs, "ID", "ten_kieu_dang");
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays, "ID", "ten_loai_giay");
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays, "ID", "ten_mui_giay");
            return View();
        }

        // POST: San_Pham/Create
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
            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus, "ID", "ten_chat_lieu", san_Pham.Chat_LieuID);
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays, "ID", "ten_loai_co_giay", san_Pham.Co_GiayID);
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs, "ID", "ten_danh_muc", san_Pham.Danh_MucID);
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays, "ID", "ten_de_giay", san_Pham.De_GiayID);
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs, "ID", "ten_kieu_dang", san_Pham.Kieu_DangID);
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays, "ID", "ten_loai_giay", san_Pham.Loai_GiayID);
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays, "ID", "ten_mui_giay", san_Pham.Mui_GiayID);
            return View(san_Pham);
        }

        // GET: San_Pham/Edit/5
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
            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus, "ID", "ten_chat_lieu", san_Pham.Chat_LieuID);
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays, "ID", "ten_loai_co_giay", san_Pham.Co_GiayID);
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs, "ID", "ten_danh_muc", san_Pham.Danh_MucID);
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays, "ID", "ten_de_giay", san_Pham.De_GiayID);
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs, "ID", "ten_kieu_dang", san_Pham.Kieu_DangID);
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays, "ID", "ten_loai_giay", san_Pham.Loai_GiayID);
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays, "ID", "ten_mui_giay", san_Pham.Mui_GiayID);
            return View(san_Pham);
        }

        // POST: San_Pham/Edit/5
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
            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus, "ID", "ten_chat_lieu", san_Pham.Chat_LieuID);
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays, "ID", "ten_loai_co_giay", san_Pham.Co_GiayID);
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs, "ID", "ten_danh_muc", san_Pham.Danh_MucID);
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays, "ID", "ten_de_giay", san_Pham.De_GiayID);
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs, "ID", "ten_kieu_dang", san_Pham.Kieu_DangID);
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays, "ID", "ten_loai_giay", san_Pham.Loai_GiayID);
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays, "ID", "ten_mui_giay", san_Pham.Mui_GiayID);
            return View(san_Pham);
        }

        // GET: San_Pham/Delete/5
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

        // POST: San_Pham/Delete/5
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
