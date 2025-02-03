using Microsoft.AspNetCore.Mvc;

namespace WebBanGiay.Areas.Admin.Controllers.SanPham
{
    public class MuiGiayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
