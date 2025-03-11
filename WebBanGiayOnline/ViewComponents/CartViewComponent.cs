using Microsoft.AspNetCore.Mvc;
using WebBanGiay.Helpers;
using WebBanGiay.Models.ViewModel;
namespace WebBanGiay.ViewComponents
{
    public class CartViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent

    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();

            return View("CartPanel", new CartModel
            {
                Quantity = cart.Sum(p => p.SoLuong),
                Total = cart.Sum(p => p.ThanhTien)
            });
        }

    }
}
