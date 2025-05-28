using ClssLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Data;

namespace WebBanGiay.Areas.Admin.Controllers.SanPham
{

    [Area("Admin")] /*sfsdfsd*/
    [Authorize(AuthenticationSchemes = "AdminScheme", Policy = "AdminOrEmployeePolicy")]

    public class KichThuocController : Controller
    {
        private readonly AppDbContext _context;

        public KichThuocController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.kich_Thuocs.ToListAsync());
        }

        // GET: KieuDangController/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kich = await _context.kich_Thuocs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kich == null)
            {
                return NotFound();
            }

            return View(kich);
        }

        // GET: KieuDangController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KieuDangController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kich_Thuoc kich_Thuoc)
        {
            if (ModelState.IsValid)
            {
                kich_Thuoc.ID = Guid.NewGuid();
                _context.kich_Thuocs.Add(kich_Thuoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kich_Thuoc);
        }

        // GET: KieuDangController/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kich = await _context.kich_Thuocs.FindAsync(id);
            if (kich == null)
            {
                return NotFound();
            }
            return View(kich);
        }

        // POST: KieuDangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Kich_Thuoc kich_Thuoc)
        {
            var kich = await _context.kich_Thuocs.FindAsync(kich_Thuoc.ID);
            if (kich == null)
                return NotFound();
            _context.Entry(kich_Thuoc).State = EntityState.Modified;
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

            var kich = await _context.kich_Thuocs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kich == null)
            {
                return NotFound();
            }

            return View(kich);
        }

        // POST: KieuDangController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var kich = await _context.kich_Thuocs.FindAsync(id);
            if (kich != null)
            {
                _context.kich_Thuocs.Remove(kich);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
