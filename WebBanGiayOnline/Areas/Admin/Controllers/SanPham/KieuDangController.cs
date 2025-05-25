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

    public class KieuDangController : Controller
    {
        // GET: KieuDangController
        private readonly AppDbContext _context;

        public KieuDangController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.kieu_Dangs.ToListAsync());
        }

        // GET: KieuDangController/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kieu_dang = await _context.kieu_Dangs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kieu_dang == null)
            {
                return NotFound();
            }

            return View(kieu_dang);
        }

        // GET: KieuDangController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KieuDangController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kieu_Dang kieu_Dang)
        {
            if (ModelState.IsValid)
            {
                kieu_Dang.ID = Guid.NewGuid();
                _context.kieu_Dangs.Add(kieu_Dang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kieu_Dang);
        }

        // GET: KieuDangController/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kieu_dang = await _context.kieu_Dangs.FindAsync(id);
            if (kieu_dang == null)
            {
                return NotFound();
            }
            return View(kieu_dang);
        }

        // POST: KieuDangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Kieu_Dang kieu_Dang)
        {
            var kieu_dang1 = await _context.kieu_Dangs.FindAsync(kieu_Dang.ID);
            if (kieu_dang1 == null)
                return NotFound();
            _context.Entry(kieu_Dang).State = EntityState.Modified;
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

            var kieu_dang = await _context.kieu_Dangs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kieu_dang == null)
            {
                return NotFound();
            }

            return View(kieu_dang);
        }

        // POST: KieuDangController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var kieu_dang = await _context.kieu_Dangs.FindAsync(id);
            if (kieu_dang != null)
            {
                _context.kieu_Dangs.Remove(kieu_dang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
