using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClssLib;
using WebBanGiay.Data;

namespace WebBanGiay.Areas.Admin.Controllers
{
    [Area("Admin")] /*sfsdfsd*/
    public class HoaDonController : Controller
    {
        private readonly AppDbContext _context;

        public HoaDonController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/HoaDon
        public async Task<IActionResult> Index()
        {
            return View(await _context.hoa_Dons.ToListAsync());
        }

        // GET: Admin/HoaDon/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoa_Don = await _context.hoa_Dons
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hoa_Don == null)
            {
                return NotFound();
            }

            return View(hoa_Don);
        }

        // GET: Admin/HoaDon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/HoaDon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,tong_tien,ghi_chu,trang_thai,dia_chi,sdt_nguoi_nhan,email_nguoi_nhan,loai_hoa_don,ten_nguoi_nhan,thoi_gian_nhan_hang,ngay_tao,ngay_sua,nguoi_tao,nguoi_sua")] Hoa_Don hoa_Don)
        {
            if (ModelState.IsValid)
            {
                hoa_Don.ID = Guid.NewGuid();
                _context.Add(hoa_Don);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hoa_Don);
        }

        // GET: Admin/HoaDon/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoa_Don = await _context.hoa_Dons.FindAsync(id);
            if (hoa_Don == null)
            {
                return NotFound();
            }
            return View(hoa_Don);
        }

        // POST: Admin/HoaDon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,tong_tien,ghi_chu,trang_thai,dia_chi,sdt_nguoi_nhan,email_nguoi_nhan,loai_hoa_don,ten_nguoi_nhan,thoi_gian_nhan_hang,ngay_tao,ngay_sua,nguoi_tao,nguoi_sua")] Hoa_Don hoa_Don)
        {
            if (id != hoa_Don.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoa_Don);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Hoa_DonExists(hoa_Don.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hoa_Don);
        }

        // GET: Admin/HoaDon/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoa_Don = await _context.hoa_Dons
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hoa_Don == null)
            {
                return NotFound();
            }

            return View(hoa_Don);
        }

        // POST: Admin/HoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var hoa_Don = await _context.hoa_Dons.FindAsync(id);
            if (hoa_Don != null)
            {
                _context.hoa_Dons.Remove(hoa_Don);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Hoa_DonExists(Guid id)
        {
            return _context.hoa_Dons.Any(e => e.ID == id);
        }
    }
}
