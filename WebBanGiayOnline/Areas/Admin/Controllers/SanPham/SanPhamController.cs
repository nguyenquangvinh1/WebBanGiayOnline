using ClssLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebBanGiay.Data;
using WebBanGiay.ViewModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Json(new { success = false, message = "⚠️ Không có file nào được tải lên!" });

            // Danh sách định dạng ảnh hợp lệ
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
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



        [HttpGet]
        public IActionResult Filter(string chatLieu, string coGiay, string danhMuc, string deGiay, string muiGiay, string kieuDang, string loaiGiay)
        {
            Console.WriteLine($"📌 Nhận giá trị lọc: chatLieu={chatLieu}, coGiay={coGiay}, danhMuc={danhMuc}, deGiay={deGiay}, muiGiay={muiGiay}, kieuDang={kieuDang}, loaiGiay={loaiGiay}");

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

            Console.WriteLine($"🔍 Tổng sản phẩm tìm thấy: {result.Count}");

            ViewData["Chat_LieuID"] = new SelectList(_context.chat_Lieus.ToList(), "ID", "ten_chat_lieu");
            ViewData["Co_GiayID"] = new SelectList(_context.co_Giays.ToList(), "ID", "ten_loai_co_giay");
            ViewData["Danh_MucID"] = new SelectList(_context.danh_Mucs.ToList(), "ID", "ten_danh_muc");
            ViewData["De_GiayID"] = new SelectList(_context.de_Giays.ToList(), "ID", "ten_de_giay");
            ViewData["Mui_GiayID"] = new SelectList(_context.mui_Giays.ToList(), "ID", "ten_mui_giay");
            ViewData["Kieu_DangID"] = new SelectList(_context.kieu_Dangs.ToList(), "ID", "ten_kieu_dang");
            ViewData["Loai_GiayID"] = new SelectList(_context.loai_Giays.ToList(), "ID", "ten_loai_giay");

            return View("Index", result); // Trả về danh sách đã lọc
        }


        [HttpPost]
        public async Task<IActionResult> AddCoGiay([FromBody] string tenCoGiay)
        {
            if (string.IsNullOrWhiteSpace(tenCoGiay))
            {
                return BadRequest(new { success = false, message = "Tên không hợp lệ" });
            }

            // Kiểm tra xem giá trị đã tồn tại chưa
            var existing = await _context.co_Giays.FirstOrDefaultAsync(l => l.ten_loai_co_giay == tenCoGiay);
            if (existing != null)
            {
                return Ok(new { success = true, id = existing.ID, text = existing.ten_loai_co_giay });
            }

            // Thêm giá trị mới vào database
            var newCoGiay = new Co_Giay { ten_loai_co_giay = tenCoGiay };
            _context.co_Giays.Add(newCoGiay);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, id = newCoGiay.ID, text = newCoGiay.ten_loai_co_giay });
        }

        [HttpPost]
        public async Task<IActionResult> AddDeGiay([FromBody] string tenDeGiay)
        {
            if (string.IsNullOrWhiteSpace(tenDeGiay))
            {
                return BadRequest(new { success = false, message = "Tên không hợp lệ" });
            }

            // Kiểm tra xem giá trị đã tồn tại chưa
            var existing = await _context.de_Giays.FirstOrDefaultAsync(l => l.ten_de_giay == tenDeGiay);
            if (existing != null)
            {
                return Ok(new { success = true, id = existing.ID, text = existing.ten_de_giay });
            }

            // Thêm giá trị mới vào database
            var newDeGiay = new De_Giay { ten_de_giay = tenDeGiay };
            _context.de_Giays.Add(newDeGiay);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, id = newDeGiay.ID, text = newDeGiay.ten_de_giay });
        }

        [HttpPost]
        public async Task<IActionResult> AddMuiGiay([FromBody] string tenMuiGiay)
        {
            if (string.IsNullOrWhiteSpace(tenMuiGiay))
            {
                return BadRequest(new { success = false, message = "Tên không hợp lệ" });
            }

            // Kiểm tra xem giá trị đã tồn tại chưa
            var existing = await _context.mui_Giays.FirstOrDefaultAsync(l => l.ten_mui_giay == tenMuiGiay);
            if (existing != null)
            {
                return Ok(new { success = true, id = existing.ID, text = existing.ten_mui_giay });
            }

            // Thêm giá trị mới vào database
            var newMuiGiay = new Mui_Giay { ten_mui_giay = tenMuiGiay };
            _context.mui_Giays.Add(newMuiGiay);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, id = newMuiGiay.ID, text = newMuiGiay.ten_mui_giay });
        }

        [HttpPost]
        public async Task<IActionResult> AddMauSac([FromBody] string tenMauSac)
        {
            if (string.IsNullOrWhiteSpace(tenMauSac))
            {
                return BadRequest(new { success = false, message = "Tên không hợp lệ" });
            }

            // Kiểm tra xem giá trị đã tồn tại chưa
            var existing = await _context.mau_Sacs.FirstOrDefaultAsync(l => l.ma_mau == tenMauSac);
            if (existing != null)
            {
                return Ok(new { success = true, id = existing.ID, text = existing.ma_mau });
            }

            // Thêm giá trị mới vào database
            var newMauSac = new Mau_Sac { ma_mau = tenMauSac };
            _context.mau_Sacs.Add(newMauSac);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, id = newMauSac.ID, text = newMauSac.ma_mau });
        }

        [HttpPost]
        public async Task<IActionResult> AddKieuDang([FromBody] string tenKieuDang)
        {
            if (string.IsNullOrWhiteSpace(tenKieuDang))
            {
                return BadRequest(new { success = false, message = "Tên không hợp lệ" });
            }

            // Kiểm tra xem giá trị đã tồn tại chưa
            var existing = await _context.kieu_Dangs.FirstOrDefaultAsync(l => l.ten_kieu_dang == tenKieuDang);
            if (existing != null)
            {
                return Ok(new { success = true, id = existing.ID, text = existing.ten_kieu_dang });
            }

            // Thêm giá trị mới vào database
            var newKieuDang = new Kieu_Dang { ten_kieu_dang = tenKieuDang };
            _context.kieu_Dangs.Add(newKieuDang);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, id = newKieuDang.ID, text = newKieuDang.ten_kieu_dang });
        }

        [HttpPost]
        public async Task<IActionResult> AddKichThuoc([FromBody] string tenKichThuoc)
        {
            if (string.IsNullOrWhiteSpace(tenKichThuoc))
            {
                return BadRequest(new { success = false, message = "Tên không hợp lệ" });
            }

            // Kiểm tra xem giá trị đã tồn tại chưa
            var existing = await _context.kich_Thuocs.FirstOrDefaultAsync(l => l.ten_kich_thuoc == tenKichThuoc);
            if (existing != null)
            {
                return Ok(new { success = true, id = existing.ID, text = existing.ten_kich_thuoc });
            }

            // Thêm giá trị mới vào database
            var newKichThuoc = new Kich_Thuoc { ten_kich_thuoc = tenKichThuoc };
            _context.kich_Thuocs.Add(newKichThuoc);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, id = newKichThuoc.ID, text = newKichThuoc.ten_kich_thuoc });
        }

        [HttpPost]
        public async Task<IActionResult> AddDanhMuc([FromBody] string tenDanhMuc)
        {
            if (string.IsNullOrWhiteSpace(tenDanhMuc))
            {
                return BadRequest(new { success = false, message = "Tên không hợp lệ" });
            }

            // Kiểm tra xem giá trị đã tồn tại chưa
            var existing = await _context.danh_Mucs.FirstOrDefaultAsync(l => l.ten_danh_muc == tenDanhMuc);
            if (existing != null)
            {
                return Ok(new { success = true, id = existing.ID, text = existing.ten_danh_muc });
            }

            // Thêm giá trị mới vào database
            var newDanhMuc = new Danh_Muc { ten_danh_muc = tenDanhMuc };
            _context.danh_Mucs.Add(newDanhMuc);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, id = newDanhMuc.ID, text = newDanhMuc.ten_danh_muc });
        }

        [HttpPost]
        public async Task<IActionResult> AddChatLieu([FromBody] string tenChatLieu)
        {
            if (string.IsNullOrWhiteSpace(tenChatLieu))
            {
                return BadRequest(new { success = false, message = "Tên không hợp lệ" });
            }

            // Kiểm tra xem giá trị đã tồn tại chưa
            var existing = await _context.chat_Lieus.FirstOrDefaultAsync(l => l.ten_chat_lieu == tenChatLieu);
            if (existing != null)
            {
                return Ok(new { success = true, id = existing.ID, text = existing.ten_chat_lieu });
            }

            // Thêm giá trị mới vào database
            var newChatLieu = new Chat_Lieu { ten_chat_lieu = tenChatLieu };
            _context.chat_Lieus.Add(newChatLieu);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, id = newChatLieu.ID, text = newChatLieu.ten_chat_lieu });
        }
        

        [HttpPost]
        public async Task<IActionResult> AddLoaiGiay([FromBody] string tenLoaiGiay)
        {
            if (string.IsNullOrWhiteSpace(tenLoaiGiay))
            {
                return BadRequest(new { success = false, message = "Tên không hợp lệ" });
            }

            // Kiểm tra xem giá trị đã tồn tại chưa
            var existing = await _context.loai_Giays.FirstOrDefaultAsync(l => l.ten_loai_giay == tenLoaiGiay);
            if (existing != null)
            {
                return Ok(new { success = true, id = existing.ID, text = existing.ten_loai_giay });
            }

            // Thêm giá trị mới vào database
            var newLoaiGiay = new Loai_Giay { ten_loai_giay = tenLoaiGiay };
            _context.loai_Giays.Add(newLoaiGiay);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, id = newLoaiGiay.ID, text = newLoaiGiay.ten_loai_giay });
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
            var query = _context.san_Phams.AsQueryable();
            var tenDaDangKy = new List<string>();
            foreach (var item in query)
            {
                tenDaDangKy.Add(item.ten_san_pham);
            }
            ViewData["TenGiay"] = tenDaDangKy;
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
        public async Task<IActionResult> Create(San_Pham san_Pham,string lstSPCT, string productImages)
        {
            

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
            // ✅ Xử lý ảnh sản phẩm nếu có
            List<Anh_San_Pham> anhSP = new List<Anh_San_Pham>();
            if (!string.IsNullOrEmpty(productImages))
            {
                var imgUrls = JsonConvert.DeserializeObject<List<string>>(productImages);
                foreach (var url in imgUrls)
                {
                    var anh = new Anh_San_Pham
                    {
                        ID = Guid.NewGuid(),
                        anh_url = url,
                        San_PhamID = SP.ID
                    };
                    await _context.anh_San_Phams.AddAsync(anh);
                    anhSP.Add(anh);
                }
            }
            if (!lstSPCT.IsNullOrEmpty())
            {

                var listCT = JsonConvert.DeserializeObject<List<San_PhamCTView>>(lstSPCT);
                foreach (var item in listCT)
                {
                    var sp = new San_Pham_Chi_Tiet()
                    {
                        ID = Guid.NewGuid(),
                        ten_SPCT = SP.ten_san_pham + " ["+ item.Mau_Sac +"-"+ item.Kich_Thuoc +"]",
                        gia = item.gia,
                        so_luong = item.so_luong,
                        trang_thai = item.trang_thai,
                        ngay_tao = item.ngay_tao,
                        Kich_ThuocID = kich.Find(x => x.ten_kich_thuoc == item.Kich_Thuoc).ID,
                        Mau_SacID = mau.Find(x => x.ma_mau == item.Mau_Sac).ID,
                        San_PhamID = SP.ID
                    };
                    _context.san_Pham_Chi_Tiets.Add(sp);

                    if (!string.IsNullOrEmpty(item.imgUrl))
                    {
                        var imgUrls = JsonConvert.DeserializeObject<List<string>>(productImages);
                        for (int i = 0;i< imgUrls.Count; i++)
                        {
                            var anh_sp = new Anh_San_Pham_San_Pham_Chi_Tiet()
                            {
                                ID = Guid.NewGuid(),
                                San_Pham_Chi_TietID = sp.ID,
                                Anh_San_PhamID = anhSP.Find(x => x.anh_url == imgUrls[i]).ID
                            };
                            await _context.anh_San_Pham_San_Pham_Chi_Tiets.AddAsync(anh_sp);
                        }
                    }
                    
                }
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


            var query = _context.san_Phams.AsQueryable();
            var tenDaDangKy = new List<string>();
            foreach (var item in query)
            {
                tenDaDangKy.Add(item.ten_san_pham);
            }
            ViewData["TenGiay"] = tenDaDangKy;

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
