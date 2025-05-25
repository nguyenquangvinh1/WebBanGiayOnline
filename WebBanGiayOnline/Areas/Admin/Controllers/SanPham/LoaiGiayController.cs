using ClssLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Data;

namespace WebBanGiay.Areas.Admin.Controllers.SanPham
{

    [Area("Admin")] /*sfsdfsd*/
    [Authorize(Policy = "AdminOrEmployeePolicy")]

    public class LoaiGiayController : Controller
    {
        private readonly AppDbContext _context;

        public LoaiGiayController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.loai_Giays.ToListAsync());
        }

        // GET: KieuDangController/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loai = await _context.loai_Giays
                .FirstOrDefaultAsync(m => m.ID == id);
            if (loai == null)
            {
                return NotFound();
            }

            return View(loai);
        }

        // GET: KieuDangController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KieuDangController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Loai_Giay loai_Giay)
        {
            if (ModelState.IsValid)
            {
                loai_Giay.ID = Guid.NewGuid();
                _context.loai_Giays.Add(loai_Giay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loai_Giay);
        }

        // GET: KieuDangController/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loai = await _context.loai_Giays.FindAsync(id);
            if (loai == null)
            {
                return NotFound();
            }
            return View(loai);
        }

        // POST: KieuDangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Loai_Giay loai_Giay)
        {
            var loai = await _context.loai_Giays.FindAsync(loai_Giay.ID);
            if (loai == null)
                return NotFound();
            _context.Entry(loai_Giay).State = EntityState.Modified;
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

            var loai = await _context.loai_Giays
                .FirstOrDefaultAsync(m => m.ID == id);
            if (loai == null)
            {
                return NotFound();
            }

            return View(loai);
        }

        // POST: KieuDangController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var loai = await _context.loai_Giays.FindAsync(id);
            if (loai != null)
            {
                _context.loai_Giays.Remove(loai);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
