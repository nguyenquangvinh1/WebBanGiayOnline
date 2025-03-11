using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebBanGiay.Data;
using WebBanGiay.Helpers;
using WebBanGiay.Models.ViewModel;

namespace WebBanGiay.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext db;
        public CartController(AppDbContext db)
        {
            this.db = db;
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
        public IActionResult CheckOut()
        {
            

            if(Cart.Count == 0) {
                return Redirect("/");
            }
            return View(Cart);
        }
    }
}
