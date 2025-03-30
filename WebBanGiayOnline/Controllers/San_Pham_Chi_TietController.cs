using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClssLib;
using WebBanGiay.Data;
using WebBanGiay.Models;
using System.ComponentModel;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace WebBanGiay.Controllers
{
    public class San_Pham_Chi_TietController : Controller
    {
        private readonly AppDbContext _context;

        public San_Pham_Chi_TietController(AppDbContext context)
        {
            _context = context;
        }

        // GET: San_Pham_Chi_Tiet
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 12)
        {
         
            var sanPham = _context.san_Phams
                .Where(sp=>sp.trang_thai==1)
           .Where(sp => sp.San_Pham_Chi_Tiets.Any(ct => ct.so_luong > 0))
           // Chỉ lấy sản phẩm có số lượng > 0
           .AsQueryable();

            var result = sanPham.Select(x => new HangHoaVM
            {
                ID = x.ID,
                TenHH = x.ten_san_pham,
                DonGia = _context.san_Pham_Chi_Tiets
                    .Where(z => z.San_PhamID == x.ID)
                    .Select(x => x.gia)
                    .Min(), // Nếu không có giá, trả về 0
                MoTa = x.mo_ta ?? "",
                Hinh = _context.anh_San_Phams.FirstOrDefault(z => z.San_PhamID == x.ID).anh_url ?? "/img/default.png",
              
            });

            // Đếm tổng số sản phẩm
            int totalItems = await result.CountAsync();
            var paginatedResult = await result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewData["TotalItems"] = totalItems;
            ViewData["PageNumber"] = pageNumber;
            ViewData["PageSize"] = pageSize;

            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus.ToList(), "ID", "ten_chat_lieu");
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays.ToList(), "ID", "ten_loai_co_giay");
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs.ToList(), "ID", "ten_danh_muc");
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays.ToList(), "ID", "ten_de_giay");
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays.ToList(), "ID", "ten_mui_giay");
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs.ToList(), "ID", "ten_kieu_dang");
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays.ToList(), "ID", "ten_loai_giay");

            return View(paginatedResult);
        }
        [HttpGet]
        public async Task<IActionResult> Filter(
string chatLieu, string coGiay, string danhMuc, string deGiay,
string muiGiay, string kieuDang, string loaiGiay,
int pageNumber = 1, int pageSize = 12)
        {
            Console.WriteLine($"📌 Nhận giá trị lọc: chatLieu={chatLieu}, coGiay={coGiay}, danhMuc={danhMuc}, deGiay={deGiay}, muiGiay={muiGiay}, kieuDang={kieuDang}, loaiGiay={loaiGiay}");

            var query = _context.san_Phams
                .Where(sp => sp.San_Pham_Chi_Tiets.Any(ct => ct.so_luong > 0)) // Chỉ lấy sản phẩm có số lượng > 0
                .AsQueryable();

            // Áp dụng bộ lọc
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

            // Truy vấn dữ liệu sản phẩm
            var result = query.Select(x => new HangHoaVM
            {
                ID = x.ID,
                TenHH = x.ten_san_pham,
                DonGia = _context.san_Pham_Chi_Tiets
                    .Where(z => z.San_PhamID == x.ID)
                    .Select(x => x.gia)
                    .Min(), // Nếu không có giá, trả về 0
                MoTa = x.mo_ta ?? "",
                Hinh = _context.anh_San_Phams
                    .Where(z => z.San_PhamID == x.ID)
                    .Select(z => z.anh_url)
                    .FirstOrDefault() ?? "/img/default.png",
            
            });

            // Đếm tổng số sản phẩm và áp dụng phân trang
            int totalItems = await result.CountAsync();
            var paginatedResult = await result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            Console.WriteLine($"🔍 Tổng sản phẩm tìm thấy: {totalItems}");

            // Lưu thông tin phân trang
            ViewData["TotalItems"] = totalItems;
            ViewData["PageNumber"] = pageNumber;
            ViewData["PageSize"] = pageSize;

            // Lưu giá trị bộ lọc vào ViewData để giữ lại khi tải trang
            ViewData["SelectedChatLieu"] = chatLieu;
            ViewData["SelectedCoGiay"] = coGiay;
            ViewData["SelectedDanhMuc"] = danhMuc;
            ViewData["SelectedDeGiay"] = deGiay;
            ViewData["SelectedMuiGiay"] = muiGiay;
            ViewData["SelectedKieuDang"] = kieuDang;
            ViewData["SelectedLoaiGiay"] = loaiGiay;

            // Truyền dữ liệu dropdown
            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus.ToList(), "ID", "ten_chat_lieu", chatLieu);
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays.ToList(), "ID", "ten_loai_co_giay", coGiay);
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs.ToList(), "ID", "ten_danh_muc", danhMuc);
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays.ToList(), "ID", "ten_de_giay", deGiay);
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays.ToList(), "ID", "ten_mui_giay", muiGiay);
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs.ToList(), "ID", "ten_kieu_dang", kieuDang);
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays.ToList(), "ID", "ten_loai_giay", loaiGiay);

            return View("Index", paginatedResult); // Trả về danh sách đã lọc
        }

        // GET: San_Pham_Chi_Tiet/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            List<string> defaul = new List<string>();
            var data1 = _context.san_Phams.Include(x => x.Loai_Giay).FirstOrDefault(x => x.ID == id);
           
            var anh1 = _context.anh_San_Phams.Where(x => x.San_PhamID == id).Select(x => x.anh_url).ToList();
            if (anh1.Count != 0)
            {
                defaul = anh1;
            }
            else
            {
                string a = "/img/default.png";
                defaul.Add(a);
            }
            var spct = _context.san_Pham_Chi_Tiets.Where(x => x.San_PhamID == id && x.trang_thai == 1).AsQueryable();
            var lsSPCT =  spct.Select(x => new SanPhamCT_VM
            {
                ID = x.ID,
                Gia = x.gia,
                SoLuong = x.so_luong,
                KichThuoc = x.Kich_ThuocID,
                MauSac = x.Mau_SacID
            }).ToList();
            foreach (var item in lsSPCT)
            {
                var anh = _context.anh_San_Pham_San_Pham_Chi_Tiets.FirstOrDefault(x => x.San_Pham_Chi_TietID == item.ID);
                if (anh == null)
                {
                    item.hinh = "/img/default.png";
                    continue;
                }
                var imgUrls = _context.anh_San_Phams
                    .Find(anh.Anh_San_PhamID);
                
                string url1 = imgUrls.anh_url;
                item.hinh = url1;
            }
            var sp = _context.san_Phams.Where(x => x.San_Pham_Chi_Tiets.Any(ct => ct.so_luong > 0)&& x.trang_thai == 1).AsQueryable();
            if (data1 == null)
            {
                TempData["Message"] = $"Không tìm thấy {id}";
                return Redirect("/404");
            }
            var result = new ChiTietHangHoaVM
            {
                ID = data1.ID,
                TenHH = data1.ten_san_pham,
                MoTa = data1.mo_ta,
                Hinh = defaul,
                lstsp = sp.Select(x => new HangHoaVM
                {
                    ID = x.ID,
                    TenHH = x.ten_san_pham,
                    Hinh = _context.anh_San_Phams
                     .Where(z => z.San_PhamID == x.ID)
                     .Select(z => z.anh_url)
                     .FirstOrDefault() ?? "/img/default.png",
                    DonGia = _context.san_Pham_Chi_Tiets
                     .Where(z => z.San_PhamID == x.ID)
                     .Select(x => x.gia)
                     .Min(),
                    MoTa = x.mo_ta ?? ""
                }).ToList(),
                lstspct = lsSPCT

            };
            ViewData["Kich_ThuocID"] = new SelectList(_context.kich_Thuocs, "ID", "ten_kich_thuoc");
            ViewData["Mau_SacID"] = new SelectList(_context.mau_Sacs, "ID", "ma_mau");
            return View(result);
        }
        // GET: San_Pham_Chi_Tiet/Create
        public IActionResult Create()
        {
            ViewData["Kich_ThuocID"] = new SelectList(_context.kich_Thuocs, "ID", "ten_kich_thuoc");
            ViewData["Mau_SacID"] = new SelectList(_context.mau_Sacs, "ID", "ma_mau");
            ViewData["San_PhamID"] = new SelectList(_context.san_Phams, "ID", "mo_ta");
            return View();
        }

        // POST: San_Pham_Chi_Tiet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MaSP,moTa,ten_SPCT,gia,so_luong,trang_thai,ngay_tao,ngay_sua,Kich_ThuocID,Mau_SacID,San_PhamID")] San_Pham_Chi_Tiet san_Pham_Chi_Tiet)
        {
            if (ModelState.IsValid)
            {
                san_Pham_Chi_Tiet.ID = Guid.NewGuid();
                _context.Add(san_Pham_Chi_Tiet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Kich_ThuocID"] = new SelectList(_context.kich_Thuocs, "ID", "ten_kich_thuoc", san_Pham_Chi_Tiet.Kich_ThuocID);
            ViewData["Mau_SacID"] = new SelectList(_context.mau_Sacs, "ID", "ma_mau", san_Pham_Chi_Tiet.Mau_SacID);
            ViewData["San_PhamID"] = new SelectList(_context.san_Phams, "ID", "mo_ta", san_Pham_Chi_Tiet.San_PhamID);
            return View(san_Pham_Chi_Tiet);
        }

        // GET: San_Pham_Chi_Tiet/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var san_Pham_Chi_Tiet = await _context.san_Pham_Chi_Tiets.FindAsync(id);
            if (san_Pham_Chi_Tiet == null)
            {
                return NotFound();
            }
            ViewData["Kich_ThuocID"] = new SelectList(_context.kich_Thuocs, "ID", "ten_kich_thuoc", san_Pham_Chi_Tiet.Kich_ThuocID);
            ViewData["Mau_SacID"] = new SelectList(_context.mau_Sacs, "ID", "ma_mau", san_Pham_Chi_Tiet.Mau_SacID);
            ViewData["San_PhamID"] = new SelectList(_context.san_Phams, "ID", "mo_ta", san_Pham_Chi_Tiet.San_PhamID);
            return View(san_Pham_Chi_Tiet);
        }

        // POST: San_Pham_Chi_Tiet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,MaSP,moTa,ten_SPCT,gia,so_luong,trang_thai,ngay_tao,ngay_sua,Kich_ThuocID,Mau_SacID,San_PhamID")] San_Pham_Chi_Tiet san_Pham_Chi_Tiet)
        {
            if (id != san_Pham_Chi_Tiet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(san_Pham_Chi_Tiet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!San_Pham_Chi_TietExists(san_Pham_Chi_Tiet.ID))
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
            ViewData["Kich_ThuocID"] = new SelectList(_context.kich_Thuocs, "ID", "ten_kich_thuoc", san_Pham_Chi_Tiet.Kich_ThuocID);
            ViewData["Mau_SacID"] = new SelectList(_context.mau_Sacs, "ID", "ma_mau", san_Pham_Chi_Tiet.Mau_SacID);
            ViewData["San_PhamID"] = new SelectList(_context.san_Phams, "ID", "mo_ta", san_Pham_Chi_Tiet.San_PhamID);
            return View(san_Pham_Chi_Tiet);
        }

        // GET: San_Pham_Chi_Tiet/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var san_Pham_Chi_Tiet = await _context.san_Pham_Chi_Tiets
                .Include(s => s.Kich_Thuoc)
                .Include(s => s.Mau_Sac)
                .Include(s => s.San_Pham)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (san_Pham_Chi_Tiet == null)
            {
                return NotFound();
            }

            return View(san_Pham_Chi_Tiet);
        }

        // POST: San_Pham_Chi_Tiet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var san_Pham_Chi_Tiet = await _context.san_Pham_Chi_Tiets.FindAsync(id);
            if (san_Pham_Chi_Tiet != null)
            {
                _context.san_Pham_Chi_Tiets.Remove(san_Pham_Chi_Tiet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool San_Pham_Chi_TietExists(Guid id)
        {
            return _context.san_Pham_Chi_Tiets.Any(e => e.ID == id);
        }
    }
}
