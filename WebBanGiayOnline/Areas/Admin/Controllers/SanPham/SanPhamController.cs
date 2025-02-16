using ClssLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebBanGiay.Data;
using WebBanGiay.ViewModel;

namespace WebBanGiay.Areas.Admin.Controllers.SanPham
{

    [Area("Admin")] /*sfsdfsd*/
    public class SanPhamController : Controller
    {
        private readonly AppDbContext _context;

        public SanPhamController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _context.san_Phams
                .Include(x => x.Chat_Lieu)
                .Include(x => x.Co_Giay)
                .Include(x => x.Danh_Muc)
                .Include(x => x.De_Giay)
                .Include(x => x.Kieu_Dang)
                .Include(x => x.Loai_Giay)
                .Include(x => x.Mui_Giay)
                .AsNoTracking()
                .ToListAsync();
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
        public IActionResult Filter(string chatLieu, string coGiay, string danhMuc, string deGiay, string muiGiay, string kieuDang, string loaiGiay)
        {
            var query = _context.san_Phams.AsQueryable();

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

            var result = query
                .Include(x => x.Chat_Lieu)
                .Include(x => x.Co_Giay)
                .Include(x => x.Danh_Muc)
                .Include(x => x.De_Giay)
                .Include(x => x.Kieu_Dang)
                .Include(x => x.Loai_Giay)
                .Include(x => x.Mui_Giay)
                .AsNoTracking()
                .ToList();
            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus.ToList(), "ID", "ten_chat_lieu");
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays.ToList(), "ID", "ten_loai_co_giay");
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs.ToList(), "ID", "ten_danh_muc");
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays.ToList(), "ID", "ten_de_giay");
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays.ToList(), "ID", "ten_mui_giay");
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs.ToList(), "ID", "ten_kieu_dang");
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays.ToList(), "ID", "ten_loai_giay");

            return View("Index", result); // Trả về danh sách San_Pham và render lại view Index
        }


        [HttpPost]
        public IActionResult ChangeStatus(Guid id, int trang_thai)
        {
            var sanPham = _context.san_Phams.FirstOrDefault(sp => sp.ID == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            // Cập nhật trạng thái
            sanPham.trang_thai = trang_thai;
            _context.san_Phams.Update(sanPham);
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

            var san = await _context.san_Phams
                .FirstOrDefaultAsync(m => m.ID == id);
            if (san == null)
            {
                return NotFound();
            }

            return View(san);
        }

        // GET: KieuDangController/Create
        public  IActionResult Create()
        {
             ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus.ToList(), "ID", "ten_chat_lieu");
            ViewData["Co_GiayID"] = new SelectList( _context.co_Giays.ToList(), "ID", "ten_loai_co_giay");
            ViewData["Danh_MucID"] = new SelectList( _context.danh_Mucs.ToList(), "ID", "ten_danh_muc");
            ViewData["De_GiayID"] = new SelectList(  _context.de_Giays.ToList(), "ID", "ten_de_giay");
            ViewData["Mui_GiayID"] = new SelectList( _context.mui_Giays.ToList(), "ID", "ten_mui_giay");
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs.ToList(), "ID", "ten_kieu_dang");
            ViewData["Loai_GiayID"] = new SelectList( _context.loai_Giays.ToList(), "ID", "ten_loai_giay");
            ViewData["Kich_ThuocID"] = new SelectList( _context.kich_Thuocs.ToList(), "ID", "ten_kich_thuoc");
            ViewData["Mau_SacID"] = new SelectList( _context.mau_Sacs.ToList(), "ID", "ma_mau");
            return View();
        }

        // POST: KieuDangController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(San_Pham san_Pham,string lstSPCT)
        {
            var listCT = JsonConvert.DeserializeObject<List<San_PhamCTView>>(lstSPCT);

            var kich = await _context.kich_Thuocs.ToListAsync();
            var mau = await _context.mau_Sacs.ToListAsync();
            
                var SP = new San_Pham();
                SP.ID = Guid.NewGuid();
                SP.ten_san_pham = san_Pham.ten_san_pham;
                SP.mo_ta = san_Pham.mo_ta;
                SP.trang_thai = 1;
                SP.ngay_tao = DateTime.Now;
                SP.Kieu_DangID = san_Pham.Kieu_DangID;
                SP.Danh_MucID = san_Pham.Danh_MucID;
                SP.Loai_GiayID = san_Pham.Loai_GiayID;
                SP.Mui_GiayID = san_Pham.Mui_GiayID;
                SP.Co_GiayID = san_Pham.Co_GiayID;
                SP.De_GiayID = san_Pham.De_GiayID;
                SP.Chat_LieuID = san_Pham.Chat_LieuID;
                await _context.san_Phams.AddAsync(SP);
            foreach (var item in listCT)
            {
                var sp = new San_Pham_Chi_Tiet()
                {
                    ID = Guid.NewGuid(),
                    gia = item.gia,
                    so_luong = item.so_luong,
                    trang_thai = item.trang_thai,
                    ngay_tao = item.ngay_tao,
                    Kich_ThuocID = kich.Find(x => x.ten_kich_thuoc == item.Kich_Thuoc).ID,
                    Mau_SacID = mau.Find(x => x.ma_mau == item.Mau_Sac).ID,
                    San_PhamID = SP.ID
                };
                _context.san_Pham_Chi_Tiets.Add(sp);
            }
            await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: KieuDangController/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var san = await _context.san_Phams.Where(x => x.ID == id)
                .Include(x => x.Chat_Lieu)
                .Include(x => x.Co_Giay)
                .Include(x => x.Danh_Muc)
                .Include(x => x.De_Giay)
                .Include(x => x.Kieu_Dang)
                .Include(x => x.Loai_Giay)
                .Include(x => x.Mui_Giay)
                .AsNoTracking().FirstOrDefaultAsync();
            if (san == null)
            {
                return NotFound();
            }
            var lstSPCT = _context.san_Pham_Chi_Tiets
                .Where(x => x.San_PhamID == id).ToList();
            ViewData["lstSPCT"] = JsonConvert.SerializeObject(lstSPCT);



            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus.ToList(), "ID", "ten_chat_lieu",san.Chat_LieuID);
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays.ToList(), "ID", "ten_loai_co_giay", san.Co_GiayID);
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs.ToList(), "ID", "ten_danh_muc", san.Danh_MucID);
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays.ToList(), "ID", "ten_de_giay", san.De_GiayID);
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays.ToList(), "ID", "ten_mui_giay", san.Mui_GiayID);
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs.ToList(), "ID", "ten_kieu_dang", san.Kieu_DangID);
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays.ToList(), "ID", "ten_loai_giay", san.Loai_GiayID);
            ViewData["Kich_ThuocID"] = new SelectList(_context.kich_Thuocs.ToList(), "ID", "ten_kich_thuoc");
            ViewData["Mau_SacID"] = new SelectList(_context.mau_Sacs.ToList(), "ID", "ma_mau");
            return View(san);
        }

        // POST: KieuDangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(San_Pham san_Pham, string lstSPCT)
        {
            // Kiểm tra xem san_Pham và danh sách spct có null không
            if (san_Pham == null)
            {
                ModelState.AddModelError("", "Dữ liệu sản phẩm không hợp lệ.");
                return View(san_Pham); // Hiển thị lại trang với thông báo lỗi
            }
            var listCT = JsonConvert.DeserializeObject<List<San_Pham_Chi_Tiet>>(lstSPCT);

            foreach (var item in listCT)
            {
                var sp = _context.san_Pham_Chi_Tiets.Find(item.ID);



                sp.gia = item.gia;
                sp.so_luong = item.so_luong;
                sp.trang_thai = item.trang_thai;
                
                _context.san_Pham_Chi_Tiets.Update(sp);
            }

            var san = await _context.san_Phams.FindAsync(san_Pham.ID);
            if (san == null)
            {
                return NotFound();
            }

            _context.Entry(san_Pham).State = EntityState.Modified;
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

            var san = await _context.san_Phams
                .FirstOrDefaultAsync(m => m.ID == id);
            if (san == null)
            {
                return NotFound();
            }

            return View(san);
        }

        // POST: KieuDangController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var san = await _context.san_Phams.FindAsync(id);
            if (san != null)
            {
                _context.san_Phams.Remove(san);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
