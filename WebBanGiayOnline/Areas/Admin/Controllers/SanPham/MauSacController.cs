using ClssLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Data;

namespace WebBanGiay.Areas.Admin.Controllers.SanPham
{

    [Area("Admin")] /*123344*/
	[Authorize(AuthenticationSchemes = "AdminScheme", Policy = "AdminOrEmployeePolicy")]

	public class MauSacController : Controller
    {
        private readonly AppDbContext _context;

        public MauSacController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.mau_Sacs.ToListAsync());
        }

        // GET: KieuDangController/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mau = await _context.mau_Sacs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mau == null)
            {
                return NotFound();
            }

            return View(mau);
        }

        // GET: KieuDangController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KieuDangController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mau_Sac mau_Sac)
        {
            if (ModelState.IsValid)
            {
                mau_Sac.ID = Guid.NewGuid();
                _context.mau_Sacs.Add(mau_Sac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mau_Sac);
        }

        // GET: KieuDangController/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mau = await _context.mau_Sacs.FindAsync(id);
            if (mau == null)
            {
                return NotFound();
            }
            return View(mau);
        }

        // POST: KieuDangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Mau_Sac mau_Sac)
        {
            var mau = await _context.mau_Sacs.FindAsync(mau_Sac.ID);
            if (mau == null)
                return NotFound();
            _context.Entry(mau_Sac).State = EntityState.Modified;
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

            var mau = await _context.mau_Sacs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mau == null)
            {
                return NotFound();
            }

            return View(mau);
        }

        // POST: KieuDangController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var mau = await _context.mau_Sacs.FindAsync(id);
            if (mau != null)
            {
                _context.mau_Sacs.Remove(mau);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
