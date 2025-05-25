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

    public class DanhMucController : Controller
    {
        private readonly AppDbContext _context;

        public DanhMucController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.danh_Mucs.ToListAsync());
        }

        // GET: KieuDangController/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danh = await _context.danh_Mucs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (danh == null)
            {
                return NotFound();
            }

            return View(danh);
        }

        // GET: KieuDangController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KieuDangController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Danh_Muc danh_Muc)
        {
            if (ModelState.IsValid)
            {
                danh_Muc.ID = Guid.NewGuid();
                _context.danh_Mucs.Add(danh_Muc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danh_Muc);
        }

        // GET: KieuDangController/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danh = await _context.danh_Mucs.FindAsync(id);
            if (danh == null)
            {
                return NotFound();
            }
            return View(danh);
        }

        // POST: KieuDangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Danh_Muc danh_Muc)
        {
            var danh = await _context.danh_Mucs.FindAsync(danh_Muc);
            if (danh == null)
                return NotFound();
            _context.Entry(danh_Muc).State = EntityState.Modified;
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

            var danh = await _context.danh_Mucs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (danh == null)
            {
                return NotFound();
            }

            return View(danh);
        }

        // POST: KieuDangController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var danh = await _context.danh_Mucs.FindAsync(id);
            if (danh != null)
            {
                _context.danh_Mucs.Remove(danh);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
