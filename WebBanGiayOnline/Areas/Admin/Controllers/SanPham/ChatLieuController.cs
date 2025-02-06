using ClssLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Data;

namespace WebBanGiay.Areas.Admin.Controllers.SanPham
{
    [Area("Admin")] /*sfsdfsd*/
    public class ChatLieuController : Controller
    {
        // GET: ChatLieuController
        // GET: CoGiayController
        private readonly AppDbContext _context;

        public ChatLieuController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.chat_Lieus.ToListAsync());
        }

        // GET: KieuDangController/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chat = await _context.chat_Lieus
                .FirstOrDefaultAsync(m => m.ID == id);
            if (chat == null)
            {
                return NotFound();
            }

            return View(chat);
        }

        // GET: KieuDangController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KieuDangController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Chat_Lieu chat_Lieu)
        {
            if (ModelState.IsValid)
            {
                chat_Lieu.ID = Guid.NewGuid();
                _context.chat_Lieus.Add(chat_Lieu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chat_Lieu);
        }

        // GET: KieuDangController/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chat = await _context.chat_Lieus.FindAsync(id);
            if (chat == null)
            {
                return NotFound();
            }
            return View(chat);
        }

        // POST: KieuDangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Chat_Lieu chat_Lieu)
        {
            var chat = await _context.chat_Lieus.FindAsync(chat_Lieu.ID);
            if (chat == null)
                return NotFound();
            _context.Entry(chat_Lieu).State = EntityState.Modified;
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

            var chat = await _context.chat_Lieus
                .FirstOrDefaultAsync(m => m.ID == id);
            if (chat == null)
            {
                return NotFound();
            }

            return View(chat);
        }

        // POST: KieuDangController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var chat = await _context.chat_Lieus.FindAsync(id);
            if (chat != null)
            {
                _context.chat_Lieus.Remove(chat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
