﻿using ClssLib;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using WebBanGiay.Areas.Admin.Models.ViewModel;
using WebBanGiay.Data;
using WebBanGiay.Helpers;
using WebBanGiay.Models.ViewModel;
using WebBanGiay.Service;
using static WebBanGiay.Models.ViewModel.GHNShipping;

namespace WebBanGiay.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext db;
        private readonly IVnPayService _vnPayservice;
        private IMomoService _momoService;
        private readonly IVnPayService _vnPayService;
        private readonly EmailService _emailService;
        private readonly IGhnService _ghnService;


        public CartController(AppDbContext db, IVnPayService vnPayservice, IMomoService momoService, EmailService emailService, IGhnService ghnService)
        {
            this.db = db;
            _vnPayservice = vnPayservice;
            _momoService = momoService;
            _emailService = emailService;
            _ghnService = ghnService;
        }

        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();

        public IActionResult Index(Guid id)
        {


            return View(Cart);
        }
        [HttpPost]
        public IActionResult UpdateQuantity(Guid id, int quantity)
        {
            var cart = Cart;

            if (cart != null)
            {
                var item = cart.FirstOrDefault(x => x.id == id);

                if (item != null)
                {
                    item.SoLuong = quantity;

                    item.ThanhTienGG = item.DonGia * quantity;
                }

                HttpContext.Session.Set(MySetting.CART_KEY, cart);

                return Json(new
                {
                    success = true,
                    newTotal = item.ThanhTien,
                    totalCart = cart.Sum(x => x.ThanhTien)
                });
            }

            return Json(new { success = false, message = "Không tìm thấy sản phẩm trong giỏ hàng!" });
        }

        [HttpGet]
        public IActionResult GetDiscounts()
        {
            var totalPrice = Cart.Sum(x => x.ThanhTien);
            var discounts = db.phieu_Giam_Gias
                .Where(x => x.kieu_giam_gia == 1 && x.trang_thai == 1 && x.gia_tri_toi_thieu <= totalPrice)
                .Select(x => new {
                    id = x.ID,
                    ten_phieu_giam_gia = x.ten_phieu_giam_gia,
                    gia_tri_giam = x.gia_tri_giam,
                    so_tien_giam_toi_da = x.so_tien_giam_toi_da,
                }).ToList();

    

            return Json(discounts);
        }


        [HttpPost]
        public IActionResult ApplyDiscount(double totalPrice , string nameDiscount)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY);
            if (cart != null && cart.Any())
            {
                foreach (var item in cart)
                {
                    totalPrice = cart.Sum(x => x.ThanhTien);
                    item.Discount = nameDiscount;
                    
                }

                HttpContext.Session.Set(MySetting.CART_KEY, cart);
               
                return Json(new { success = true, message = "Giảm giá đã áp dụng!" });
            }
            return Json(new { success = false, message = "Giỏ hàng rỗng!" });
        }

        [HttpPost]
        public IActionResult AddCart1(Guid id, int quantity)
        {
            var Cart1 = Cart;
            var item = Cart1.SingleOrDefault(x => x.id == id);
            var hinh = db.anh_San_Pham_San_Pham_Chi_Tiets
              .Include(c => c.Anh_San_Pham)
              .ToList();

            if (item == null)
            {
                var hanghoa = db.san_Pham_Chi_Tiets.SingleOrDefault(x => x.ID == id);
                if (hanghoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy {id}";
                    return Redirect("/404");
                }


                item = new CartItem
                {
                    id = hanghoa.ID,
                    TenHH = hanghoa.ten_SPCT,
                    DonGia = db.san_Pham_Chi_Tiets.Find(id).gia,
                    SoLuong = quantity,
                    Hinh = hinh?.FirstOrDefault(x => x.San_Pham_Chi_TietID == id)?.Anh_San_Pham.anh_url ?? "Default_Image_URL"
                };
                Cart1.Add(item);
            }
            else
            {
                item.SoLuong += quantity;


            }
            HttpContext.Session.Set(MySetting.CART_KEY, Cart1);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveCart(Guid id)
        {
            var Cart1 = Cart;
            var item = Cart1.SingleOrDefault(x => x.id == id);
            if (item != null)
            {
                Cart1.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, Cart1);
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult CheckOut()
        {
            foreach (var item in Cart)
            {
                var sanpham = db.san_Pham_Chi_Tiets.SingleOrDefault(x => x.ID == item.id);
                if (sanpham.so_luong < item.SoLuong)
                {
                    TempData["ErrorMessage"] = "Sản phẩm "+sanpham.ten_SPCT + " trong kho chỉ còn"  +  sanpham.so_luong + " sản phẩm " ;
                    return RedirectToAction("Index");
                }
            }

            if (Cart.Count == 0)
            {
                return Redirect("/");
            }
            return View(Cart);
        }
        [HttpPost]
        [Route("GetShippingFee")]
        public async Task<IActionResult> GetShippingFee([FromBody] GHNShipping request)
        {
            try
            {
                var ship = await _ghnService.CalculateFeeAsync(request);
                HttpContext.Session.SetInt32("ShippingFee", ship);
                return Json(new { success = true, ship });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi vận chuyển: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(CheckoutVM model, Guid? id)
        {
            if (ModelState.IsValid)
            {
               
                var shippingFee = HttpContext.Session.GetInt32("ShippingFee") ?? 0;
                var khach = new Tai_Khoan();
                var tien = new CartItem();            
                var lastHoaDon = db.hoa_Dons
                .OrderByDescending(h => h.MaHoaDon)
                .FirstOrDefault();
                if (model.GiongKhachHang)
                {
                    var khachhang = db.tai_Khoans
                        .Include(x => x.Vai_Tro)
                        .Where(x => x.Vai_Tro.ten_vai_tro == "Khách hàng");
                }
           
              

                Random TenBienRanDom = new Random();
                int soNgauNhien = TenBienRanDom.Next(10000, 99999);
                string newMa = $"HD{soNgauNhien}";
                var hoadon = new Hoa_Don
                {
                    MaHoaDon = newMa,
                    ten_nguoi_nhan = model.TenKhachHang ?? khach.ho_ten,
                    dia_chi = model.fulldiachi,
                    sdt_nguoi_nhan = model.Sdt ?? khach.sdt,
                    email_nguoi_nhan = model.Email ?? khach.email,
                    tong_tien = Cart.Sum(x => x.ThanhTienGG) == 0
                    ? Cart.Sum(x => x.ThanhTien)
                    : Cart.Sum(x => x.ThanhTienGG),
                    ngay_tao = DateTime.Now,
                    Ship = shippingFee,
                    discount = Cart.FirstOrDefault()?.Discount,
                    trang_thai = 0,
                    loai_hoa_don = 2,
                    ghi_chu = model.GhiChu,
                };
                db.Database.BeginTransaction();
                try
                {
                    db.Database.CommitTransaction();
                    db.Add(hoadon);
                    db.SaveChanges();
                    var cthd = new List<Hoa_Don_Chi_Tiet>();
                    foreach (var item in Cart)
                    {
                        var spChiTiet = db.san_Pham_Chi_Tiets
                                .SingleOrDefault(X => X.ID == item.id);

                        if (spChiTiet != null && spChiTiet.so_luong >= item.SoLuong)
                        {

                            spChiTiet.so_luong -= item.SoLuong;
                            db.Update(spChiTiet);
                        }



                        cthd.Add(new Hoa_Don_Chi_Tiet
                        {


                            ID = Guid.NewGuid(),
                            ma = newMa,
                            tensp = item.TenHH,
                            so_luong = item.SoLuong,
                            gia = item.DonGia,
                            thanh_tien = item.DonGia * item.SoLuong,
                            discount = Cart.FirstOrDefault()?.Discount,
                            phuongThucthanhtoan = 2,
                            Hoa_DonID = hoadon.ID,
                            ngay_tao = DateTime.Now,


                        });
                    }
                    db.AddRange(cthd);
                    db.SaveChanges();
                    HttpContext.Session.Set<List<CartItem>>(MySetting.CART_KEY, new List<CartItem>());
                    return View("Success");
                }
                catch
                {
                    db.Database.RollbackTransaction();

                }
            }





            return View(Cart);

        }
        [HttpPost]
        public IActionResult PayMent(CheckoutVM model)
        {
      
     

            
            var totalAmount = Cart.Sum(x => x.ThanhTienGG) == 0
                ? Cart.Sum(x => x.ThanhTien)
                : Cart.Sum(x => x.ThanhTienGG);
            var shippingFee = HttpContext.Session.GetInt32("ShippingFee") ?? 0;
           
            var orderId = new Random().Next(1000, 100000);
            var description = $"{model.TenKhachHang} {model.Sdt}";
            var createdDate = DateTime.Now;

            var vnPayModel = new VnPaymentRequestModel
            {
                Amount = totalAmount + shippingFee,
                CreatedDate = createdDate,
                Description = description,
                FullName = model.TenKhachHang,
                OrderId = orderId,
                Status = 3
            };

            // Lưu tạm thông tin đơn hàng và vận chuyển
            HttpContext.Session.Set("CheckoutInfo", model);
        

            // Chuyển hướng đến VNPay
            return Redirect(_vnPayservice.CreatePaymentUrl(HttpContext, vnPayModel));
        }



        public IActionResult PaymentSuccess()
        {
            return View("Success");
        }
        public IActionResult PaymentFail()
        {
            return View();
        }
        public IActionResult PaymentCallback()
        {
            var response = _vnPayservice.PaymentExecute(Request.Query);

            if (response.Success)
            {
                var shippingFee = HttpContext.Session.GetInt32("ShippingFee") ?? 0;
                var model = HttpContext.Session.Get<CheckoutVM>("CheckoutInfo");
            

                int soNgauNhien = new Random().Next(10000, 99999);
                string newMa = $"HD{soNgauNhien}";

                var khach = new Tai_Khoan();

                var hoadon = new Hoa_Don
                {
                    MaHoaDon = newMa,
                    ten_nguoi_nhan = model.TenKhachHang ?? khach.ho_ten,
                    dia_chi = model.fulldiachi,
                    sdt_nguoi_nhan = model.Sdt ?? khach.sdt,
                    email_nguoi_nhan = model.Email ?? khach.email,
                    tong_tien = Cart.Sum(x => x.ThanhTienGG) == 0
                        ? Cart.Sum(x => x.ThanhTien)
                        : Cart.Sum(x => x.ThanhTienGG),
                    Ship = shippingFee,
                    discount = Cart.FirstOrDefault()?.Discount,

                    ngay_tao = DateTime.Now,
                    trang_thai = 0,
                    loai_hoa_don = 2,
                    ghi_chu = model.GhiChu,
                };

                db.Database.BeginTransaction();
                try
                {
                    db.Add(hoadon);
                    db.SaveChanges();

                    var cthd = new List<Hoa_Don_Chi_Tiet>();
                    foreach (var item in Cart)
                    {
                        var spChiTiet = db.san_Pham_Chi_Tiets
                            .SingleOrDefault(X => X.ID == item.id);

                        if (spChiTiet != null && spChiTiet.so_luong >= item.SoLuong)
                        {
                            spChiTiet.so_luong -= item.SoLuong;
                            db.Update(spChiTiet);
                        }

                        cthd.Add(new Hoa_Don_Chi_Tiet
                        {
                            ID = Guid.NewGuid(),
                            ma = newMa,
                            so_luong = item.SoLuong,
                            gia = item.DonGia,
                            thanh_tien = item.DonGia * item.SoLuong,
                            phuongThucthanhtoan = 3,
                            Hoa_DonID = hoadon.ID,
                            ngay_tao = DateTime.Now,
                        });
                    }

                    db.AddRange(cthd);
                    db.SaveChanges();
                    db.Database.CommitTransaction();

          
                    HttpContext.Session.Remove(MySetting.CART_KEY);
                    HttpContext.Session.Remove("CheckoutInfo");
                    HttpContext.Session.Remove("ShippingInfo");

                    return View("Success");
                }
                catch
                {
                    db.Database.RollbackTransaction();
                    return View("PaymentFail");
                }
            }

            return View("PaymentFail");
        }

        [HttpPost]
        public async Task<IActionResult> PaymentMoMo(CheckoutVM model)
        {
            var hoadon1 = new Hoa_Don();
            var totalAmount = Cart.Sum(p => p.DonGia * p.SoLuong);

            var orderId = new Random().Next(1000, 100000).ToString();
            var description = $"{model.TenKhachHang} {model.Sdt}";
            var createdDate = DateTime.Now;

            var MomoModel = new MomoExecuteResponseModel
            {
                Amount = totalAmount,
              
                Description = description,
                FullName = model.TenKhachHang,
                OrderId = orderId,
                Status = hoadon1.trang_thai = 3
            };
            HttpContext.Session.Set("CheckoutInfo", model);
           
            var response = await _momoService.CreatePaymentMoMo(MomoModel);
                return Redirect(response.PayUrl);
          

        }
    

    }
}
    

