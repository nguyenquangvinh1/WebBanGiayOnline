using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebBanGiayOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOrEmployeePolicy")]
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
