using ClssLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Data;

namespace WebBanGiay.Areas.Admin.Controllers.SanPham
{

    [Area("Admin")] /*sfsdfsd*/
    public class SanPhamController : Controller
    {
        private readonly AppDbContext _context;

        public SanPhamController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.san_Phams.ToListAsync());
        }

        // GET: KieuDangController/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var san = await _context.san_Phams
                .FirstOrDefaultAsync(m => m.ID == id);
            if (san == null)
            {
                return NotFound();
            }

            return View(san);
        }

        // GET: KieuDangController/Create
        public  IActionResult Create()
        {
             ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus.ToList(), "ID", "ten_chat_lieu");
            ViewData["Co_GiayID"] = new SelectList( _context.co_Giays.ToList(), "ID", "ten_loai_co_giay");
            ViewData["Danh_MucID"] = new SelectList( _context.danh_Mucs.ToList(), "ID", "ten_danh_muc");
            ViewData["De_GiayID"] = new SelectList(  _context.de_Giays.ToList(), "ID", "ten_de_giay");
            ViewData["Mui_GiayID"] = new SelectList( _context.mui_Giays.ToList(), "ID", "ten_mui_giay");
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs.ToList(), "ID", "ten_kieu_dang");
            ViewData["Loai_GiayID"] = new SelectList( _context.loai_Giays.ToList(), "ID", "ten_loai_giay");
            return View();
        }

        // POST: KieuDangController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ten_san_pham,mo_ta,trang_thai,ngay_tao,ngay_sua,nguoi_tao,nguoi_sua,Chat_LieuID,Co_GiayID,Danh_MucID,De_GiayID,Mui_GiayID,Kieu_DangID,Loai_GiayID")]San_Pham san_Pham)
        {
            if (ModelState.IsValid)
            {
                san_Pham.ID = Guid.NewGuid();
                await _context.san_Phams.AddAsync(san_Pham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(san_Pham);
        }

        // GET: KieuDangController/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus.ToList(), "ID", "ten_chat_lieu");
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays.ToList(), "ID", "ten_loai_co_giay");
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs.ToList(), "ID", "ten_danh_muc");
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays.ToList(), "ID", "ten_de_giay");
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays.ToList(), "ID", "ten_mui_giay");
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs.ToList(), "ID", "ten_kieu_dang");
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays.ToList(), "ID", "ten_loai_giay");
            if (id == null)
            {
                return NotFound();
            }

            var san = await _context.san_Phams.FindAsync(id);
            if (san == null)
            {
                return NotFound();
            }
            return View(san);
        }

        // POST: KieuDangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ten_san_pham,mo_ta,trang_thai,ngay_tao,ngay_sua,nguoi_tao,nguoi_sua,Chat_LieuID,Co_GiayID,Danh_MucID,De_GiayID,Mui_GiayID,Kieu_DangID,Loai_GiayID")] San_Pham san_Pham)
        {
            var san = await _context.san_Phams.FindAsync(san_Pham.ID);
            if (san == null)
                return NotFound();
            _context.Entry(san_Pham).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: KieuDangController/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var san = await _context.san_Phams
                .FirstOrDefaultAsync(m => m.ID == id);
            if (san == null)
            {
                return NotFound();
            }

            return View(san);
        }

        // POST: KieuDangController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var san = await _context.san_Phams.FindAsync(id);
            if (san != null)
            {
                _context.san_Phams.Remove(san);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
