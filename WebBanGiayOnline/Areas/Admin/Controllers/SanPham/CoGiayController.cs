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

    public class CoGiayController : Controller
    {
        // GET: CoGiayController
        private readonly AppDbContext _context;

        public CoGiayController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.co_Giays.ToListAsync());
        }

        // GET: KieuDangController/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var co_Giay = await _context.co_Giays
                .FirstOrDefaultAsync(m => m.ID == id);
            if (co_Giay == null)
            {
                return NotFound();
            }

            return View(co_Giay);
        }

        // GET: KieuDangController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KieuDangController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Co_Giay co_Giay)
        {
            if (ModelState.IsValid)
            {
                co_Giay.ID = Guid.NewGuid();
                _context.co_Giays.Add(co_Giay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(co_Giay);
        }

        // GET: KieuDangController/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var co_Giay = await _context.co_Giays.FindAsync(id);
            if (co_Giay == null)
            {
                return NotFound();
            }
            return View(co_Giay);
        }

        // POST: KieuDangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Co_Giay co_Giay)
        {
            var co = await _context.kieu_Dangs.FindAsync(co_Giay.ID);
            if (co == null)
                return NotFound();
            _context.Entry(co_Giay).State = EntityState.Modified;
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

            var co = await _context.co_Giays
                .FirstOrDefaultAsync(m => m.ID == id);
            if (co == null)
            {
                return NotFound();
            }

            return View(co);
        }

        // POST: KieuDangController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var co = await _context.co_Giays.FindAsync(id);
            if (co != null)
            {
                _context.co_Giays.Remove(co);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
