using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebBanGiay.Areas.Admin.Controllers.SanPham
{
    public class LoaiGiayController : Controller
    {
        // GET: LoaiGiayController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LoaiGiayController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoaiGiayController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiGiayController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoaiGiayController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoaiGiayController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoaiGiayController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoaiGiayController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
