using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebBanGiay.Areas.Admin.Controllers.SanPham
{
    public class CoGiayController : Controller
    {
        // GET: CoGiayController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CoGiayController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CoGiayController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CoGiayController/Create
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

        // GET: CoGiayController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CoGiayController/Edit/5
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

        // GET: CoGiayController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CoGiayController/Delete/5
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
