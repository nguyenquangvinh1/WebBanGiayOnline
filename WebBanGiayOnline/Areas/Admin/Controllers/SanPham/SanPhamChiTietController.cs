using ClssLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Data;

namespace WebBanGiay.Areas.Admin.Controllers.SanPham
{

    [Area("Admin")] /*sfsdfsd*/
    public class SanPhamChiTietController : Controller
    {
        private readonly AppDbContext _context;

        public SanPhamChiTietController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("id")]
        public async Task<IActionResult> Index(Guid? id)
        {
            var sp = await _context.san_Phams.FirstOrDefaultAsync(x => x.ID == id);
            ViewData["San_Pham"] = sp;
            return View(await _context.san_Pham_Chi_Tiets.Where(x => x.San_PhamID == id).ToListAsync());
        }

        // GET: KieuDangController/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanCT = await _context.san_Pham_Chi_Tiets
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sanCT == null)
            {
                return NotFound();
            }

            return View(sanCT);
        }

        // GET: KieuDangController/Create
        public ActionResult Create()
        {
            ViewData["Kich_ThuocID"] = new SelectList(_context.kich_Thuocs.ToList(), "ID", "ten_kich_thuoc");
            ViewData["Mau_SacID"] = new SelectList(_context.mau_Sacs.ToList(), "ID", "ten_mau");
            return View();
        }

        // POST: KieuDangController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ma,gia,so_luong,trang_thai,Kich_ThuocID,Mau_SacID")] San_Pham_Chi_Tiet sanCT)
        {
            if (ModelState.IsValid)
            {
                sanCT.ID = Guid.NewGuid();
                _context.san_Pham_Chi_Tiets.Add(sanCT);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sanCT);
        }

        // GET: KieuDangController/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            ViewData["Kich_ThuocID"] = new SelectList(_context.kich_Thuocs.ToList(), "ID", "ten_kich_thuoc");
            ViewData["Mau_SacID"] = new SelectList(_context.mau_Sacs.ToList(), "ID", "ten_mau");
            if (id == null)
            {
                return NotFound();
            }

            var sanCT = await _context.san_Pham_Chi_Tiets.FindAsync(id);
            if (sanCT == null)
            {
                return NotFound();
            }
            return View(sanCT);
        }

        // POST: KieuDangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ma,gia,so_luong,trang_thai,Kich_ThuocID,Mau_SacID")] San_Pham_Chi_Tiet sanCT)
        {
            var sanC = await _context.san_Pham_Chi_Tiets.FindAsync(sanCT.ID);
            if (sanC == null)
                return NotFound();
            _context.Entry(sanCT).State = EntityState.Modified;
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

            var sanCT = await _context.san_Pham_Chi_Tiets
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sanCT == null)
            {
                return NotFound();
            }

            return View(sanCT);
        }

        // POST: KieuDangController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var sanCT = await _context.san_Pham_Chi_Tiets.FindAsync(id);
            if (sanCT != null)
            {
                _context.san_Pham_Chi_Tiets.Remove(sanCT);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult GetAllSanPhamChiTiet()
        {
            try
            {
                var list = _context.san_Pham_Chi_Tiets
                    .Include(x => x.Kich_Thuoc)
                    .Include(x => x.Mau_Sac)
                    .Select(x => new {
                        id = x.ID,
                        imageUrl = "",               // hoặc x.HinhAnh nếu có
                        name = x.ten_SPCT,          // Tên sản phẩm chi tiết
                        price = x.gia,              // Giá bán
                        dbQuantity = x.so_luong,    // Số lượng thực tế từ DB
                        size = x.Kich_Thuoc.ten_kich_thuoc,
                        color = x.Mau_Sac.ma_mau,
                        status = x.trang_thai
                    })
                    .ToList();

                return Json(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public IActionResult UpdateQuantity([FromBody] UpdateQuantityRequest req)
        {
            try
            {
                var sp = _context.san_Pham_Chi_Tiets.FirstOrDefault(x => x.ID == req.productId);
                if (sp == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm chi tiết." });
                }

                // newQty = sp.so_luong - req.delta
                // => delta = 1 => user +1 trong hóa đơn => DB -1
                // => delta = -1 => user -1 trong hóa đơn => DB +1
                int newQty = sp.so_luong - req.delta;

                if (newQty < 0)
                {
                    return Json(new { success = false, message = "Số lượng không đủ trong kho." });
                }

                sp.so_luong = newQty;
                _context.san_Pham_Chi_Tiets.Update(sp);
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public class UpdateQuantityRequest
        {
            public Guid productId { get; set; }
            public int delta { get; set; }
        }



    }
}
