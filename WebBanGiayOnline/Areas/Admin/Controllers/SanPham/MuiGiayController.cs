using ClssLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Data;

namespace WebBanGiay.Areas.Admin.Controllers.SanPham
{

    [Area("Admin")] /*sfsdfsd*/
	[Authorize(AuthenticationSchemes = "AdminScheme", Policy = "AdminOrEmployeePolicy")]

	public class MuiGiayController : Controller
    {
        private readonly AppDbContext _context;

        public MuiGiayController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.mui_Giays.OrderByDescending(x => x.ngay_tao).ToListAsync());
        }

        // GET: KieuDangController/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mui = await _context.mui_Giays
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mui == null)
            {
                return NotFound();
            }

            return View(mui);
        }

        // GET: KieuDangController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KieuDangController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mui_Giay mui_Giay)
        {
            if (ModelState.IsValid)
            {
                mui_Giay.ID = Guid.NewGuid();
                mui_Giay.ngay_tao = DateTime.Now;
                _context.mui_Giays.Add(mui_Giay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mui_Giay);
        }

        // GET: KieuDangController/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mui = await _context.mui_Giays.FindAsync(id);
            if (mui == null)
            {
                return NotFound();
            }
            return View(mui);
        }

        // POST: KieuDangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Mui_Giay mui_Giay)
        {
            var mui = await _context.mui_Giays.FindAsync(mui_Giay.ID);
            if (mui == null)
                return NotFound();
            _context.Entry(mui_Giay).State = EntityState.Modified;
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

            var mui = await _context.mui_Giays
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mui == null)
            {
                return NotFound();
            }

            return View(mui);
        }

        // POST: KieuDangController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var mui = await _context.mui_Giays.FindAsync(id);
            if (mui != null)
            {
                _context.mui_Giays.Remove(mui);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
