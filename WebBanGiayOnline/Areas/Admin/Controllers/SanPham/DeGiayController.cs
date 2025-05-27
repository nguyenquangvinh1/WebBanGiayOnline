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

	public class DeGiayController : Controller
    {
        private readonly AppDbContext _context;

        public DeGiayController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.de_Giays.ToListAsync());
        }

        // GET: KieuDangController/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var de = await _context.de_Giays
                .FirstOrDefaultAsync(m => m.ID == id);
            if (de == null)
            {
                return NotFound();
            }

            return View(de);
        }

        // GET: KieuDangController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KieuDangController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(De_Giay de_Giay)
        {
            if (ModelState.IsValid)
            {
                de_Giay.ID = Guid.NewGuid();
                _context.de_Giays.Add(de_Giay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(de_Giay);
        }

        // GET: KieuDangController/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var de = await _context.de_Giays.FindAsync(id);
            if (de == null)
            {
                return NotFound();
            }
            return View(de);
        }

        // POST: KieuDangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(De_Giay de_Giay)
        {
            var de = await _context.de_Giays.FindAsync(de_Giay.ID);
            if (de == null)
                return NotFound();
            _context.Entry(de_Giay).State = EntityState.Modified;
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

            var de = await _context.de_Giays
                .FirstOrDefaultAsync(m => m.ID == id);
            if (de == null)
            {
                return NotFound();
            }

            return View(de);
        }

        // POST: KieuDangController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var de = await _context.de_Giays.FindAsync(id);
            if (de != null)
            {
                _context.de_Giays.Remove(de);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
