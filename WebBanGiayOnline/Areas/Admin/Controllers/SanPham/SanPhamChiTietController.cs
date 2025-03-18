using ClssLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebBanGiay.Data;
using WebBanGiay.ViewModel;

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
        public async Task<IActionResult> Index()
        {
            var list = await _context.san_Pham_Chi_Tiets
                .Include(x => x.Chat_Lieu)
                .Include(x => x.Co_Giay)
                .Include(x => x.Danh_Muc)
                .Include(x => x.De_Giay)
                .Include(x => x.Kieu_Dang)
                .Include(x => x.Loai_Giay)
                .Include(x => x.Mui_Giay)
                .AsNoTracking()
                .Select(x => new San_PhamCTView
                {
                    ID = x.ID,
                    Ten = x.ten_SPCT,
                    gia = (int)x.gia,
                    so_luong = x.so_luong,
                    trang_thai = x.trang_thai,
                    ngay_tao = x.ngay_tao,
                    Kich_Thuoc = x.Kich_Thuoc.ten_kich_thuoc,
                    Mau_Sac = x.Mau_Sac.ma_mau,
                    Kieu_Dang = x.Kieu_Dang.ten_kieu_dang,
                    Danh_Muc = x.Danh_Muc.ten_danh_muc,
                    Loai_Giay = x.Loai_Giay.ten_loai_giay,
                    Mui_Giay = x.Mui_Giay.ten_mui_giay,
                    Co_Giay = x.Co_Giay.ten_loai_co_giay,
                    De_Giay = x.De_Giay.ten_de_giay,
                    Chat_Lieu = x.Chat_Lieu.ten_chat_lieu
                })
                .ToListAsync();

            foreach (var item in list)
            {
                var anh = _context.anh_San_Pham_San_Pham_Chi_Tiets.Where(x => x.San_Pham_Chi_TietID == item.ID).Select(x => x.Anh_San_PhamID).ToList();
                var imgUrls = _context.anh_San_Phams
           .Where(x => anh.Contains(x.ID))
           .Select(x => x.anh_url)
           .ToList();

                item.imgUrl = JsonConvert.SerializeObject(imgUrls);
            }

            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus.ToList(), "ID", "ten_chat_lieu");
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays.ToList(), "ID", "ten_loai_co_giay");
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs.ToList(), "ID", "ten_danh_muc");
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays.ToList(), "ID", "ten_de_giay");
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays.ToList(), "ID", "ten_mui_giay");
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs.ToList(), "ID", "ten_kieu_dang");
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays.ToList(), "ID", "ten_loai_giay");

            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> Filter(string chatLieu, string coGiay, string danhMuc, string deGiay, string muiGiay, string kieuDang, string loaiGiay, string tenSanPham)
        {
            Console.WriteLine($"📌 Nhận giá trị lọc: chatLieu={chatLieu}, coGiay={coGiay}, danhMuc={danhMuc}, deGiay={deGiay}, muiGiay={muiGiay}, kieuDang={kieuDang}, loaiGiay={loaiGiay}, tenSanPham={tenSanPham}");

            var query = _context.san_Pham_Chi_Tiets.AsQueryable();

            if (!string.IsNullOrEmpty(chatLieu))
                query = query.Where(sp => sp.Chat_LieuID.ToString() == chatLieu);

            if (!string.IsNullOrEmpty(coGiay))
                query = query.Where(sp => sp.Co_GiayID.ToString() == coGiay);

            if (!string.IsNullOrEmpty(danhMuc))
                query = query.Where(sp => sp.Danh_MucID.ToString() == danhMuc);

            if (!string.IsNullOrEmpty(deGiay))
                query = query.Where(sp => sp.De_GiayID.ToString() == deGiay);

            if (!string.IsNullOrEmpty(muiGiay))
                query = query.Where(sp => sp.Mui_GiayID.ToString() == muiGiay);

            if (!string.IsNullOrEmpty(kieuDang))
                query = query.Where(sp => sp.Kieu_DangID.ToString() == kieuDang);

            if (!string.IsNullOrEmpty(loaiGiay))
                query = query.Where(sp => sp.Loai_GiayID.ToString() == loaiGiay);

            // Tìm kiếm theo tên sản phẩm (không phân biệt chữ hoa/thường)
            if (!string.IsNullOrEmpty(tenSanPham))
                query = query.Where(sp => sp.ten_SPCT.Contains(tenSanPham));

            var result = await query
                .Include(x => x.Chat_Lieu)
                .Include(x => x.Co_Giay)
                .Include(x => x.Danh_Muc)
                .Include(x => x.De_Giay)
                .Include(x => x.Kieu_Dang)
                .Include(x => x.Loai_Giay)
                .Include(x => x.Mui_Giay)
                .AsNoTracking()
                .Select(x => new San_PhamCTView
                {
                    ID = x.ID,
                    Ten = x.ten_SPCT,
                    gia = (int)x.gia,
                    so_luong = x.so_luong,
                    trang_thai = x.trang_thai,
                    ngay_tao = x.ngay_tao,
                    Kich_Thuoc = x.Kich_Thuoc.ten_kich_thuoc,
                    Mau_Sac = x.Mau_Sac.ma_mau,
                    Kieu_Dang = x.Kieu_Dang.ten_kieu_dang,
                    Danh_Muc = x.Danh_Muc.ten_danh_muc,
                    Loai_Giay = x.Loai_Giay.ten_loai_giay,
                    Mui_Giay = x.Mui_Giay.ten_mui_giay,
                    Co_Giay = x.Co_Giay.ten_loai_co_giay,
                    De_Giay = x.De_Giay.ten_de_giay,
                    Chat_Lieu = x.Chat_Lieu.ten_chat_lieu
                })
                .ToListAsync();

            foreach (var item in result)
            {
                var anh = _context.anh_San_Pham_San_Pham_Chi_Tiets.Where(x => x.San_Pham_Chi_TietID == item.ID).Select(x => x.Anh_San_PhamID).ToList();
                var imgUrls = _context.anh_San_Phams
           .Where(x => anh.Contains(x.ID))
           .Select(x => x.anh_url)
           .ToList();

                item.imgUrl = JsonConvert.SerializeObject(imgUrls);
            }

            Console.WriteLine($"🔍 Tổng sản phẩm tìm thấy: {result.Count}");

            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus.ToList(), "ID", "ten_chat_lieu", chatLieu);
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays.ToList(), "ID", "ten_loai_co_giay", coGiay);
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs.ToList(), "ID", "ten_danh_muc", danhMuc);
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays.ToList(), "ID", "ten_de_giay", deGiay);
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays.ToList(), "ID", "ten_mui_giay", muiGiay);
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs.ToList(), "ID", "ten_kieu_dang", kieuDang);
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays.ToList(), "ID", "ten_loai_giay", loaiGiay);


            return View("Index", result);
        }
        [HttpPost]
        public IActionResult ChangeStatus(Guid id, int trang_thai)
        {
            var sanPham = _context.san_Pham_Chi_Tiets.FirstOrDefault(sp => sp.ID == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            // Cập nhật trạng thái
            sanPham.trang_thai = trang_thai;
            _context.san_Pham_Chi_Tiets.Update(sanPham);
            _context.SaveChanges();

            return Json(new { success = true });
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
        public async Task<IActionResult> Create([Bind("ma,gia,so_luong,trang_thai,Kich_ThuocID,Mau_SacID")]San_Pham_Chi_Tiet sanCT)
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
                    imageUrl = (from asp in _context.anh_San_Pham_San_Pham_Chi_Tiets
                                join a in _context.anh_San_Phams on asp.Anh_San_PhamID equals a.ID
                                where asp.San_Pham_Chi_TietID == x.ID
                                select a.anh_url).FirstOrDefault() ?? "/img/default.jpg",  // Nếu null, dùng ảnh mặc định
                    ten_SPCT = x.ten_SPCT, // Đổi lại để đồng bộ với JS
                    price = x.gia,
                    dbQuantity = x.so_luong,
                    size = x.Kich_Thuoc != null ? x.Kich_Thuoc.ten_kich_thuoc : "Không xác định",
                    color = x.Mau_Sac != null ? x.Mau_Sac.ma_mau : "Không xác định",
                    status = x.trang_thai == 1 ? "Hoạt động" : "Không hoạt động"
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
