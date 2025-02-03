using Microsoft.AspNetCore.Mvc;

namespace WebBanGiay.Areas.Admin.Controllers.SanPham
{
    public class SanPhamChiTietController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
