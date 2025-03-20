using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Data;
using WebBanGiay.Models.ViewModel;

namespace WebBanGiay.Controllers
{
    public class ShippingController : Controller
    {
        AppDbContext _db;
        public ShippingController(AppDbContext db)
        {
            _db = db;
        }
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("StoreShipping")]
        public async Task<IActionResult> StoreShipping(Shipping model, string huyen, string xa, string tinh, double gia)
        {
            try
            {
                if (model == null) return BadRequest("Invalid data");
                var shipExists = await _db.shippingModels
                    .AnyAsync(x => x.tinh == tinh && x.huyen == huyen && x.xa == xa);

                if (shipExists)
                {
                    return Ok(new { duplicate = true, message = "Dữ liệu không trống" });
                }

                // Chuyển đổi từ Shipping (ViewModel) sang ShippingModel (Entity)
                var shippingEntity = new ClssLib.ShippingModel
                {
                    tinh = tinh,
                    huyen = huyen,
                    xa = xa,
                    Gia = gia
                };

                _db.shippingModels.Add(shippingEntity);
                await _db.SaveChangesAsync();
                return Ok(new { success = true, message = "Dữ liệu đã lưu thành công" });
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return StatusCode(500, "Lỗi hệ thống");
            }
        }

    }
}
