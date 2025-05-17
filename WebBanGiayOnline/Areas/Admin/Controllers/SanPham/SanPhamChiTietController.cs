using ClssLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Policy;
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
        public async Task<IActionResult> Index(Guid? id)
        {
            var query = _context.san_Pham_Chi_Tiets.AsQueryable();
            if (id != null)
            {
                var exist = await _context.san_Phams.FirstOrDefaultAsync(x => x.ID == id);
                if (exist == null)
                {
                    ViewBag["message"] = "Sản phẩm không tồn tại";
                }
                query = query.Where(x => x.San_PhamID == id);
            }
            var list = await query
                .Include(x => x.Chat_Lieu)
                .Include(x => x.Co_Giay)
                .Include(x => x.Danh_Muc)
                .Include(x => x.De_Giay)
                .Include(x => x.Kieu_Dang)
                .Include(x => x.Loai_Giay)
                .Include(x => x.Mui_Giay)
                .OrderByDescending(x => x.ngay_sua)
                .AsNoTracking()
                .Select(x => new San_PhamCTView
                {
                    ID = x.ID,
                    ma = x.ma,
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
                    ma = x.ma,
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

            // Truyền giá trị đã chọn vào ViewData để giữ lại sau khi lọc
            ViewData["SelectedChatLieu"] = chatLieu;
            ViewData["SelectedCoGiay"] = coGiay;
            ViewData["SelectedDanhMuc"] = danhMuc;
            ViewData["SelectedDeGiay"] = deGiay;
            ViewData["SelectedMuiGiay"] = muiGiay;
            ViewData["SelectedKieuDang"] = kieuDang;
            ViewData["SelectedLoaiGiay"] = loaiGiay;
            ViewData["SelectedTenSanPham"] = tenSanPham;


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
        public async Task<IActionResult> UploadImage(Guid id ,IFormFile file)
        {

            if (file == null || file.Length == 0)
                return Json(new { success = false, message = "⚠️ Không có file nào được tải lên!" });

            // Danh sách định dạng ảnh hợp lệ
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".jfif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
                return Json(new { success = false, message = "❌ Định dạng ảnh không hợp lệ! Chỉ chấp nhận JPG, PNG, GIF." });

            try
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                string relativePath = $"/img/{uniqueFileName}"; // Đường dẫn ảnh

                return Json(new { success = true, filePath = relativePath });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "❌ Lỗi khi tải ảnh!", error = ex.Message });
            }
        }
        [HttpPost]
        public IActionResult ChangeStatus(Guid id, int trang_thai)
        {
            var sanPham = _context.san_Pham_Chi_Tiets.FirstOrDefault(sp => sp.ID == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            CheckStatusSP(sanPham.San_PhamID, trang_thai);
            // Cập nhật trạng thái
            sanPham.trang_thai = trang_thai;
            _context.san_Pham_Chi_Tiets.Update(sanPham);
            _context.SaveChanges();

            return Json(new { success = true });
        }
        public void CheckStatusSP(Guid id, int trang_thai)
        {
            var sp = _context.san_Phams.Find(id);
            if (sp == null) return;

            var spctList = _context.san_Pham_Chi_Tiets
                .Where(x => x.San_PhamID == id)
                .ToList();

            bool allMatch = spctList.All(x => x.trang_thai == trang_thai);

            if (allMatch)
            {
                sp.trang_thai = trang_thai;
                _context.SaveChanges();
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSpct([FromBody] San_PhamCTView model)
        {
            
            if (model == null || model.ID == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });
            }

            var sanPham = await _context.san_Pham_Chi_Tiets.FindAsync(model.ID);
            if (sanPham == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm" });
            }

            try
            {
                // Cập nhật dữ liệu sản phẩm
                sanPham.gia = model.gia;
                sanPham.so_luong = model.so_luong;
                sanPham.ngay_sua = DateTime.Now;
                sanPham.Kieu_DangID =  _context.kieu_Dangs.FirstOrDefault(x => x.ten_kieu_dang == model.Kieu_Dang).ID;
                sanPham.Danh_MucID =  _context.danh_Mucs.FirstOrDefault(x => x.ten_danh_muc == model.Danh_Muc).ID;
                sanPham.Loai_GiayID =  _context.loai_Giays.FirstOrDefault(x => x.ten_loai_giay == model.Loai_Giay).ID;
                sanPham.Mui_GiayID =  _context.mui_Giays.FirstOrDefault(x => x.ten_mui_giay == model.Mui_Giay).ID;
                sanPham.Co_GiayID =  _context.co_Giays.FirstOrDefault(x => x.ten_loai_co_giay == model.Co_Giay).ID;
                sanPham.De_GiayID =  _context.de_Giays.FirstOrDefault(x => x.ten_de_giay == model.De_Giay).ID;
                sanPham.Chat_LieuID =  _context.chat_Lieus.FirstOrDefault(x => x.ten_chat_lieu == model.Chat_Lieu).ID;

                // Lưu thay đổi vào database
                _context.san_Pham_Chi_Tiets.Update(sanPham);

                List<Anh_San_Pham> anh = new List<Anh_San_Pham>();
                List<string> lis = JsonConvert.DeserializeObject<List<string>>(model.imgUrl);
                if (lis == null)
                {
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Cập nhật thành công, Không có ảnh nào được thêm vào sản phẩm!" });
                }
                for (int i = 0; i < lis.Count; i++)
                {
                    var exist = _context.anh_San_Phams.FirstOrDefault(x => x.anh_url == lis[i]);
                    if (exist == null)
                    {
                        var anhMoi = new Anh_San_Pham
                        {
                            ID = Guid.NewGuid(),
                            anh_url = lis[i],
                            San_PhamID = sanPham.San_PhamID
                        };
                        await _context.anh_San_Phams.AddAsync(anhMoi);
                        anh.Add(anhMoi);
                        continue;
                    }
                    anh.Add(_context.anh_San_Phams.First(x => x.anh_url == lis[i]));
                }

                // Lấy danh sách ID của các ảnh đã được lưu trong bảng liên kết với San_Pham_Chi_TietID == spct.ID
                var existingAnhIds = _context.anh_San_Pham_San_Pham_Chi_Tiets
                    .Where(x => x.San_Pham_Chi_TietID == model.ID)
                    .Select(x => x.Anh_San_PhamID)
                    .ToList();


                // Lấy danh sách ID của các ảnh cần bỏ qua
                var anhIds = anh.Select(a => a.ID).ToList();

                // Lọc danh sách, bỏ qua các mục có Anh_San_PhamID trùng với danh sách anhIds
                var imgdel = _context.anh_San_Pham_San_Pham_Chi_Tiets
                    .Where(x => !anhIds.Contains(x.Anh_San_PhamID) && x.San_Pham_Chi_TietID == model.ID)
                    .ToList();
                foreach (var item in imgdel)
                {
                    _context.anh_San_Pham_San_Pham_Chi_Tiets.Remove(item);
                }


                // Lọc danh sách anh, chỉ giữ lại các ảnh chưa tồn tại trong bảng liên kết
                anh = anh.Where(a => !existingAnhIds.Contains(a.ID)).ToList();
                foreach (var item in anh)
                {
                    var link = new Anh_San_Pham_San_Pham_Chi_Tiet()
                    {
                        ID = Guid.NewGuid(),
                        Anh_San_PhamID = item.ID,
                        San_Pham_Chi_TietID = (Guid)model.ID
                    };
                    _context.anh_San_Pham_San_Pham_Chi_Tiets.Add(link);
                }


                await _context.SaveChangesAsync();

                return Ok(new { message = "Cập nhật thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", error = ex.Message });
            }
        }
        [HttpPost]
        public IActionResult UpdateSPCTALL([FromBody] List<San_PhamCTView> updatedSPCT)
        {
            if (updatedSPCT == null || !updatedSPCT.Any())
            {
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ" });
            }

            foreach (var spct in updatedSPCT)
            {
                var existingSPCT = _context.san_Pham_Chi_Tiets.FirstOrDefault(x => x.ID == spct.ID);

                if (existingSPCT != null)
                {
                    existingSPCT.so_luong = spct.so_luong;
                    existingSPCT.gia = spct.gia;
                    existingSPCT.ngay_sua = DateTime.Now;

                    _context.san_Pham_Chi_Tiets.Update(existingSPCT);
                }
            }

            _context.SaveChanges();
            return Ok(new { success = true, message = "Cập nhật thành công", updatedCount = updatedSPCT.Count });
        }

        [HttpGet]
        public IActionResult GetProductImages(Guid id)
        {
            var product = _context.san_Pham_Chi_Tiets.Find(id);

            if (product == null)
            {
                return Json(new string[] { }); // Trả về mảng rỗng nếu không tìm thấy sản phẩm
            }

            var imageUrls = _context.anh_San_Phams
                .Where(x => x.San_PhamID == product.San_PhamID)
                .Select(x => x.anh_url)
                .ToList();

            return Json(imageUrls); // Trả về danh sách URL ảnh
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
        public async Task<IActionResult> bhtqFilter(string chatLieu, string coGiay, string danhMuc, string deGiay, string muiGiay, string kieuDang, string loaiGiay, string tenSanPham)
        {
            Console.WriteLine($"📌 Nhận giá trị lọc: chatLieu={chatLieu}, coGiay={coGiay}, danhMuc={danhMuc}, deGiay={deGiay}, muiGiay={muiGiay}, kieuDang={kieuDang}, loaiGiay={loaiGiay}, tenSanPham={tenSanPham}");

            var query = _context.san_Pham_Chi_Tiets.Where(x=>x.trang_thai==1).AsQueryable();

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
                    status = x.trang_thai == 1 ? "Hoạt động" : "Không hoạt động",
                    chatlieu = x.Chat_Lieu != null ? x.Chat_Lieu.ten_chat_lieu : "Chưa có",
                    cogiay = x.Co_Giay != null ? x.Co_Giay.ten_loai_co_giay : "Chưa có",
                    degiay = x.De_Giay != null ? x.De_Giay.ten_de_giay : "Chưa có"
                })
                .ToListAsync();

           

            Console.WriteLine($"🔍 Tổng sản phẩm tìm thấy: {result.Count}");

            // Truyền giá trị đã chọn vào ViewData để giữ lại sau khi lọc
            ViewData["SelectedChatLieu"] = chatLieu;
            ViewData["SelectedCoGiay"] = coGiay;
            ViewData["SelectedDanhMuc"] = danhMuc;
            ViewData["SelectedDeGiay"] = deGiay;
            ViewData["SelectedMuiGiay"] = muiGiay;
            ViewData["SelectedKieuDang"] = kieuDang;
            ViewData["SelectedLoaiGiay"] = loaiGiay;
            ViewData["SelectedTenSanPham"] = tenSanPham;


            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus.ToList(), "ID", "ten_chat_lieu", chatLieu);
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays.ToList(), "ID", "ten_loai_co_giay", coGiay);
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs.ToList(), "ID", "ten_danh_muc", danhMuc);
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays.ToList(), "ID", "ten_de_giay", deGiay);
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays.ToList(), "ID", "ten_mui_giay", muiGiay);
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs.ToList(), "ID", "ten_kieu_dang", kieuDang);
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays.ToList(), "ID", "ten_loai_giay", loaiGiay);


            return  Json(result); 
        }

        //[HttpGet]
        //public IActionResult GetAllSanPhamChiTiet(int pageNumber = 1, int pageSize = 10)
        //{
        //    try
        //    {
        //        var query = _context.san_Pham_Chi_Tiets
        //            .Include(x => x.Kich_Thuoc)
        //            .Include(x => x.Mau_Sac)
        //            .Where(x => x.trang_thai == 1)
        //            .Select(x => new {
        //                id = x.ID,
        //                imageUrl = (from asp in _context.anh_San_Pham_San_Pham_Chi_Tiets
        //                            join a in _context.anh_San_Phams on asp.Anh_San_PhamID equals a.ID
        //                            where asp.San_Pham_Chi_TietID == x.ID
        //                            select a.anh_url).FirstOrDefault() ?? "/img/default.jpg",
        //                ten_SPCT = x.ten_SPCT,
        //                price = x.gia,
        //                dbQuantity = x.so_luong,
        //                size = x.Kich_Thuoc != null ? x.Kich_Thuoc.ten_kich_thuoc : "Chưa có",
        //                color = x.Mau_Sac != null ? x.Mau_Sac.ma_mau : "Chưa có",
        //                status = "Hoạt động",
        //                chatlieu=x.Chat_Lieu != null ?x.Chat_Lieu.ten_chat_lieu:"Chưa có",
        //                cogiay=x.Co_Giay!=null ?x.Co_Giay.ten_loai_co_giay:"Chưa có",
        //                degiay=x.De_Giay!=null ?x.De_Giay.ten_de_giay :"Chưa có"
        //            });

        //        int totalItems = query.Count();

        //        var list = query
        //            .Skip((pageNumber - 1) * pageSize)
        //            .Take(pageSize)
        //            .ToList();

        //        return Json(new
        //        {
        //            success = true,
        //            data = list,
        //            pagination = new
        //            {
        //                pageNumber,
        //                pageSize,
        //                totalItems,
        //                totalPages = (int)Math.Ceiling((double)totalItems / pageSize)
        //            }
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        [HttpGet]
        public IActionResult GetAllSanPhamChiTiet(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var baseQuery = _context.san_Pham_Chi_Tiets
                    .Include(x => x.Kich_Thuoc)
                    .Include(x => x.Mau_Sac)
                    .Where(x => x.trang_thai == 1)
                    // <-- thêm OrderByDescending theo ID
                    .OrderByDescending(x => x.ID);

                int totalItems = baseQuery.Count();

                var list = baseQuery
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new {
                        id = x.ID,
                        imageUrl = (from asp in _context.anh_San_Pham_San_Pham_Chi_Tiets
                                    join a in _context.anh_San_Phams on asp.Anh_San_PhamID equals a.ID
                                    where asp.San_Pham_Chi_TietID == x.ID
                                    select a.anh_url).FirstOrDefault() ?? "/img/default.jpg",
                        ten_SPCT = x.ten_SPCT,
                        price = x.gia,
                        dbQuantity = x.so_luong,
                        size = x.Kich_Thuoc != null ? x.Kich_Thuoc.ten_kich_thuoc : "Chưa có",
                        color = x.Mau_Sac != null ? x.Mau_Sac.ma_mau : "Chưa có",
                        status = "Hoạt động",
                        chatlieu = x.Chat_Lieu != null ? x.Chat_Lieu.ten_chat_lieu : "Chưa có",
                        cogiay = x.Co_Giay != null ? x.Co_Giay.ten_loai_co_giay : "Chưa có",
                        degiay = x.De_Giay != null ? x.De_Giay.ten_de_giay : "Chưa có"
                    })
                    .ToList();

                return Json(new
                {
                    success = true,
                    data = list,
                    pagination = new
                    {
                        pageNumber,
                        pageSize,
                        totalItems,
                        totalPages = (int)Math.Ceiling((double)totalItems / pageSize)
                    }
                });
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
