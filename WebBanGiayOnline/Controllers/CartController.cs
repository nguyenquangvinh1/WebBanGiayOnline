using ClssLib;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebBanGiay.Data;
using WebBanGiay.Helpers;
using WebBanGiay.Models.ViewModel;
using WebBanGiay.Service;

namespace WebBanGiay.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext db;
        private readonly IVnPayService _vnPayservice;
        private IMomoService _momoService;
        private readonly IVnPayService _vnPayService;
    
        public CartController(AppDbContext db, IVnPayService vnPayservice, IMomoService momoService)
        {
            this.db = db;
            _vnPayservice = vnPayservice;
            _momoService = momoService;
        }

        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();

        public IActionResult Index()
        {
            return View(Cart);
        }
        public IActionResult AddCart(Guid id, int quantity = 1)
        {
            var Cart1 = Cart;
            var item = Cart1.SingleOrDefault(x => x.id == id);
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
                    DonGia = hanghoa.gia,
                    SoLuong = quantity,
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
        public IActionResult CheckOut(CheckoutVM model, Guid? id/* , string payment = "COD"*/)
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

                var customer = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMER);
                var diachi = new Dia_Chi();
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
                    /*  MaHoaDon = model.MaHoaDon*/
                    MaHoaDon = newMa,
                    ten_nguoi_nhan = model.TenKhachHang ?? khach.ho_ten,
                    dia_chi = model.DiaChi ?? diachi.dia_chi_chi_tiet,
                    sdt_nguoi_nhan = model.Sdt ?? khach.sdt,
                    email_nguoi_nhan = model.Email ?? khach.email,
                    tong_tien = Cart.Sum(x=>x.DonGia*x.SoLuong),
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
                        var sp = db.san_Pham_Chi_Tiets.FirstOrDefault(x => x.ten_SPCT == item.TenHH);
                        if (sp != null && sp.so_luong >= item.SoLuong) // Kiểm tra số lượng đủ hàng
                        {
                            sp.so_luong -= item.SoLuong; // Trừ số lượng sản phẩm

                            cthd.Add(new Hoa_Don_Chi_Tiet
                            {


                                ID = Guid.NewGuid(),
                                ma = newMa,
                                so_luong = item.SoLuong,
                                gia = item.DonGia,
                                Hoa_DonID = hoadon.ID,
                                ngay_tao = DateTime.Now,


                            });
                        }
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
    

