using Microsoft.AspNetCore.Mvc;

namespace WebBanGiay.Areas.Admin.Controllers.SanPham
{
    public class SanPhamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
