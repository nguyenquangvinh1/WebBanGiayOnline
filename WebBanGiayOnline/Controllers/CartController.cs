using ClssLib;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;
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
        public IActionResult AddCart(Guid id, int quantity = 1)
        {
            var Cart1 = Cart;
            var item = Cart1.SingleOrDefault(x => x.id == id);

            if (item == null)
            {
                var hanghoa = db.san_Phams.SingleOrDefault(x => x.ID == id);
                if (hanghoa == null)
                {
                    return Json(new { success = false, message = "Sản phẩm không tồn tại!" });
                }

                item = new CartItem
                {
                    id = hanghoa.ID,
                    TenHH = hanghoa.ten_san_pham,
                    DonGia = db.san_Pham_Chi_Tiets.Where(z => z.San_PhamID == hanghoa.ID).Select(x => x.gia).Min(),
                    SoLuong = quantity,
                    Hinh = db.anh_San_Phams.FirstOrDefault(z => z.San_PhamID == hanghoa.ID).anh_url ?? "/img/default.png",
                };
                Cart1.Add(item);
            }
            else
            {
                item.SoLuong += quantity;
            }

            HttpContext.Session.Set(MySetting.CART_KEY, Cart1);
            return Json(new { success = true, message = "Đã thêm vào giỏ hàng!", cartCount = Cart1.Sum(x => x.SoLuong) });
        }
        [HttpGet]
        public IActionResult RefreshCart()
        {
            return ViewComponent("Cart");
        }


        public IActionResult AddCart1(Guid id, int quantity = 1)
        {
            var Cart1 = Cart;
            var item = Cart1.SingleOrDefault(x => x.id == id);
            if (item == null)
            {
                var hanghoa = db.san_Phams.SingleOrDefault(x => x.ID == id);
                if (hanghoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy {id}";
                    return Redirect("/404");
                }

                item = new CartItem
                {
                    id = hanghoa.ID,
                    TenHH = hanghoa.ten_san_pham,
                    DonGia = db.san_Pham_Chi_Tiets.Where(z => z.San_PhamID == hanghoa.ID).Select(x => x.gia).Min(),
                    SoLuong = quantity,
                    Hinh = db.anh_San_Phams.FirstOrDefault(z => z.San_PhamID == hanghoa.ID).anh_url ?? "/img/default.png",
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
             
                var fee = await _ghnService.CalculateFeeAsync(request);
                return Json(new { success = true, fee });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi vận chuyển: " + ex.Message });
            }
        }

        [HttpPost]
       
        public IActionResult CheckOut(CheckoutVM model, Guid? id, GHNShipping request/* , string payment = "COD"*/)
        {
         
            if (ModelState.IsValid)
            {
                //    if (payment == "Thanh toán VnPay")
                //    {
                //        var hoadon1 = new Hoa_Don();
                //        var vnPayModel = new VnPaymentRequestModel
                //        {
                //            Amount = Cart.Sum(p => p.DonGia * p.SoLuong),
                //            CreatedDate = DateTime.Now,
                //            Description = $"{model.TenKhachHang} {model.Sdt}",
                //            FullName = model.TenKhachHang,
                //            OrderId = new Random().Next(1000, 100000),
                //            Status = hoadon1.trang_thai = 3,


                //        };
                //        return Redirect(_vnPayservice.CreatePaymentUrl(HttpContext, vnPayModel));
                //    }

                var fee =  _ghnService.CalculateFeeAsync(request);
                var khach = new Tai_Khoan();
                var tien = new CartItem();
                //var diachi = new Dia_Chi();
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
                //var khachhang = "84d87dac-2912-42f4-9b60-b7c9669c996a";
                //var diaChi = db.dia_Chis.Select(X=> new Dia_Chi()
                //{
                //    Tai_KhoanID = Guid.Parse("84d87dac-2912-42f4-9b60-b7c9669c996a"),
                //    tinh = X.tinh,
                //    huyen = X.huyen,
                //    xa = X.xa,
                //    dia_chi_chi_tiet = X.dia_chi_chi_tiet,
                //    loai_dia_chi = 1,
                //    ngay_tao = DateTime.Now,

                //});


                var hoadon = new Hoa_Don
                {
                    /*  MaHoaDon = model.MaHoaDon*/
                    MaHoaDon = newMa,
                    ten_nguoi_nhan = model.TenKhachHang ?? khach.ho_ten,
                    dia_chi = model.fulldiachi,
                    
                    sdt_nguoi_nhan = model.Sdt ?? khach.sdt,
                    email_nguoi_nhan = model.Email ?? khach.email,
                    tong_tien = Cart.Sum(x=>x.DonGia*x.SoLuong)  ,
                    ngay_tao = DateTime.Now,
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
                        var sp = db.san_Phams.FirstOrDefault(x => x.ten_san_pham == item.TenHH);
                       
                        var spChiTiet = db.san_Pham_Chi_Tiets
                                .Where(x => x.San_PhamID == sp.ID)
                                .OrderBy(x => x.so_luong) 
                                .FirstOrDefault();

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
                                thanh_tien= item.DonGia*item.SoLuong,
                                Hoa_DonID = hoadon.ID,
                                ngay_tao = DateTime.Now,


                            });
                        var taiKhoanHoaDon = new Tai_Khoan_Hoa_Don
                        {
                            ID = Guid.NewGuid(),
                            Ten = model.TenKhachHang ?? khach.ho_ten,
                            vai_tro = "Khách hàng",
                            ngay_tao = DateTime.Now,
                            
                            Hoa_DonID = hoadon.ID  // Liên kết với hóa đơn
                        };
                        db.Add(taiKhoanHoaDon);
                        db.SaveChanges();




                        db.AddRange(cthd);
                        db.SaveChanges();
                        HttpContext.Session.Set<List<CartItem>>(MySetting.CART_KEY, new List<CartItem>());
                        return View("Success");
                    }
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
            var hoadon1 = new Hoa_Don();
           var totalAmount = Cart.Sum(p => p.DonGia * p.SoLuong);

            var orderId = new Random().Next(1000, 100000);
            var description = $"{model.TenKhachHang} {model.Sdt}";
            var createdDate = DateTime.Now;

            var vnPayModel = new VnPaymentRequestModel
            {
                Amount = totalAmount,
                CreatedDate = createdDate,
                Description = description,
                FullName = model.TenKhachHang,
                OrderId = orderId,
                Status = hoadon1.trang_thai = 3
            };


           

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
        public IActionResult PaymentCallBack()
        {
            var response = _vnPayservice.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
                return RedirectToAction("CheckOut");
            }


            // Lưu đơn hàng vô database

            TempData["Message"] = $"Thanh toán VNPay thành công";
            return RedirectToAction("PaymentSuccess");
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

            var response = await _momoService.CreatePaymentMoMo(MomoModel);
            return Redirect(response.PayUrl);

        }
    

    }
}
    

