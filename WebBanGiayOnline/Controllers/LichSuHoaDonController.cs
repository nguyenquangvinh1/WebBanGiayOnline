using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Data;

namespace WebBanGiay.Controllers
{
    public class LichSuHoaDonController : Controller
    {
        private readonly AppDbContext _context;
        public LichSuHoaDonController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirst("userid").Value;
                Guid id =  Guid.Parse(userId) ;
                var lichSu = _context.tai_Khoan_Hoa_Dons
                .OrderByDescending(x => x.ngay_tao)
                    .Include(x => x.Hoa_Don)
                    .Where(x => x.Tai_KhoanID == id);

                return View(lichSu);
            }
            return NotFound();
        }
    }
}
